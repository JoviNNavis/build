using System;
using UnityEngine;

namespace GH
{
    public class AL_BannerImpl : MonoBehaviour
    {
#if AC_APPLOVIN

        [SerializeField]
        string adUnitId = "";
        MaxSdk.BannerPosition bannerPosition;

        public void BannerImpl(string AdID, MaxSdk.BannerPosition _bannerPosition)
        {
            adUnitId = AdID;
            bannerPosition = _bannerPosition;
        }

        private int interstitialRetryAttempt;

        #region Banner Ad Methods


        private bool isBannerShowing;
        private bool IsBannerLoaded;
        private bool IsSubscribedToEvents = false;

        public void InitializeBannerAds()
        {
            //if (PlayerPrefs.GetInt("RemoveAds", 0) == 0)
            {
                MaxSdk.CreateBanner(adUnitId, bannerPosition);
                MaxSdk.SetBannerExtraParameter(adUnitId, "adaptive_banner", "true");

                Color banner_BG = new Color(1, 1, 1, 0);

                // banner_BG = Color.red;

                MaxSdk.SetBannerBackgroundColor(adUnitId, banner_BG);

                if (!IsSubscribedToEvents)
                {
                    MaxSdkCallbacks.Banner.OnAdLoadedEvent += OnBannerAdLoadedEvent;
                    MaxSdkCallbacks.Banner.OnAdLoadFailedEvent += OnBannerAdFailedEvent;
                    MaxSdkCallbacks.Banner.OnAdClickedEvent += OnBannerAdClickedEvent;
                    MaxSdkCallbacks.Banner.OnAdRevenuePaidEvent += OnBannerAdRevenuePaidEvent;

                    MaxSdkCallbacks.Banner.OnAdLoadedEvent += SendOnBannerAdLoadedPixel;
                    MaxSdkCallbacks.Banner.OnAdLoadFailedEvent += SendOnBannerAdFailedPixel;
                    MaxSdkCallbacks.Banner.OnAdClickedEvent += SendOnBannerAdClickedPixel;
                    MaxSdkCallbacks.Banner.OnAdRevenuePaidEvent += SendOnBannerAdRevenuePaidPixel;

                    IsSubscribedToEvents = true;
                }
                // Banners are automatically sized to 320x50 on phones and 728x90 on tablets.
                // You may use the utility method `MaxSdkUtils.isTablet()` to help with view sizing adjustments.

                // Set background or background color for banners to be fully functional.
            }
        }

        public void OnDisable()
        {
            MaxSdkCallbacks.Banner.OnAdLoadedEvent -= OnBannerAdLoadedEvent;
            MaxSdkCallbacks.Banner.OnAdLoadFailedEvent -= OnBannerAdFailedEvent;
            MaxSdkCallbacks.Banner.OnAdClickedEvent -= OnBannerAdClickedEvent;
            MaxSdkCallbacks.Banner.OnAdRevenuePaidEvent -= OnBannerAdRevenuePaidEvent;

            MaxSdkCallbacks.Banner.OnAdLoadedEvent -= SendOnBannerAdLoadedPixel;
            MaxSdkCallbacks.Banner.OnAdLoadFailedEvent -= SendOnBannerAdFailedPixel;
            MaxSdkCallbacks.Banner.OnAdClickedEvent -= SendOnBannerAdClickedPixel;
            MaxSdkCallbacks.Banner.OnAdRevenuePaidEvent -= SendOnBannerAdRevenuePaidPixel;
        }

        private void ToggleBannerVisibility()
        {
            if (!isBannerShowing)
            {
                MaxSdk.ShowBanner(adUnitId);
            }
            else
            {
                MaxSdk.HideBanner(adUnitId);
            }
            isBannerShowing = !isBannerShowing;
        }

