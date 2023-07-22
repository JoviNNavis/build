using System;
using UnityEngine;
using AppCentralAPI;
using System.Collections;

namespace GH
{
    public class ApplovinAdManager : MonoBehaviour
    {
        public static ApplovinAdManager Instance;
        private static AppLovinAdSetting appLovinAdSetting = new AppLovinAdSetting();

        public static bool IsBannerLoaded = false;

        void Awake()
        {
            if (Instance == null)
                Instance = this;
        }
        [SerializeField]
        private AL_BannerImpl aL_Banner;

        [SerializeField]
        private AL_InterstitialImpl aL_Interstitial;

        [SerializeField]
        public AL_AppOpenImpl aL_AppOpenImpl;

        [SerializeField]
        private AL_RewardedImpl aL_Rewarded;

#if AC_APPLOVIN

        public bool IsRewardedLoaded
        {
            get => aL_Rewarded.HasRewardedAd();
        }

        public bool IsInterstitialLoaded
        {
            get => aL_Interstitial.HasInterstitalAd();
        }

        void Start()
        {
            StartCoroutine(ContiniouslyCheckForBannerAd());
        }

        private void OnEnable()
        {
            AppCentralInAppPurchaser.OnUserSubscibedSuccessfully += OnSubscriptionStatusChange;
            UI_PayWall.PaywallOpen += PayWallOpen;
            UI_PayWall.PaywallClose += PayWallClosed;
        }

        private void OnDisable()
        {
            AppCentralInAppPurchaser.OnUserSubscibedSuccessfully -= OnSubscriptionStatusChange;
            UI_PayWall.PaywallOpen -= PayWallOpen;
            UI_PayWall.PaywallClose -= PayWallClosed;
        }

        private void OnSubscriptionStatusChange()
        {
            HideBanner();
        }

    
        public void InitializeAppLovin()
        {
            ACLogger.UserDebug(": InitializeAppLovin is Called");

            // aL_Banner = transform.GetComponent<AL_BannerImpl>();
            // aL_Interstitial = transform.GetComponent<AL_InterstitialImpl>();
            // aL_Rewarded = transform.GetComponent<AL_RewardedImpl>();

            AppCentralCore.newSetting setting = AppCentralCore.AppCentralSettings.LoadSetting();

            if (!setting.UseAppLovin)
            {
                ACLogger.UserDebug(": InitializeAppLovin is Disabled from AppCentral Settings");
                return;
            }

            ACLogger.UserDebug(
                ": InitializeAppLovin is Enable from AppCentral Settings with SDK keys="
                    + setting.AppLovinAppID
            );
            ACLogger.UserDebug(
                ": InitializeAppLovin is Enable from AppCentral Settings with Banner ID ="
                    + setting.BannerAdId
            );
            ACLogger.UserDebug(": InitializeAppLovin is Enable from AppCentral Settings with Interstitial ID="+ setting.InterstitailAdId);
            ACLogger.UserDebug(": InitializeAppLovin is Enable from AppCentral Settings with AppOpen Ad ID="+ setting.AppOpenAdId);
            ACLogger.UserDebug(": InitializeAppLovin is Enable from AppCentral Settings with Interstitial ID="+ setting.InterstitailAdId);
            ACLogger.UserDebug(": InitializeAppLovin is Enable from AppCentral Settings with Rewarded ID=" + setting.RewardedAdId);

            appLovinAdSetting.LovinID = setting.AppLovinAppID;

            appLovinAdSetting.BannerAdUnitId = setting.BannerAdId;
            appLovinAdSetting.MaxBannerPosition = (MaxSdk.BannerPosition)setting.BannerPosition;
            appLovinAdSetting.LovinAppOpenId = setting.AppOpenAdId;
            appLovinAdSetting.LovinInter = setting.InterstitailAdId;
            appLovinAdSetting.LovinRewarded = setting.RewardedAdId;

            aL_Banner.BannerImpl(appLovinAdSetting.BannerAdUnitId,appLovinAdSetting.MaxBannerPosition);
            aL_AppOpenImpl.AppOpenImpl(appLovinAdSetting.LovinAppOpenId);
            aL_Interstitial.InterstitialImpl(appLovinAdSetting.LovinInter);
            aL_Rewarded.RewardedImpl(appLovinAdSetting.LovinRewarded);

            aL_AppOpenImpl.InitializeAppOpenAds();

            MaxSdkCallbacks.OnSdkInitializedEvent += sdkConfiguration =>
            {
                // AppLovin SDK is initialized, configure and start loading ads.
                ACLogger.UserDebug(": MAX SDK Initialized");

                aL_Interstitial.InitializeInterstitialAds();
                aL_Rewarded.InitializeRewardedAds();
                aL_Banner.InitializeBannerAds();
            };

            MaxSdk.SetSdkKey(appLovinAdSetting.LovinID);
            MaxSdk.InitializeSdk();
        }

