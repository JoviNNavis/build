using System;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using AppCentralAPI;

#if AC_APPLOVIN
using AppLovinMax.Scripts.IntegrationManager.Editor;
#endif

using UnityEngine.Video;

namespace AppCentralCore
{
    [CustomEditor(typeof(AppCentralSettings))]
    public class AppCentralSettingsEditor : Editor, IPreprocessBuildWithReport
    {
        //private const string EditorPrefEditorIDFA = "EditorSDKExtentionIDFA";
        private AppCentralSettings Settings => target as AppCentralSettings;

        public int callbackOrder => -500;

        private const float networkFieldMinWidth = 250f;
        private const float versionFieldMinWidth = 190f;

        private static GUILayoutOption networkWidthOption = GUILayout.Width(networkFieldMinWidth);
        private static GUILayoutOption versionWidthOption = GUILayout.Width(versionFieldMinWidth);
        private GUIStyle _WarningLableStyle;

        private GUIStyle titleLabelStyle;
        private GUIStyle headerLabelStyle;
        private GUIStyle versionLabelStyle;
        private GUIStyle warningLabelStyle;

        static Appcentral_ReferenceManager appcentral_ReferenceManager;


        [MenuItem("AppCentral/AppCentral SDK/Edit SDK Setings", false, 100)]
        private static void EditSettings()
        {
            AppCentralSettings appCentralAPISetting = CreateAppCentralSDKSettings();

            if (appCentralAPISetting == null)
            {
                ACLogger.UserError("Appcentral SDK Setings is null");
            }
            else
            {
                ACLogger.UserDebug("Appcentral SDK is avalible");
                Selection.activeObject = appCentralAPISetting;
            }
        }

        private static AppCentralSettings CreateAppCentralSDKSettings()
        {
            AppCentralSettings settings = AppCentralSettings.Load();

            if (settings == null)
            {
                settings = CreateInstance<AppCentralSettings>();

                if (!AssetDatabase.IsValidFolder("Assets/Resources"))
                    AssetDatabase.CreateFolder("Assets", "Resources");

                if (!AssetDatabase.IsValidFolder("Assets/Resources/AppCentral"))
                    AssetDatabase.CreateFolder("Assets/Resources", "AppCentral");

                AssetDatabase.CreateAsset(
                    settings,
                    "Assets/Resources/AppCentral/AppCentral SDK Setting.asset"
                );
                settings = AppCentralSettings.Load();
            }

            return settings;
        }

        private AppLovinPreBuilder _appLovinPreBuilder = new AppLovinPreBuilder();

        private void setupGUIStyles()
        {
            titleLabelStyle = new GUIStyle(EditorStyles.label)
            {
                fontSize = 20,
                fontStyle = FontStyle.Bold,
                fixedHeight = 20,
                alignment = TextAnchor.MiddleCenter,
            };

            headerLabelStyle = new GUIStyle(EditorStyles.label)
            {
                fontSize = 14,
                fontStyle = FontStyle.Bold,
                fixedHeight = 18,
                alignment = TextAnchor.MiddleLeft
            };

            versionLabelStyle = new GUIStyle(EditorStyles.label)
            {
                fontSize = 12,
                fontStyle = FontStyle.Normal,
                fixedHeight = 16,
                alignment = TextAnchor.MiddleCenter
            };

            warningLabelStyle = new GUIStyle(EditorStyles.label)
            {
                fontSize = 12,
                fontStyle = FontStyle.Normal,
                fixedHeight = 12,
                alignment = TextAnchor.MiddleLeft
            };
        }

