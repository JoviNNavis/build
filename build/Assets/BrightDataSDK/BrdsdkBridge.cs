using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using AOT;

#if UNITY_EDITOR_OSX
using UnityEditor;
using UnityEditor.iOS.Xcode;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode.Extensions;
#endif

namespace Brdsdk
{
    // 1. Call `BrdsdkBridge.init(...)` from `Start()` method of any game
    // object handler, e.g. instead of direct ads initialization.
    // 2. Generate Xcode project.
    // 3. In Settings screen (you should implement it) call `BrdsdkBridge.opt_out()`
    // when user turns off SDK, and call `BrdsdkBridge.show_consent()`
    // if user tries to turn back on.
    public class BrdsdkBridge
    {
        /// <summary>
		/// The consent screen is not yet shown.
		/// </summary>
        public static int CHOICE_NONE = 0;
        /// <summary>
		/// User accepted the consent screen.
		/// </summary>
        public static int CHOICE_AGREED = 1;
        /// <summary>
		/// User declined the consent screen.
		/// </summary>
        public static int CHOICE_DISAGREED = 2;

        /// <summary>
        /// The choice delegate method. Used to receive the user's choice.
        /// </summary>
        /// <param name="choice">Value representing the choice:
        /// - BrdsdkBridge.CHOICE_NONE - the consent screen is not yet shown;
        /// - BrdsdkBridge.CHOICE_AGREED - user accepted the consent screen;
        /// - BrdsdkBridge.CHOICE_DISAGREED - user declined the consent screen.
        /// </param>
        public delegate void ChoiceCallback(int choice);

        /// <summary>
        /// Stores the reference to ChoiceCallback method used to notify about
        /// changes of the user's choice.
        /// </summary>
        private static ChoiceCallback callback;

#if UNITY_IOS
        [DllImport("__Internal")]
        private static extern void brdsdk_set_delegate(delegate_message callback);
        [DllImport("__Internal")]
        private static extern void brdsdk_init(
            [MarshalAs(UnmanagedType.LPWStr)]string benefit,
            int benefit_len,
            [MarshalAs(UnmanagedType.LPWStr)]string agree_btn,
            int agree_btn_len,
            [MarshalAs(UnmanagedType.LPWStr)]string disagree_btn,
            int disagree_btn_len,
            bool skip_consent,
            string language = null);
        [DllImport("__Internal")]
        private static extern void brdsdk_opt_out();
        [DllImport("__Internal")]
        private static extern void brdsdk_show_consent(
            [MarshalAs(UnmanagedType.LPWStr)]string benefit,
            int benefit_len,
            [MarshalAs(UnmanagedType.LPWStr)]string agree_btn,
            int agree_btn_len,
            [MarshalAs(UnmanagedType.LPWStr)]string disagree_btn,
            int disagree_btn_len,
            string language = null
        );
        [DllImport("__Internal")]
        private static extern int brdsdk_get_choice();

        public delegate void delegate_message(int choice);

        /// <summary>
	    /// The method called from native SDK code when user's choice
	    /// is changed, or right after preceding `init()` method call.
	    /// </summary>
	    /// <param name="choice">Value representing the choice:
	    /// - 0 (BrdsdkBridge.CHOICE_NONE) - the consent screen is not yet shown;
	    /// - 1 (BrdsdkBridge.CHOICE_AGREED) - user accepted the consent screen;
	    /// - 2 (BrdsdkBridge.CHOICE_DISAGREED) - user declined the consent screen.</param>
        [MonoPInvokeCallback(typeof(BrdsdkBridge.delegate_message))]
        public static void on_choice_change(int choice) {
            Debug.Log("on_choice_change: choice=" + choice);
            if (BrdsdkBridge.callback != null) 
                BrdsdkBridge.callback(choice);
        }
#endif

        /// <summary>
        /// SDK initialization method. Used to initialize the SDK and configure
        /// BrdsdkBridge with the method used to handle the user's choice.
        /// </summary>
        /// <param name="benefit">benefit text which is used in the consent screen</param>
        /// <param name="agree_btn">consent screen “agree” button text</param>
        /// <param name="disagree_btn">consent screen “disagree” button text</param>
        /// <param name="on_choice_callback">The reference to the callback method</param>
		/// <param name="skip_consent">can be passed to skip showing the consent screen on the initialization of the API. The consent screen can be shown later with show_consent method.</param>
		/// <param name="language">the preferred language (optional): ar_SA, de-DE, en-US, es-ES, fa-AF, fr-FR, he-IL, hi-IN, it-IT, ja-JP, ko-KR, ms-MY, nl-NL, pt-PT, ro-RO, ru-RU, th, tr-TR, vi-VN, zh-CN, zh-TW</param>
        public static void init(
            string benefit,
            string agree_btn,
            string disagree_btn,
            ChoiceCallback on_choice_callback,
            bool skip_consent = false,
            string language = null
        )
        {
            BrdsdkBridge.callback = on_choice_callback;
#if UNITY_IOS
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                // register the delegate method
                brdsdk_set_delegate(on_choice_change);

                // initialize the SDK
                brdsdk_init(
                    benefit, benefit.Length,
                    agree_btn, agree_btn.Length,
                    disagree_btn, disagree_btn.Length,
                    skip_consent,
                    language);
                return;
            }
#endif
            BrdsdkBridge.callback(BrdsdkBridge.CHOICE_NONE);
        }