        public void ShowBanner()
        {
#if UNITY_EDITOR
            IsBannerLoaded = true;
#endif
            if (IsAdsDisabled())
                return;

            if (IsBannerLoaded)
            {
                MaxSdk.ShowBanner(adUnitId);
                isBannerShowing = true;

                ACLogger.UserDebug("Banner Ad is Showing: " + adUnitId);

                SendShowBannerPixel();
            }
            else
            {
                isBannerShowing = false;
                InitializeBannerAds();
            }
        }

        public void HideBanner()
        {
            MaxSdk.HideBanner(adUnitId);
            isBannerShowing = false;

            ACLogger.UserDebug("Banner Ad is Hidden");
        }

        private void DestroyBanner()
        {
            MaxSdk.DestroyBanner(adUnitId);
            isBannerShowing = false;
        }

        private void OnBannerAdLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            // Banner ad is ready to be shown.
            // If you have already called MaxSdk.ShowBanner(BannerAdUnitId) it will automatically be shown on the next ad refresh.
            ACLogger.UserDebug("Banner ad loaded");

            IsBannerLoaded = true;
        }

        private void OnBannerAdFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
        {
            // Banner ad failed to load. MAX will automatically try loading a new ad internally.
            ACLogger.UserDebug("Banner ad failed to load with error code: " + errorInfo.Code);

            IsBannerLoaded = false;
        }

        private void OnBannerAdClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            ACLogger.UserDebug("Banner ad clicked");
        }

        private void OnBannerAdRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            // Banner ad revenue paid. Use this callback to track user revenue.
            ACLogger.UserDebug("Banner ad revenue paid");
        }

        #endregion

        #region AppCentral Pixel

        int retryAttempt = 0;

        const string AdPixelType = "banner_pixel";

        const string ShowBannerPixel = "ShowBannerEvent";
        const string OnBannerAdLoadedEventPixel = "OnBannerAdLoadedEvent";
        const string OnBannerAdFailedEventPixel = "OnBannerAdFailedEvent";
        const string OnBannerAdClickedEventPixel = "OnBannerAdClickedEvent";
        const string OnBannerAdRevenuePaidEventPixel = "OnBannerAdRevenuePaidEvent";

        #region PixelEvents
        private void SendShowBannerPixel()
        {
            AppCentralCore.AppCentralPixelController.Instance.CallAdLogPixel(
                AdPixelType,
                ShowBannerPixel,
                null
            );
        }

        private void SendOnBannerAdLoadedPixel(string adUnitId = "", MaxSdkBase.AdInfo s = null)
        {
            AppCentralCore.AppCentralPixelController.Instance.CallAdLogPixel(
                AdPixelType,
                OnBannerAdLoadedEventPixel,
                retryAttempt.ToString()
            );
            // Reset retry attempt
            retryAttempt = 0;
        }

        private void SendOnBannerAdFailedPixel(string adUnitId = "", MaxSdkBase.ErrorInfo s = null)
        {
            retryAttempt++;

            AppCentralCore.AppCentralPixelController.Instance.CallAdLogPixel(
                AdPixelType,
                OnBannerAdFailedEventPixel,
                retryAttempt.ToString()
            );
            AppCentralCore.AppCentralPixelController.Instance.WaitandSendAdToEnd(AdPixelType);
        }

        private void SendOnBannerAdClickedPixel(string adUnitId, MaxSdk.AdInfo info = null)
        {
            AppCentralCore.AppCentralPixelController.Instance.CallAdLogPixel(
                AdPixelType,
                OnBannerAdClickedEventPixel,
                null
            );
        }

        private void SendOnBannerAdRevenuePaidPixel(
            string adUnitId = "",
            MaxSdkBase.AdInfo s = null
        )
        {
            AppCentralCore.AppCentralPixelController.Instance.CallAdLogPixel(
                AdPixelType,
                OnBannerAdRevenuePaidEventPixel,
                null
            );
        }

        #endregion



        #endregion

        private void UpdateAdStatus(bool status)
        {
            ApplovinAdManager.IsBannerLoaded = status;
        }

        private bool IsAdsDisabled()
        {
            return false;
        }
#endif
    }
}