        public override void OnInspectorGUI()
        {
            //base.OnInspectorGUI();
            // _appLovinPreBuilder = FindObjectOfType<AppLovinPreBuilder>();
            //return;

            setupGUIStyles();

            SettingHeader();

            Splitter();

            AppSettingsMenu();
            Splitter();

            EditorGUILayout.LabelField("APPCENTRAL SDK SETTING PHASE-1:", headerLabelStyle);

            using (new EditorGUILayout.VerticalScope("box"))
            {
                EditorGUILayout.Space(10);

                //Settings.isSDKPhase1FoldOut = EditorGUILayout.Foldout(Settings.isSDKPhase1FoldOut, "PHASE-1 Setting", true);

                //if (Settings.isSDKPhase1FoldOut)
                {
                    AppsFlyerMenu();
                    Splitter();
                    GameAnalyticsMenu();
                    Splitter();
                    SmartLookMenu();
                }
                EditorGUILayout.Space(20);
            }

            Splitter();

            EditorGUILayout.LabelField("APPCENTRAL SDK SETTING PHASE-2:", headerLabelStyle);

            GUILayout.BeginHorizontal();
            //GUILayout.Space(25);

            string EnablePhase2SDKText = "Enable Phase 2 SDK";

            if (Settings.newSetting.UseAppCentralSDKPhase2)
            {
                EnablePhase2SDKText = "Disable Phase 2 SDK";
            }

            if (GUILayout.Button(EnablePhase2SDKText, GUILayout.Width(200)))
            {
                Settings.newSetting.UseAppCentralSDKPhase2 = !Settings
                    .newSetting
                    .UseAppCentralSDKPhase2;
            }

            if (!Settings.newSetting.UseAppCentralSDKPhase2)
            {
                EditorGUILayout.LabelField(
                    "All features under SDK Phase 2 are disabled",
                    warningLabelStyle
                );
            }

            GUILayout.EndHorizontal();
            EditorGUILayout.Space(10);

            using (new EditorGUILayout.VerticalScope("box"))
            {
                if (Settings.newSetting.UseAppCentralSDKPhase2)
                {
                    //Settings.isSDKPhase2FoldOut = EditorGUILayout.Foldout(Settings.isSDKPhase2FoldOut, "PHASE-2 Setting", true);
                    //if (Settings.isSDKPhase2FoldOut)
                    {
                        EditorGUILayout.Space(10);
                        OtherSettingsMenu();
                        Splitter();
                        SetupURLScheme();
                        Splitter();
                        OneSignalMenu();
                        Splitter();
                        AppLovinMenu();
                        Splitter();
                        InAppMenu();
                        EditorGUILayout.Space(10);
                    }
                }
                else
                {
                    Settings.newSetting.UseRateUS = false;
                    Settings.newSetting.UseFlightModePrompt = false;
                    Settings.newSetting.Set_URL_scheme = false;
                    Settings.newSetting.UseOneSignal = false;
                    Settings.newSetting.UseAppLovin = false;
                    Settings.newSetting.UseInApps = false;
                }
            }
            Splitter();

            SyncSettingButtons();
            EditorUtility.SetDirty(Settings);
        }

        private void SyncSettingButtons()
        {
            //GUILayout.Space(15);

            if (
                GUILayout.Button(
                    Environment.NewLine
                        + "Check and Sync Settings (It may take some time)"
                        + Environment.NewLine
                )
            )
            {
                SetupScriptDefineSymbolsBasedOnActivePlugings(Settings);
                _appLovinPreBuilder.RemoveAppLovInMediationNwtorks();
            }
        }

        private void SettingHeader()
        {
            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField("AppCental SDK Settings", titleLabelStyle);

            EditorGUILayout.Space(10);
            GUILayout.BeginHorizontal();
            GUILayout.Space(25);
            Settings.newSetting.ShowLogs = CreateToggleButton(
                Settings.newSetting.ShowLogs,
                "Show Debug Logs"
            );
            GUILayout.EndHorizontal();
            //SaveSettings();
            // LoadSettings();

            EditorGUILayout.Space(10);
            GUILayout.BeginHorizontal();
            GUILayout.Space(25);

            string showVersionInfoText = "Show Plugins Version Info";

            if (Settings.newSetting.ShowVersionInfo)
            {
                showVersionInfoText = "Hide Plugins Version Info";
            }

            if (GUILayout.Button(showVersionInfoText, GUILayout.Width(200)))
            {
                Settings.newSetting.ShowVersionInfo = !Settings.newSetting.ShowVersionInfo;
            }

            GUILayout.EndHorizontal();

            //GUILayout.BeginHorizontal();
            //GUILayout.Space(25);
            //using (new EditorGUILayout.VerticalScope(GUILayout.Width(500), GUILayout.Height(1000)))
            //{
            //    EditorGUILayout.LabelField("test 01:", headerLabelStyle);
            //}
            //GUILayout.EndHorizontal();

            if (Settings.newSetting.ShowVersionInfo)
            {
                EditorGUILayout.Space(10);
                EditorGUILayout.LabelField("Plugins Version Info:", headerLabelStyle);

                EditorGUILayout.Space(10);
                GUILayout.BeginHorizontal();
                GUILayout.Space(25);
                DrawHeaders("Platform", false);
                GUILayout.EndHorizontal();

                EditorGUILayout.Space(10);
                GUILayout.BeginHorizontal();
                GUILayout.Space(25);
                DrawPluginDetailRow(
                    "Appcentral Media SDK Phase 1",
                    AppCentralSDKVersionTracker.ACSDKVersion
                );
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(25);
                DrawPluginDetailRow(
                    "Appcentral Ads SDK Phase 2",
                    AppCentralSDKVersionTracker.ACMediaSDKPhase2Version
                );
                GUILayout.EndHorizontal();

                GUILayout.Space(5);

                GUILayout.BeginHorizontal();
                GUILayout.Space(25);
                DrawPluginDetailRow("AppsFlyer SDK", AppCentralSDKVersionTracker.AppsFlyerVersion);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(25);
                DrawPluginDetailRow(
                    "GameAnalytics SDK",
                    AppCentralSDKVersionTracker.GameAnalyticsVersion
                );
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(25);
                DrawPluginDetailRow("SmartLook SDK", AppCentralSDKVersionTracker.SmartLookVersion);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(25);
                DrawPluginDetailRow("OneSignal SDK", AppCentralSDKVersionTracker.OneSignalVersion);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(25);
                DrawPluginDetailRow("AppLovin SDK", AppCentralSDKVersionTracker.AppLovinVersion);
                GUILayout.EndHorizontal();


                GUILayout.BeginHorizontal();
                GUILayout.Space(25);
                DrawPluginDetailRow("Bright SDK", AppCentralSDKVersionTracker.BrightSDKVeriosn);
                GUILayout.EndHorizontal();

                //string AC_SDK_Version = "AppCentralSDK Phase-02: " + "0.1.1";
                //EditorGUILayout.LabelField(AC_SDK_Version, versionLabelStyle);
                //GUILayout.EndHorizontal();
            }
        }

