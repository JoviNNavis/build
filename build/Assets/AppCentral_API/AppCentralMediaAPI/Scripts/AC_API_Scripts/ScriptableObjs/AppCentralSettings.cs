using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;
using UnityEngine.Video;
using AppCentralAPI;

#if AC_APPLOVIN && UNITY_EDITOR
using AppLovinMax.Scripts.IntegrationManager.Editor;
using Network = AppLovinMax.Scripts.IntegrationManager.Editor.Network;
#endif

namespace AppCentralCore
{
    [CreateAssetMenu(
        fileName = "Assets/Resources/AppCentral/AppCentralSDKExtentionSettings",
        menuName = "AppCentralSDK/AppCentralSDKExtention Settings file"
    )]
    public class AppCentralSettings : ScriptableObject
    {
        public const string localFolderPath_EDITOR_Testing =
            "Assets/AppCentral_API/AppCentralMediaAPI/Scripts/AC_API_Scripts/Settings/";

        private const string SETTING_RESOURCES_PATH = "AppCentral/AppCentral SDK Setting";

        public newSetting newSetting = new newSetting();

        [SerializeField]
        public bool isSDKPhase1FoldOut = true;

        [SerializeField]
        public bool isSDKPhase2FoldOut = true;

        public static AppCentralSettings Load() =>
            Resources.Load<AppCentralSettings>(SETTING_RESOURCES_PATH);

        public static newSetting LoadSetting() =>
            Resources.Load<AppCentralSettings>(SETTING_RESOURCES_PATH).newSetting;
    }

    [System.Serializable]
    public class newSetting
    {
        [SerializeField]
        public bool ShowVersionInfo = false;

        [SerializeField]
        public bool ShowLogs = true;

        [SerializeField]
        public bool UseAppCentralSDKPhase2 = false;

        [Tooltip("AppCentral SDKExtention Setting")]
        [SerializeField]
        public Sprite GameIcon;

        [SerializeField]
        public string GameName;

        [SerializeField]
        public string BundleID;

        [SerializeField]
        public string IOSAppID;

        [SerializeField]
        public bool OverRide_URLTermsOfConditions;

        [SerializeField]
        public string URLTermsOfConditions;

        [SerializeField]
        public bool OverRide_URLPrivacyPolicy;

        [SerializeField]
        public string URLPrivacyPolicy;

        [SerializeField]
        public bool canUseAppsFlyer = true;

        [SerializeField]
        public string appsFlyer_devKey;

        [SerializeField]
        public string appsFlyer_appID;

        [SerializeField]
        public bool canUseGameAnalytics = false;

        [SerializeField]
        public string gameAnalyticsIosGameKey;

        [SerializeField]
        public string gameAnalyticsIosSecretKey;

        [SerializeField]
        public List<LevelInfo> levelInfos;

        [SerializeField]
        public bool canUseSmartLook = false;

        [SerializeField]
        public string SmartLookKey;

        [SerializeField]
        public bool UseRateUS = false;

        [SerializeField]
        public bool UseFlightModePrompt = false;

        [SerializeField]
        public bool Set_URL_scheme = false;

        [SerializeField]
        public string CurrentURl_Scheme = "";

        [Tooltip("Set OneSignal Setting")]
        [SerializeField]
        public bool UseOneSignal = false;

        [SerializeField]
        public string OneSignalID;

        [Tooltip("Set AppLovin IDs")]
        [SerializeField]
        public bool UseAppLovin = false;

        [SerializeField]
        public string AppLovinAppID;

        [SerializeField]
        public string BannerAdId;

        [SerializeField]
        public AppCentralAPI.BannerPosition BannerPosition;

        [SerializeField]
        public string AppOpenAdId;

        [SerializeField]
        public string InterstitailAdId;

        [SerializeField]
        public string RewardedAdId;

        [SerializeField]
        public List<MaxAdapter> maxAdapters = new List<MaxAdapter>();

        [Tooltip("Set InApps IDs")]
        [SerializeField]
        public bool UseInApps = false;

        [SerializeField]
        public VideoClip videoClip;

        [SerializeField]
        public string AC_AllGamesAdFreeInAppID;

        [SerializeField]
        public string AC_AllBundleAdfreeInAppID;

        [SerializeField]
        public List<string> DynamicSubscriptionInAppID = new List<string>();

        [SerializeField]
        public List<string> MidGameSubscriptionInAppID = new List<string>();
    }

    [System.Serializable]
    public class MaxAdapter
    {
        [SerializeField] public bool IsEnable = false;

#if AC_APPLOVIN && UNITY_EDITOR
        [SerializeField] public Network network;
#endif
    }

    [System.Serializable]
    public class LevelInfo
    {
        public Progresion01 LevelType;
        public Progresion02 ModeType;
        public int[] Level;
    }


}
