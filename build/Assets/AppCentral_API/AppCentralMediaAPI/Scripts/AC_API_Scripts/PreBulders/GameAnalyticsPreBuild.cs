using UnityEngine;
#if UNITY_EDITOR
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using System;
using UnityEditor;


namespace AppCentralCore
{
    public class GameAnalyticsPreBuild : IPreprocessBuildWithReport
    {
       

        public int callbackOrder => 1;

        public void OnPreprocessBuild(BuildReport report)
        {
#if AC_GAMEANALYTICS

            CheckAndUpdateGameAnalyticsSettings(AppCentralSettings.LoadSetting());
#endif
        }

#if AC_GAMEANALYTICS

        public static void CheckAndUpdateGameAnalyticsSettings(newSetting settings)
        {

            if (!settings.canUseGameAnalytics) return;

#if UNITY_EDITOR
            if (settings == null || !CheckGameAnalyticsSettings(settings.gameAnalyticsIosGameKey, settings.gameAnalyticsIosSecretKey, RuntimePlatform.IPhonePlayer))
            {
                ACLogger.UserError("Failed to set up GameAnalytics Key, Please try again or set them manually in the GameAnalytics Setting");
            }
            else
            {
                ACLogger.UserDebug("GameAnalytics SDK keys setup Successfully");
            }
#endif
        }

        private static bool CheckGameAnalyticsSettings(string gameKey, string secretKey, RuntimePlatform platform)
        {

            if (string.IsNullOrEmpty(gameKey) || string.IsNullOrEmpty(secretKey))
                return false;

            if (gameKey.ToLower() == "ignore" && secretKey.ToLower() == "ignore")
                return true;

            if (!GameAnalyticsSDK.GameAnalytics.SettingsGA.Platforms.Contains(platform))
                GameAnalyticsSDK.GameAnalytics.SettingsGA.AddPlatform(platform);

            int platformIndex = GameAnalyticsSDK.GameAnalytics.SettingsGA.Platforms.IndexOf(platform);
            GameAnalyticsSDK.GameAnalytics.SettingsGA.UpdateGameKey(platformIndex, gameKey);
            GameAnalyticsSDK.GameAnalytics.SettingsGA.UpdateSecretKey(platformIndex, secretKey);


            string versionNum = Application.version;
#if UNITY_IOS
            versionNum = versionNum + "." + PlayerSettings.iOS.buildNumber;
#elif UNITY_ANDROID
            versionNum = versionNum + "." + PlayerSettings.Android.bundleVersionCode.ToString();
#endif

            GameAnalyticsSDK.GameAnalytics.SettingsGA.Build[platformIndex] = versionNum;

            GameAnalyticsSDK.GameAnalytics.SettingsGA.InfoLogBuild = false;
            GameAnalyticsSDK.GameAnalytics.SettingsGA.InfoLogEditor = false;


            GameAnalyticsSDK.GameAnalytics.SettingsGA.CustomDimensions01.Clear();
            GameAnalyticsSDK.GameAnalytics.SettingsGA.CustomDimensions02.Clear();

            GameAnalyticsSDK.GameAnalytics.SettingsGA.CustomDimensions01.Add("theme1");
            GameAnalyticsSDK.GameAnalytics.SettingsGA.CustomDimensions01.Add("theme2");

            GameAnalyticsSDK.GameAnalytics.SettingsGA.CustomDimensions02.Add("notifications.Authorized");
            GameAnalyticsSDK.GameAnalytics.SettingsGA.CustomDimensions02.Add("notifications.Denied");
            GameAnalyticsSDK.GameAnalytics.SettingsGA.CustomDimensions02.Add("notifications.NotDetermined");
            GameAnalyticsSDK.GameAnalytics.SettingsGA.CustomDimensions02.Add("notifications.Provisional");
            GameAnalyticsSDK.GameAnalytics.SettingsGA.CustomDimensions02.Add("notifications.Ephemeral");

            EditorUtility.SetDirty(GameAnalyticsSDK.GameAnalytics.SettingsGA);

            return true;
        }


        public static Appcentral_ReferenceManager AddGAPrefebToAppCentralRefrenceManager(newSetting settings)
        {
            GameObject go = AssetDatabase.LoadAssetAtPath(GameAnalyticsSDK.GameAnalytics.WhereIs("GameAnalytics.prefab", "Prefab"), typeof(GameObject)) as GameObject;
            string path = AppCentralSettings.localFolderPath_EDITOR_Testing + "AC_ReferenceManagers.asset";
            Appcentral_ReferenceManager appcentral_ReferenceManager = AssetDatabase.LoadAssetAtPath(path, typeof(Appcentral_ReferenceManager)) as Appcentral_ReferenceManager;

            if (appcentral_ReferenceManager)
            {
                if (go != null)
                {
                    appcentral_ReferenceManager.GameAnalytics_Prefeb = go;
                    ACLogger.UserDebug(": Successfully set GameAnalytics Prefeb to Appcentral_ReferenceManager class");
                }
                else
                {
                    ACLogger.UserError(": Failed to find GameAnalytics Prefeb, Please aasign it manually");
                }
            }
            else
            {
                ACLogger.UserError(": Failed to set GameAnalytics Prefeb to Appcentral_ReferenceManager class, Please assign it manually");
            }

            EditorUtility.SetDirty(appcentral_ReferenceManager);

            return appcentral_ReferenceManager;
        }


        /// <summary>
        /// Dynamic search for a file.
        /// </summary>
        /// <returns>Returns the Unity path to a specified file.</returns>
        /// <param name="">File name including extension e.g. image.png</param>
        /// 


        public static T[] GetAllInstances<T>(string filename) where T : ScriptableObject
        {
            string[] guids = AssetDatabase.FindAssets("t:" + typeof(T).Name);  //FindAssets uses tags check documentation for more info
            T[] a = new T[guids.Length];
            for (int i = 0; i < guids.Length; i++)         //probably could get optimized 
            {

                string path = AssetDatabase.GUIDToAssetPath(guids[i]);

                ACLogger.UserDebug("guids=" + path);

                if (path.Contains(filename))
                    a[i] = AssetDatabase.LoadAssetAtPath<T>(path);
            }

            return a;

        }

        public static string WhereIs(string _file, string _type)
        {
#if UNITY_SAMSUNGTV
            return "";
#else
            string[] guids = AssetDatabase.FindAssets("t:" + _type);
            foreach (string g in guids)
            {
                string p = AssetDatabase.GUIDToAssetPath(g);
                if (p.EndsWith(_file))
                {
                    return p;
                }
            }
            return "";
#endif
        }


#endif
    }
}
#endif