        public void SaveSettings()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(25);

            if (GUILayout.Button("Save Setting to JSON", GUILayout.Width(200)))
            {
                FileHandler.SaveToJSON<newSetting>(
                    Settings.newSetting,
                    "Assets/AppCentralSettings.json"
                );
                RefreshEditorProjectWindow();
            }

            GUILayout.EndHorizontal();
        }

        public void LoadSettings()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(25);

            if (GUILayout.Button("Load Setting from JSON", GUILayout.Width(200)))
            {
                Settings.newSetting = FileHandler.ReadFromJSON<newSetting>(
                    "Assets/AppCentralSettings.json"
                );
            }

            GUILayout.EndHorizontal();
        }

        private void AppSettingsMenu()
        {
            EditorGUILayout.LabelField("GENERAL GAME SETTINGS:", headerLabelStyle);
            using (new EditorGUILayout.VerticalScope("box"))
            {
                EditorGUILayout.Space(10);
                GUILayout.BeginHorizontal();
                GUILayout.Space(25);
                Settings.newSetting.GameIcon = (Sprite)
                    EditorGUILayout.ObjectField(
                        "",
                        Settings.newSetting.GameIcon,
                        typeof(Sprite),
                        false,
                        GUILayout.Width(64),
                        GUILayout.Height(64)
                    );
                GUILayout.Label("Game Icon", GUILayout.Width(100), GUILayout.Height(64));
                GUILayout.EndHorizontal();

                EditorGUILayout.Space(10);

                GUILayout.BeginHorizontal();
                GUILayout.Space(25);
                GUILayout.Label("Game Name", GUILayout.Width(150));

                Settings.newSetting.GameName = EditorGUILayout.TextField(
                    "",
                    Settings.newSetting.GameName
                );

                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(25);
                GUILayout.Label("Project Bundle ID", GUILayout.Width(150));
                Settings.newSetting.BundleID = EditorGUILayout.TextField(
                    "",
                    Settings.newSetting.BundleID
                );
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(25);
                GUILayout.Label("IOS App ID", GUILayout.Width(150));
                Settings.newSetting.IOSAppID = EditorGUILayout.TextField(
                    "",
                    Settings.newSetting.IOSAppID
                );
                GUILayout.EndHorizontal();

                GUILayout.Space(10);

                GUILayout.BeginHorizontal();
                GUILayout.Space(25);
                GUILayout.Label("Terms&Condition URL", GUILayout.Width(150));
                Settings.newSetting.URLTermsOfConditions = "https://www.app-central.com/terms";
                Settings.newSetting.URLTermsOfConditions = EditorGUILayout.TextField(
                    "",
                    Settings.newSetting.URLTermsOfConditions
                );
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(25);
                GUILayout.Label("Privacy Policy URL", GUILayout.Width(150));
                if (!Settings.newSetting.OverRide_URLPrivacyPolicy)
                {
                    Settings.newSetting.URLPrivacyPolicy =
                        "https://www.app-central.com/privacy-policy";
                }

                //GUI.enabled = false;
                //if (!string.IsNullOrEmpty(Settings.newSetting.CurrentURl_Scheme))
                //{
                //    EditorGUILayout.TextField("", Settings.newSetting.CurrentURl_Scheme, GUILayout.Width(300));
                //}
                //else
                //{
                //    EditorGUILayout.TextField("", "No URL scheme has been set");
                //}
                //GUI.enabled = true;

                Settings.newSetting.URLPrivacyPolicy = EditorGUILayout.TextField(
                    "",
                    Settings.newSetting.URLPrivacyPolicy
                );
                GUILayout.EndHorizontal();
                EditorGUILayout.Space(10);
            }
        }

        private void OtherSettingsMenu()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("", GUILayout.Width(0));
            Settings.newSetting.UseFlightModePrompt = EditorGUILayout.Toggle(
                "",
                Settings.newSetting.UseFlightModePrompt,
                GUILayout.Width(15)
            );
            GUILayout.Label("Use Internet Connection Prompt");
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("", GUILayout.Width(0));
            Settings.newSetting.UseRateUS = EditorGUILayout.Toggle(
                "",
                Settings.newSetting.UseRateUS,
                GUILayout.Width(15)
            );
            GUILayout.Label("Use Rate Us Dialog");
            GUILayout.EndHorizontal();


        }

        private void AppsFlyerMenu()
        {
            Settings.newSetting.canUseAppsFlyer = CreateToggleButton(
                Settings.newSetting.canUseAppsFlyer,
                "Use AppsFlyer"
            );
            if (Settings.newSetting.canUseAppsFlyer)
            {
                Settings.newSetting.appsFlyer_devKey = CreateStringField(
                    Settings.newSetting.appsFlyer_devKey,
                    "AppsFlyer Key"
                );
                //Settings.appsFlyer_appID = CreateStringField(Settings.appsFlyer_appID, "AppsFlyer App ID");
            }
        }

        private void GameAnalyticsMenu()
        {
            Settings.newSetting.canUseGameAnalytics = CreateToggleButton(
                Settings.newSetting.canUseGameAnalytics,
                "Use GameAnalytics"
            );

            if (Settings.newSetting.canUseGameAnalytics)
            {
                Settings.newSetting.gameAnalyticsIosGameKey = CreateStringField(
                    Settings.newSetting.gameAnalyticsIosGameKey,
                    "GA GameKey"
                );
                Settings.newSetting.gameAnalyticsIosSecretKey = CreateStringField(
                    Settings.newSetting.gameAnalyticsIosSecretKey,
                    "GA SecretKey"
                );

                //GUILayout.BeginHorizontal();
                //GUILayout.Space(25);
                //GUILayout.Label("Add level info", GUILayout.Width(200));
                //GUILayout.EndHorizontal();
                //ShowLevelInfoList(Settings.newSetting.levelInfos);

                GUILayout.Space(25);
                GUILayout.BeginHorizontal();
                GUILayout.Space(200);

                bool isGAavalible = false;

#if !AC_GAMEANALYTICS


                if (
                    GUILayout.Button(
                        "Setup GameAnalytics ScriptDefine Symbols first ",
                        GUILayout.Width(350)
                    )
                )
                {
                    SetupScriptDefineSymbolsBasedOnActivePlugings(Settings);
                }

                isGAavalible = false;

#else

                isGAavalible = true;

#endif

                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(250);

                GUI.enabled = isGAavalible;

                if (GUILayout.Button("Setup GameAnalytics Setting", GUILayout.Width(250)))
                {
                    Setup_GA_Settings(Settings.newSetting);
                }

                GUI.enabled = true;

                //if (appcentral_ReferenceManager == null)
                //{
                //    if (GUILayout.Button("Setup GameAnalytics ScriptDefine symbol first ", GUILayout.Width(250)))
                //    {
                //        SetupScriptDefineSymbolsBasedOnActivePlugings(Settings);
                //    }

                //}else if (GUILayout.Button("Setup GameAnalytics Setting", GUILayout.Width(250)))
                //{
                //    Setup_GA_Settings(Settings);
                //}


                GUILayout.EndHorizontal();
            }
        }

        private void SmartLookMenu()
        {
            Settings.newSetting.canUseSmartLook = CreateToggleButton(
                Settings.newSetting.canUseSmartLook,
                "Use SmartLook"
            );
            if (Settings.newSetting.canUseSmartLook)
            {
                Settings.newSetting.SmartLookKey = CreateStringField(
                    Settings.newSetting.SmartLookKey,
                    "SmartLook Key"
                );
            }
        }

        private void OneSignalMenu()
        {
            Settings.newSetting.UseOneSignal = CreateToggleButton(
                Settings.newSetting.UseOneSignal,
                "Use OneSignal"
            );
            if (Settings.newSetting.UseOneSignal)
            {
                Settings.newSetting.OneSignalID = CreateStringField(
                    Settings.newSetting.OneSignalID,
                    "OneSignal ID"
                );

                GUILayout.Space(25);
                GUILayout.BeginHorizontal();
                GUILayout.Space(200);
                bool isGAavalible = false;

#if !AC_ONESIGNAL


                if (
                    GUILayout.Button(
                        "Setup OneSignal Script Define Symbols first ",
                        GUILayout.Width(350)
                    )
                )
                {
                    SetupScriptDefineSymbolsBasedOnActivePlugings(Settings);
                }

                isGAavalible = false;

#else

                isGAavalible = true;

#endif

                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(250);

                GUI.enabled = isGAavalible;

                if (GUILayout.Button("Goto OneSignal SDK setup Menu", GUILayout.Width(250)))
                {
                    GotoOnSignalSDKSettings();
                }

                GUI.enabled = true;
                GUILayout.EndHorizontal();
            }
        }

        private void AppLovinMenu()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("", GUILayout.Width(0));
            Settings.newSetting.UseAppLovin = EditorGUILayout.Toggle("", Settings.newSetting.UseAppLovin, GUILayout.Width(15));
            GUILayout.Label("Use AppLovin");
            GUILayout.EndHorizontal();

            if (Settings.newSetting.UseAppLovin)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Space(25);
                GUILayout.Label("AppLovin Project ID", GUILayout.Width(150));
                Settings.newSetting.AppLovinAppID = EditorGUILayout.TextField("", Settings.newSetting.AppLovinAppID);
                GUILayout.EndHorizontal();

                EditorGUILayout.Space(10);

                GUILayout.BeginHorizontal();
                GUILayout.Space(25);
                GUILayout.Label("Banner Position", GUILayout.Width(150));
                Settings.newSetting.BannerPosition = (AppCentralAPI.BannerPosition)EditorGUILayout.EnumPopup("", Settings.newSetting.BannerPosition);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(25);
                GUILayout.Label("Banner Ad Id", GUILayout.Width(150));
                Settings.newSetting.BannerAdId = EditorGUILayout.TextField("", Settings.newSetting.BannerAdId);
                GUILayout.EndHorizontal();

                EditorGUILayout.Space(10);

                GUILayout.BeginHorizontal();
                GUILayout.Space(25);
                GUILayout.Label("AppOpen Ad ID", GUILayout.Width(150));
                Settings.newSetting.AppOpenAdId = EditorGUILayout.TextField("", Settings.newSetting.AppOpenAdId);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(25);
                GUILayout.Label("Interstital Ad ID", GUILayout.Width(150));
                Settings.newSetting.InterstitailAdId = EditorGUILayout.TextField("", Settings.newSetting.InterstitailAdId);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(25);
                GUILayout.Label("Rewarded Ad Id", GUILayout.Width(150));
                Settings.newSetting.RewardedAdId = EditorGUILayout.TextField("", Settings.newSetting.RewardedAdId);
                GUILayout.EndHorizontal();

                GUILayout.Space(25);
                GUILayout.BeginHorizontal();
                GUILayout.Space(200);
                //GUILayout.Label("Setup AppLovin Mediation Adapters ", GUILayout.Width(150));



                bool isAppLovInavalible = false;

                //

