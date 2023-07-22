using System;
using System.Collections;
using UnityEngine;

namespace GH
{
    public class AL_InterstitialImpl : MonoBehaviour
    {
#if AC_APPLOVIN

        string adUnitId = "";

        Action AdFinishedCallback;

        public void InterstitialImpl(string AdID)
        {
            adUnitId = AdID;
        }

        #region Interstitial Ad Methods

        private int interstitialRetryAttempt;

        public void InitializeInterstitialAds()
        {
            UpdateAdStatus(false);

            // Attach callbacks
            MaxSdkCallbacks.Interstitial.OnAdLoadedEvent += OnInterstitialLoadedEvent;
            MaxSdkCallbacks.Interstitial.OnAdLoadFailedEvent += OnInterstitialLoadFailedEvent;
            MaxSdkCallbacks.Interstitial.OnAdDisplayedEvent += OnInterstitialDisplayedEvent;
            MaxSdkCallbacks.Interstitial.OnAdHiddenEvent += OnInterstitialHiddenEvent;
            MaxSdkCallbacks.Interstitial.OnAdClickedEvent += OnInterstitialClickedEvent;
            MaxSdkCallbacks.Interstitial.OnAdDisplayFailedEvent +=
                OnInterstitialAdFailedToDisplayEvent;

            // Appcentral Pixel Directly bind to the Ads

            MaxSdkCallbacks.Interstitial.OnAdLoadedEvent += SendOnInterstitialLoadedPixel;
            MaxSdkCallbacks.Interstitial.OnAdLoadFailedEvent += SendOnInterstitialLoadFailedPixel;
            MaxSdkCallbacks.Interstitial.OnAdDisplayedEvent += SendOnInterstitialDisplayedPixel;
            MaxSdkCallbacks.Interstitial.OnAdHiddenEvent += SendOnInterstitialHiddenPixel;
            MaxSdkCallbacks.Interstitial.OnAdClickedEvent += SendOnInterstitialClickedPixel;
            MaxSdkCallbacks.Interstitial.OnAdDisplayFailedEvent +=
                SendOnInterstitialAdFailedToDisplayPixel;

            // Load the first interstitial
            LoadInterstitialAd();
        }

        private void LoadInterstitialAd()
        {
            ACLogger.UserDebug(": LoadInterstitialAd_Applovin");
            MaxSdk.LoadInterstitial(adUnitId);
            SendLoadInterstitialPixel();
        }

        public bool HasInterstitalAd()
        {
            bool hasAd = MaxSdk.IsInterstitialReady(adUnitId);
            if (!hasAd)
                LoadInterstitialAd();
            return hasAd;
        }

        public void ShowInterstitialAd()
        {
            if (IsAdsDisabled())
                return;

            ACLogger.UserDebug("HasInterstitalAd=" + HasInterstitalAd());

            if (HasInterstitalAd())
            {
                PlayerPrefs.SetFloat("AudioListenerVolume", AudioListener.volume);
                AudioListener.volume = 0f;
                MaxSdk.ShowInterstitial(adUnitId);

                SendShowInterstitialPixel();
                SendShowInterstitialSmartLook();
            }
        }

        public void ShowInterstitialAd(Action adFinishedCallback)
        {
            AdFinishedCallback = adFinishedCallback;

            if (HasInterstitalAd())
            {
                ShowInterstitialAd();
            }
            else
            {
                OnAdFinished();
            }
        }

        #region Interstitial CallBakcs
        private void OnInterstitialLoadedEvent(string adUnitId, MaxSdkBase.AdInfo s)
        {
            ACLogger.UserDebug("Interstitial loaded");
            UpdateAdStatus(true);
            interstitialRetryAttempt = 0;
        }

        private void OnInterstitialLoadFailedEvent(string adUnitId, MaxSdk.ErrorInfo errorInfo)
        {
            UpdateAdStatus(false);

            ACLogger.UserDebug("Interstitial failed to load with error code: " + errorInfo.Code);

            // Interstitial ad failed to load. We recommend retrying with exponentially higher delays.



            try
            {
                interstitialRetryAttempt++;
                double retryDelay = Math.Pow(2, interstitialRetryAttempt);
                Invoke("LoadInterstitialAd", (float)retryDelay);
            }
            catch
            {
                ACLogger.UserDebug("Catch: OnInterstitialLoadFailedEvent error occured");
            }
        }

        private void OnInterstitialDisplayedEvent(string adUnitId, MaxSdk.AdInfo errorCode)
        {
            UpdateAdStatus(false);

#if !UNITY_EDITOR
            OnAdFinished();
#endif
            // Interstitial ad failed to display. We recommend loading the next ad
            ACLogger.UserDebug("OnInterstitialDisplayedEvent with error code: " + errorCode);
            LoadInterstitialAd();
        }

        private void OnInterstitialClickedEvent(string adUnitId, MaxSdk.AdInfo info)
        {
            UpdateAdStatus(false);

            ACLogger.UserDebug("Ad clicked");
        }

        private void OnInterstitialHiddenEvent(string adUnitId, MaxSdk.AdInfo info)
        {
            UpdateAdStatus(false);

            OnAdFinished();

            AudioListener.volume = PlayerPrefs.GetFloat("AudioListenerVolume", 1f);
            // Interstitial ad is hidden. Pre-load the next ad
            ACLogger.UserDebug("Interstitial dismissed");
            LoadInterstitialAd();
        }

        private void OnInterstitialAdFailedToDisplayEvent(
            string adUnitId,
            MaxSdk.ErrorInfo errorInfo,
            MaxSdk.AdInfo info
        )
        {
            UpdateAdStatus(false);

            OnAdFinished();

            AudioListener.volume = PlayerPrefs.GetFloat("AudioListenerVolume", 1f);
            // Interstitial ad is hidden. Pre-load the next ad
            ACLogger.UserDebug("OnInterstitialAdFailedToDisplayEvent");
            LoadInterstitialAd();
        }

        #endregion


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