        public void ShowBanner()
        {
            if (!MaxSdk.IsInitialized())
                return;

            if (!IsSubscriptionActive())
                aL_Banner.ShowBanner();
        }

        public void HideBanner()
        {
            if (!MaxSdk.IsInitialized())
                return;

            aL_Banner.HideBanner();
        }

        public void ShowInterstitial()
        {
            if (!MaxSdk.IsInitialized())
                return;

            if (!IsSubscriptionActive())
                aL_Interstitial.ShowInterstitialAd();
        }

        public void ShowInterstitial(Action adFinishedCallback)
        {
            if (
                !MaxSdk.IsInitialized()
                || Application.internetReachability == NetworkReachability.NotReachable
            )
            {
                adFinishedCallback?.Invoke();
                return;
            }

            if (!IsSubscriptionActive())
                aL_Interstitial.ShowInterstitialAd(adFinishedCallback);
            else
            {
                adFinishedCallback?.Invoke();
            }
        }

        public void ShowRewardedAd(Action<bool> IsRewardReceived)
        {
            if (!MaxSdk.IsInitialized())
            {
                IsRewardReceived(false); // this callback was miising before version 0.1.8
                PresistanceCanvas.GH_WarningManager.Instance.Prompt("Ads not initialized.");
                return;
            }

            aL_Rewarded.ShowRewardedAd(IsRewardReceived);
        }

        private bool IsSubscriptionActive()
        {
            return AppCentralInAppPurchaser.IsSubscriptionActive();
        }
#endif

        public bool isPaywallOpened = false;

        IEnumerator ContiniouslyCheckForBannerAd()
        {
#if AC_APPLOVIN
            while (true)
            {
                if (isPaywallOpened)
                {
                    HideBanner();
                }
                yield return new WaitForSeconds(1);
            }
#else
            yield return null;
#endif
        }

        private void PayWallOpen(AppCentralAPI.PAYWALL_TYPE pAYWALL_TYPE)
        {
            isPaywallOpened = true;

            if (pAYWALL_TYPE == PAYWALL_TYPE.popup)
                isPaywallOpened = false;
        }

        private void PayWallClosed(AppCentralAPI.PAYWALL_TYPE pAYWALL_TYPE)
        {
            isPaywallOpened = false;

            // if (pAYWALL_TYPE == PAYWALL_TYPE.popup)
            //     isPaywallOpened = true;
        }

        public static void Show_DebugPanel()
        {
#if AC_APPLOVIN

            if (!MaxSdk.IsInitialized())
                return;

            MaxSdk.ShowMediationDebugger();
#endif
        }
    }

    [System.Serializable]
    public class AppLovinAdSetting
    {
        public bool IsAppLovinAdsDisabled = true;

#if AC_APPLOVIN

        [Space(5)]
        public MaxSdk.BannerPosition MaxBannerPosition;
#endif

        [Space(5)]
        public string LovinID = "";
        public string BannerAdUnitId;
        public string LovinAppOpenId;
        public string LovinInter;
        public string LovinRewarded;
    }
}