#if !AC_APPLOVIN


                if (
                    GUILayout.Button(
                        "Setup AppLovin Script Define Symbols first ",
                        GUILayout.Width(350)
                    )
                )
                {
                    SetupScriptDefineSymbolsBasedOnActivePlugings(Settings);
                }

                isAppLovInavalible = false;

#else

                isAppLovInavalible = true;

#endif

                GUILayout.EndHorizontal();

                /* Disabled as AC no longer want this feature
                 
                GUILayout.BeginHorizontal();
                GUILayout.Space(50);
                if (GUILayout.Button("check for change in networks", GUILayout.Width(250)))
                {
                    PluginData pluginData = FileHandler.ReadFromJSON<PluginData>("Assets/AppCentral_API/AppCentralAdsAPI/Editor/unity_integration_manager.json");

                    for (int i = 0; i < pluginData.MediatedNetworks.Length; i++)
                    {
                        if (Settings.newSetting.maxAdapters.Count <= i)
                        {
                            Settings.newSetting.maxAdapters.Add(new MaxAdapter());
                        }
                        Settings.newSetting.maxAdapters[i].network = pluginData.MediatedNetworks[i];
                    }
                }
                GUILayout.EndHorizontal();
                GUILayout.Space(15);




                foreach (var item in Settings.newSetting.maxAdapters)
                {

                    GUILayout.BeginHorizontal();
                    GUILayout.Space(50);
                    item.IsEnable = EditorGUILayout.Toggle("",item.IsEnable,GUILayout.Width(15));
                    GUILayout.Space(5);

                    GUILayout.Label(item.network.DisplayName);
                    GUILayout.Space(25);

                    GUILayout.Label(item.network.LatestVersions.Unity);

                    GUILayout.EndHorizontal();

                }
                

                EditorGUILayout.Space(10);


                GUILayout.BeginHorizontal();
                GUILayout.Space(250);
                if (GUILayout.Button("Update all Mediation network changes", GUILayout.Width(250)))
                {
                    _appLovinPreBuilder.UpdateAllMediationNetworksToDefaultVersions(Settings.newSetting.maxAdapters);
                }
                GUILayout.EndHorizontal();

                */

                GUILayout.BeginHorizontal();
                GUILayout.Space(250);


                GUI.enabled = isAppLovInavalible;

                if (GUILayout.Button("Setup AppLovin Mediation Adapters", GUILayout.Width(250)))
                {
                    SetUpAppLovinSettings();
                }

                GUILayout.EndHorizontal();


                GUI.enabled = true;

            }
        }

        private void InAppMenu()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("", GUILayout.Width(0));
            Settings.newSetting.UseInApps = EditorGUILayout.Toggle(
                "",
                Settings.newSetting.UseInApps,
                GUILayout.Width(15)
            );
            GUILayout.Label("Use InApps Subscription");
            GUILayout.EndHorizontal();

            if (Settings.newSetting.UseInApps)
            {
                GUILayout.Space(25);

                GUILayout.BeginHorizontal();
                GUILayout.Space(25);
                //GUILayout.Label("VideoClip", GUILayout.Width(100), GUILayout.Height(64));
                GUILayout.Label("VideoClip", GUILayout.Width(225));
                Settings.newSetting.videoClip = (VideoClip)
                    EditorGUILayout.ObjectField(
                        "",
                        Settings.newSetting.videoClip,
                        typeof(VideoClip),
                        false
                    );
                GUILayout.EndHorizontal();

                GUILayout.Space(25);

                GUILayout.BeginHorizontal();
                GUILayout.Space(25);
                GUILayout.Label("AC_AllGamesAdFreeIAP_ProductID", GUILayout.Width(225));
                Settings.newSetting.AC_AllGamesAdFreeInAppID = EditorGUILayout.TextField(
                    "",
                    Settings.newSetting.AC_AllGamesAdFreeInAppID
                );
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(25);
                GUILayout.Label("AC_AllBundlesAdFreeIAP_ProductID", GUILayout.Width(225));
                Settings.newSetting.AC_AllBundleAdfreeInAppID = EditorGUILayout.TextField(
                    "",
                    Settings.newSetting.AC_AllBundleAdfreeInAppID
                );
                GUILayout.EndHorizontal();

                GUILayout.Space(25);

                //GUILayout.BeginHorizontal();
                //GUILayout.Space(25);
                //GUILayout.Label("Dynamic InApp ProductID", GUILayout.Width(200));
                //GUILayout.EndHorizontal();
                //ShowList(Settings.newSetting.DynamicSubscriptionInAppID);

                //GUILayout.Space(10);

                //GUILayout.BeginHorizontal();
                //GUILayout.Space(25);
                //GUILayout.Label("MidGame InApp ProductID", GUILayout.Width(200));
                //GUILayout.EndHorizontal();
                //ShowList(Settings.newSetting.MidGameSubscriptionInAppID);
            }
        }

        private void ShowList(List<string> InAppIds)
        {
            List<int> c1ToRemove = new List<int>();
            for (int i = 0; i < InAppIds.Count; i++)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label("", GUILayout.Width(21));
                GUILayout.Label("-", GUILayout.Width(10));

                InAppIds[i] = (EditorGUILayout.TextField(InAppIds[i]));

                if (
                    GUILayout.Button(
                        "DELETE",
                        new GUILayoutOption[] { GUILayout.Width(60), GUILayout.Height(20) }
                    )
                )
                {
                    c1ToRemove.Add(i);
                }
                EditorGUIUtility.AddCursorRect(GUILayoutUtility.GetLastRect(), MouseCursor.Link);
                GUILayout.EndHorizontal();
                GUILayout.Space(2);
            }

            foreach (int i in c1ToRemove)
            {
                InAppIds.RemoveAt(i);
            }

            GUILayout.BeginHorizontal();
            GUILayout.Label("", GUILayout.Width(21));
            if (GUILayout.Button("Add", GUILayout.Width(63)))
            {
                if (InAppIds.Count < 20)
                {
                    //InAppIds.Add("New InApp ID (" + (InAppIds.Count + 1) + ")");
                    InAppIds.Add("");
                }
            }
            GUILayout.EndHorizontal();
        }

        private void ShowArray(List<MaxAdapter> InAppIds)
        {
            // List<int> c1ToRemove = new List<int>();
            // for (int i = 0; i < InAppIds.Count; i++)
            // {
            //     GUILayout.BeginHorizontal();
            //     GUILayout.Label("", GUILayout.Width(21));
            //     GUILayout.Label("-", GUILayout.Width(10));
            //
            //     InAppIds[i] = (EditorGUILayout.TextField(InAppIds[i]));
            //
            //     if (
            //         GUILayout.Button(
            //             "DELETE",
            //             new GUILayoutOption[] { GUILayout.Width(60), GUILayout.Height(20) }
            //             )
            //     )
            //     {
            //         c1ToRemove.Add(i);
            //     }
            //     EditorGUIUtility.AddCursorRect(GUILayoutUtility.GetLastRect(), MouseCursor.Link);
            //     GUILayout.EndHorizontal();
            //     GUILayout.Space(2);
            // }
            //
            // foreach (int i in c1ToRemove)
            // {
            //     InAppIds.RemoveAt(i);
            // }
            //
            // GUILayout.BeginHorizontal();
            // GUILayout.Label("", GUILayout.Width(21));
            // if (GUILayout.Button("Add", GUILayout.Width(63)))
            // {
            //     if (InAppIds.Count < 20)
            //     {
            //         //InAppIds.Add("New InApp ID (" + (InAppIds.Count + 1) + ")");
            //         InAppIds.Add("");
            //     }
            // }
            // GUILayout.EndHorizontal();
        }

        private void ShowLevelInfoList(List<LevelInfo> levelInfos)
        {
            List<int> itemsToRemove = new List<int>();

            for (int i = 0; i < levelInfos.Count; i++)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label("", GUILayout.Width(21));
                GUILayout.Label("-", GUILayout.Width(10));

                // Display LevelType and ModeType fields as popup menus
                levelInfos[i].LevelType = (Progresion01)EditorGUILayout.EnumPopup(levelInfos[i].LevelType);
                levelInfos[i].ModeType = (Progresion02)EditorGUILayout.EnumPopup(levelInfos[i].ModeType);

                // Display Level field as an integer input field
                levelInfos[i].Level[0] = EditorGUILayout.IntField(levelInfos[i].Level[0]);

                if (
                    GUILayout.Button(
                        "DELETE",
                        new GUILayoutOption[] { GUILayout.Width(60), GUILayout.Height(20) }
                    )
                )
                {
                    itemsToRemove.Add(i);
                }
                EditorGUIUtility.AddCursorRect(GUILayoutUtility.GetLastRect(), MouseCursor.Link);
                GUILayout.EndHorizontal();
                GUILayout.Space(2);
            }

            // Remove items marked for deletion
            foreach (int i in itemsToRemove)
            {
                levelInfos.RemoveAt(i);
            }

            GUILayout.BeginHorizontal();
            GUILayout.Label("", GUILayout.Width(21));
            if (GUILayout.Button("Add", GUILayout.Width(63)))
            {
                if (levelInfos.Count < 20)
                {
                    // Add a new LevelInfo object to the list
                    LevelInfo newLevelInfo = new LevelInfo();
                    newLevelInfo.LevelType = Progresion01.level;
                    newLevelInfo.ModeType = Progresion02.@default;
                    newLevelInfo.Level = new int[1] { 0 };
                    levelInfos.Add(newLevelInfo);
                }
            }
            GUILayout.EndHorizontal();
        }



        private void SetupURLScheme()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("", GUILayout.Width(0));
            Settings.newSetting.Set_URL_scheme = EditorGUILayout.Toggle(
                "",
                Settings.newSetting.Set_URL_scheme,
                GUILayout.Width(15)
            );
            GUILayout.Label("Set URL scheme");
            GUILayout.EndHorizontal();

            if (Settings.newSetting.Set_URL_scheme)
            {
                GUILayout.Space(10);
                GUILayout.BeginHorizontal();

                if (GUILayout.Button("Click to setup URL Scheme", GUILayout.Width(200)))
                {
                    SetUpURlScheme(Settings);
                }

                GUILayout.Space(15);

                GUI.enabled = false;
                if (!string.IsNullOrEmpty(Settings.newSetting.CurrentURl_Scheme))
                {
                    EditorGUILayout.TextField(
                        "",
                        Settings.newSetting.CurrentURl_Scheme,
                        GUILayout.Width(300)
                    );
                }
                else
                {
                    EditorGUILayout.TextField("", "No URL scheme has been set");
                }
                GUI.enabled = true;

                GUILayout.EndHorizontal();
            }
        }

        private void ShowHideVersionInfo(bool CanShow)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("", GUILayout.Width(0));
            Settings.newSetting.Set_URL_scheme = EditorGUILayout.Toggle(
                "",
                Settings.newSetting.Set_URL_scheme,
                GUILayout.Width(15)
            );
            GUILayout.Label("Set URL scheme");
            GUILayout.EndHorizontal();

            if (Settings.newSetting.Set_URL_scheme)
            {
                GUILayout.Space(10);
                GUILayout.BeginHorizontal();

                if (GUILayout.Button("Click to setup URL Scheme", GUILayout.Width(200)))
                {
                    SetUpURlScheme(Settings);
                }

                GUILayout.Space(15);

                GUI.enabled = false;
                if (!string.IsNullOrEmpty(Settings.newSetting.CurrentURl_Scheme))
                {
                    EditorGUILayout.TextField(
                        "",
                        Settings.newSetting.CurrentURl_Scheme,
                        GUILayout.Width(300)
                    );
                }
                else
                {
                    EditorGUILayout.TextField("", "No URL scheme has been set");
                }
                GUI.enabled = true;

                GUILayout.EndHorizontal();
            }
        }

        private static void SetUpAppLovinSettings()
        {
#if AC_APPLOVIN

            Console.Clear();

            AppLovinSettings.Instance.ConsentFlowEnabled = false;
            AppLovinSettings.Instance.QualityServiceEnabled = false;

            AppLovinIntegrationManagerWindow window = (AppLovinIntegrationManagerWindow)
                EditorWindow.GetWindow(
                    typeof(AppLovinIntegrationManagerWindow),
                    false,
                    "Gib Halp Plis"
                );
            window.Show();

#endif
        }

        private void GotoOnSignalSDKSettings()
        {
#if AC_ONESIGNAL

            Console.Clear();

            //OneSignalSDK.OneSignalSetupWindow window = (OneSignalSDK.OneSignalSetupWindow)EditorWindow.GetWindow(typeof(OneSignalSDK.OneSignalSetupWindow), false, "Gib Halp Plis");
            //window.Show();

            EditorApplication.ExecuteMenuItem("Window/OneSignal SDK Setup");
            //OneSignalSDK.OneSignalSetupWindow.ShowWindow();
#endif
        }

        private static void SetUpURlScheme(AppCentralSettings setting)
        {
            Console.Clear();
            if (string.IsNullOrEmpty(setting.newSetting.BundleID))
            {
                setting.newSetting.CurrentURl_Scheme = "";
                URLSchemePreBuilder.ClearAllurlSchemes();
                ACLogger.UserError("Please SetUp The Bundle ID First In the AppCentral Settings");
            }
            else
            {
                URLSchemePreBuilder.BuildAppCentralURLScheme(setting.newSetting.BundleID);
                setting.newSetting.CurrentURl_Scheme = URLSchemePreBuilder.GetAppCentralURLScheme();
            }
        }

        private static void Setup_GA_Settings(newSetting settings)
        {
#if AC_GAMEANALYTICS

            Console.Clear();
            GameAnalyticsPreBuild.CheckAndUpdateGameAnalyticsSettings(settings);
            appcentral_ReferenceManager =
                GameAnalyticsPreBuild.AddGAPrefebToAppCentralRefrenceManager(settings);
#else
            ACLogger.UserError(
                ": FAILED to set the GameAnalytics Settings"
                    + "/n"
                    + "Please check if you have GameAnalytics SDK avalible in you project or not"
            );
#endif
        }

        private static void SetupScriptDefineSymbolsBasedOnActivePlugings(AppCentralSettings settings)
        {
            Console.Clear();
            AppCentralCore.ScriptableDefineSymbolsPrebuilder.SetSDS(settings.newSetting);
        }

        public void OnPreprocessBuild(BuildReport report) { }

        private static void Splitter(float thickness = 1, int margin = 10)
        {
            EditorGUILayout.Space(10);

            Color rgb = new Color(0.35f, 0.35f, 0.35f);

            GUIStyle splitter = new GUIStyle();
            splitter.normal.background = EditorGUIUtility.whiteTexture;
            splitter.stretchWidth = true;
            splitter.margin = new RectOffset(margin, margin, 7, 7);

            Rect position = GUILayoutUtility.GetRect(
                GUIContent.none,
                splitter,
                GUILayout.Height(thickness)
            );

            if (Event.current.type == EventType.Repaint)
            {
                Color restoreColor = GUI.color;
                GUI.color = rgb;
                splitter.Draw(position, false, false, false, false);
                GUI.color = restoreColor;
            }

            EditorGUILayout.Space(10);
        }

        private void DrawHeaders(string firstColumnTitle, bool drawAction)
        {
            using (new EditorGUILayout.HorizontalScope())
            {
                GUILayout.Space(5);
                EditorGUILayout.LabelField(firstColumnTitle, headerLabelStyle, networkWidthOption);
                EditorGUILayout.LabelField("Current Version", headerLabelStyle, versionWidthOption);
                GUILayout.Space(3);
            }

            GUILayout.Space(4);
        }

        private void DrawPluginDetailRow(string platform, string currentVersion)
        {
            using (new EditorGUILayout.HorizontalScope())
            {
                GUILayout.Space(5);
                EditorGUILayout.LabelField(new GUIContent(platform), networkWidthOption);
                GUILayout.Space(30);
                EditorGUILayout.LabelField(new GUIContent(currentVersion), versionWidthOption);
            }

            GUILayout.Space(4);
        }

        private bool CreateToggleButton(bool toggleProperty, string lableName, int layoutWidth = 15)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("", GUILayout.Width(0));
            toggleProperty = EditorGUILayout.Toggle("", toggleProperty, GUILayout.Width(15));
            GUILayout.Label(lableName);
            GUILayout.EndHorizontal();

            return toggleProperty;
        }

        private string CreateStringField(string StringProperty,string lableName,int layoutWidth = 150)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(25);
            GUILayout.Label(lableName, GUILayout.Width(layoutWidth));
            StringProperty = EditorGUILayout.TextField("", StringProperty);
            GUILayout.EndHorizontal();

            return StringProperty;
        }

        void RefreshEditorProjectWindow()
        {
#if UNITY_EDITOR
            UnityEditor.AssetDatabase.Refresh();
#endif
        }
    }
}