        /// <summary>
        /// Disable Bright SDK, e.g. from Settings screen
        /// </summary>
        public static void opt_out()
        {
#if UNITY_IOS
            if (Application.platform == RuntimePlatform.IPhonePlayer)
                brdsdk_opt_out();
#endif
        }

        /// <summary>
        /// Show consent screen, e.g. when trying to turn on from Settings screen
        /// </summary>
        /// <param name="benefit">benefit text which is used in the consent screen</param>
        /// <param name="agree_btn">consent screen “agree” button text</param>
        /// <param name="disagree_btn">consent screen “disagree” button text</param>
        /// <param name="language">the preferred language (optional): ar_SA, de-DE, en-US, es-ES, fa-AF, fr-FR, he-IL, hi-IN, it-IT, ja-JP, ko-KR, ms-MY, nl-NL, pt-PT, ro-RO, ru-RU, th, tr-TR, vi-VN, zh-CN, zh-TW</param>
        public static void show_consent(
            string benefit = null,
            string agree_btn = null,
            string disagree_btn = null,
            string language = null
        )
        {
#if UNITY_IOS
            if (Application.platform == RuntimePlatform.IPhonePlayer) {
                int benefit_len = 0;
                if (benefit != null)
                    benefit_len = benefit.Length;
                int agree_btn_len = 0;
                if (agree_btn != null)
                    agree_btn_len = agree_btn.Length;
                int disagree_btn_len = 0;
                if (disagree_btn != null)
                    disagree_btn_len = disagree_btn.Length;
                brdsdk_show_consent(
                    benefit, benefit_len,
                    agree_btn, agree_btn_len,
                    disagree_btn, disagree_btn_len,
                    language);
            }
#endif
        }

        /// <summary>
        /// Obtains the user's choice:
        /// - <see cref="CHOICE_AGREED">BrdsdkBridge.CHOICE_AGREED</see> - User agreed. SDK is enabled and works;
        /// - <see cref="CHOICE_DISAGREED">BrdsdkBridge.CHOICE_DISAGREED</see> - User disagreed. SDK disabled
        ///     and disconnected;
        /// - <see cref="CHOICE_NONE">BrdsdkBridge.CHOICE_NONE</see> - before the user passes the consent screen
        ///     for the first time.
        /// </summary>
        /// <returns>the user's choice</returns>
        public static int get_choice()
        {
#if UNITY_IOS
            if (Application.platform == RuntimePlatform.IPhonePlayer)
                return brdsdk_get_choice();
            else
                return BrdsdkBridge.CHOICE_NONE;
#else
            return BrdsdkBridge.CHOICE_NONE;
#endif
        }

#if UNITY_EDITOR_OSX
        /// <summary>
	    /// The method that tweaks the generated Xcode project -
	    /// links the target with brdsdk.framework
	    /// </summary>
	    /// <param name="target">The build target</param>
	    /// <param name="pathToBuiltProject">The path to the project</param>
        [PostProcessBuildAttribute(1)]
        public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject)
        {
            string projPath = PBXProject.GetPBXProjectPath(pathToBuiltProject);
            PBXProject proj = new PBXProject();
            proj.ReadFromString(File.ReadAllText(projPath));
            string targetGuid = proj.GetUnityMainTargetGuid();
            const string defaultLocationInProj = "BrightDataSDK";
            const string coreFrameworkName = "brdsdk.framework";
            string framework = Path.Combine(defaultLocationInProj, coreFrameworkName);
            string fileGuid = proj.AddFile(framework,
                "Frameworks/" + framework, PBXSourceTree.Sdk);
            PBXProjectExtensions.AddFileToEmbedFrameworks(proj, targetGuid, fileGuid);
            proj.SetBuildProperty(targetGuid,
                "LD_RUNPATH_SEARCH_PATHS", "$(inherited) @executable_path/Frameworks");
            proj.WriteToFile(projPath);
        }
#endif
    }
}
