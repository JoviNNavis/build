using System;
using System.Collections;
using UnityEngine;

namespace GH
{
    public class AL_AppOpenImpl : MonoBehaviour
    {
#if AC_APPLOVIN

        string adUnitId = "";

        Action AdFinishedCallback;

        public void AppOpenImpl(string AdID)
        {
            adUnitId = AdID;
        }

        #region Interstitial Ad Methods

        private int interstitialRetryAttempt;


        public void InitializeAppOpenAds()
        {

            if (string.IsNullOrEmpty(adUnitId))
            {
                ACLogger.UserError("AppOpenImpl ad not InitializeAppOpenAds due to empty ad unity ID");
                return;
            }


            ACLogger.UserDebug("AppOpenImpl InitializeAppOpenAds");


            if (!AppCentralAPI.AppCentral.IsSuscriptionActive())
            {
                MaxSdkCallbacks.OnSdkInitializedEvent += sdkConfiguration =>
                {
                    ACLogger.UserDebug("AppOpenImpl OnSdkInitializedEvent");

                    // Register for App Open ad events
                    MaxSdkCallbacks.AppOpen.OnAdLoadedEvent += OnAppOpenAdLoadedEvent;
                    MaxSdkCallbacks.AppOpen.OnAdLoadFailedEvent += OnAppOpenAdLoadFailedEvent;
                    MaxSdkCallbacks.AppOpen.OnAdDisplayedEvent += OnAppOpenAdDisplayedEvent;
                    MaxSdkCallbacks.AppOpen.OnAdDisplayFailedEvent += OnAppOpenAdFailedToDisplayEvent;
                    MaxSdkCallbacks.AppOpen.OnAdClickedEvent += OnAppOpenAdClickedEvent;
                    MaxSdkCallbacks.AppOpen.OnAdRevenuePaidEvent += OnAppOpenAdRevenuePaidEvent;

                    MaxSdkCallbacks.AppOpen.OnAdHiddenEvent += OnAppOpenDismissedEvent;

                    LoadAppOpen();
                };
            }
        }

        public void OnAppOpenDismissedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {

            if (string.IsNullOrEmpty(adUnitId)) { return; }


            ACLogger.UserDebug("AppOpenImpl OnAppOpenDismissedEvent");

            if (IsAppOpenEnabledFromServer() && !AppCentralAPI.AppCentral.IsSuscriptionActive()) LoadAppOpen();
        }

        private void LoadAppOpen()
        {


            if (!string.IsNullOrEmpty(adUnitId))
            {
                ACLogger.UserDebug("AppOpenImpl ad load request send");
                MaxSdk.LoadAppOpenAd(adUnitId);
            }
            else
            {
                ACLogger.UserDebug("AppOpenImpl ad not loading due to empty ad unity ID");
            }
        }

        public void ShowAdIfReady()
        {



            ACLogger.UserDebug("AppOpenImpl requested");
            ACLogger.UserDebug("AppOpenImpl IsAppOpenEnabledFromServer: " + IsAppOpenEnabledFromServer());
            ACLogger.UserDebug("AppOpenImpl IsSuscriptionActive: " + AppCentralAPI.AppCentral.IsSuscriptionActive());
            ACLogger.UserDebug("AppOpenImpl Ad unity ID: " + adUnitId);

            if (string.IsNullOrEmpty(adUnitId)) { return; }



            if (IsAppOpenEnabledFromServer() && !AppCentralAPI.AppCentral.IsSuscriptionActive())
            {

                if (MaxSdk.IsAppOpenAdReady(adUnitId))
                {
                    ACLogger.UserDebug("AppOpenImpl AppOpenAd Ready");
                    MaxSdk.ShowAppOpenAd(adUnitId);
                }
                else
                {
                    ACLogger.UserDebug("AppOpenImpl AppOpenAd Not Ready");
                    LoadAppOpen();
                }
            }
        }


        private bool IsAppOpenEnabledFromServer()
        {
            return PlayerPrefs.GetInt(AppCentralCore.AppCentralPrefsManager.app_open_ad) == 1 ? true : true;
        }


        private void OnAppOpenAdLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            ACLogger.UserDebug("App Open ad loaded.");
        }

        private void OnAppOpenAdLoadFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
        {
            ACLogger.UserDebug("App Open ad failed to load: " + errorInfo);
        }

        private void OnAppOpenAdDisplayedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            ACLogger.UserDebug("App Open ad displayed.");
        }

        private void OnAppOpenAdFailedToDisplayEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo)
        {
            ACLogger.UserDebug("App Open ad failed to display: " + errorInfo);
        }

        private void OnAppOpenAdClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            ACLogger.UserDebug("App Open ad clicked.");
        }

        private void OnAppOpenAdRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            ACLogger.UserDebug("App Open ad revenue paid.");
        }

        private void OnAppOpenAdHiddenEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {

            ACLogger.UserDebug("App Open ad hidden.");
        }

        private void OnDestroy()
        {
            if (string.IsNullOrEmpty(adUnitId)) { return; }



            // Unregister for App Open ad events
            MaxSdkCallbacks.AppOpen.OnAdLoadedEvent -= OnAppOpenAdLoadedEvent;
            MaxSdkCallbacks.AppOpen.OnAdLoadFailedEvent -= OnAppOpenAdLoadFailedEvent;
            MaxSdkCallbacks.AppOpen.OnAdDisplayedEvent -= OnAppOpenAdDisplayedEvent;
            MaxSdkCallbacks.AppOpen.OnAdDisplayFailedEvent -= OnAppOpenAdFailedToDisplayEvent;
            MaxSdkCallbacks.AppOpen.OnAdClickedEvent -= OnAppOpenAdClickedEvent;
            MaxSdkCallbacks.AppOpen.OnAdRevenuePaidEvent -= OnAppOpenAdRevenuePaidEvent;
            MaxSdkCallbacks.AppOpen.OnAdHiddenEvent -= OnAppOpenAdHiddenEvent;
        }


        #endregion


        #region AppCentral Pixel

        int retryAttempt = 0;

        const string AdPixelType = "interstitial_pixel";

        const string LoadInterstitialPixel = "LoadInterstitial";
        const string OnInterstitialLoadedEventPixel = "OnInterstitialLoadedEvent";
        const string OnInterstitialLoadFailedEventPixel = "OnInterstitialLoadFailedEvent";
        const string OnInterstitialDisplayedEventPixel = "OnInterstitialDisplayedEvent";
        const string OnInterstitialAdFailedToDisplayEventPixel =
            "OnInterstitialAdFailedToDisplayEvent";
        const string OnInterstitialClickedEventPixel = "OnInterstitialClickedEvent";
        const string OnInterstitialHiddenEventPixel = "OnInterstitialHiddenEvent";
        const string ShowInterstitialPixel = "ShowInterstitial";

        #region PixelEvents
        private void SendLoadInterstitialPixel()
        {
            AppCentralCore.AppCentralPixelController.Instance.CallAdLogPixel(
                AdPixelType,
                LoadInterstitialPixel,
                null
            );
        }

        private void SendOnInterstitialLoadedPixel(string adUnitId = "", MaxSdkBase.AdInfo s = null)
        {
            AppCentralCore.AppCentralPixelController.Instance.CallAdLogPixel(
                AdPixelType,
                OnInterstitialLoadedEventPixel,
                retryAttempt.ToString()
            );
            // Reset retry attempt
            retryAttempt = 0;
        }

        private void SendOnInterstitialLoadFailedPixel(
            string adUnitId = "",
            MaxSdk.ErrorInfo errorInfo = null
        )
        {
            AppCentralCore.AppCentralPixelController.Instance.CallAdLogPixel(
                AdPixelType,
                OnInterstitialLoadFailedEventPixel,
                retryAttempt.ToString()
            );
            // Increment retryAttempt
            retryAttempt++;
        }

        private void SendOnInterstitialDisplayedPixel(
            string adUnitId = "",
            MaxSdkBase.AdInfo s = null
        )
        {
            AppCentralCore.AppCentralPixelController.Instance.CallAdLogPixel(
                AdPixelType,
                OnInterstitialDisplayedEventPixel,
                null
            );
            AppCentralCore.AppCentralPixelController.Instance.WaitandSendAdToEnd(AdPixelType);
        }

        private void SendOnInterstitialAdFailedToDisplayPixel(
            string adUnitId,
            MaxSdk.ErrorInfo errorInfo = null,
            MaxSdk.AdInfo info = null
        )
        {
            AppCentralCore.AppCentralPixelController.Instance.CallAdLogPixel(
                AdPixelType,
                OnInterstitialAdFailedToDisplayEventPixel,
                null
            );
        }

        private void SendOnInterstitialClickedPixel(
            string adUnitId = "",
            MaxSdkBase.AdInfo s = null
        )
        {
            AppCentralCore.AppCentralPixelController.Instance.CallAdLogPixel(
                AdPixelType,
                OnInterstitialClickedEventPixel,
                null
            );
        }

        private void SendOnInterstitialHiddenPixel(string adUnitId = "", MaxSdkBase.AdInfo s = null)
        {
            AppCentralCore.AppCentralPixelController.Instance.CallAdLogPixel(
                AdPixelType,
                OnInterstitialHiddenEventPixel,
                null
            );
        }

        private void SendShowInterstitialPixel()
        {
            AppCentralCore.AppCentralPixelController.Instance.CallAdLogPixel(
                AdPixelType,
                ShowInterstitialPixel,
                null
            );
        }

        #endregion



        #endregion


        #region SmartLookEventsCalls

        private void SendShowInterstitialSmartLook()
        {
            AppCentralCore.AppCentralSmartLookEventsInternal.SmartLookTrack_InterstitialAd();
        }

        #endregion

        private void OnAdFinished()
        {
            AdFinishedCallback?.Invoke();
            AdFinishedCallback = null;
        }

        private void UpdateAdStatus(bool status)
        {
            // ApplovinAdManager.IsInterstitialLoaded = status;
        }

        private bool IsAdsDisabled()
        {
            return false;
        }

#endif
    }
}
