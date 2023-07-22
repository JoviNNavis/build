using System;
using System.Collections;
using UnityEngine;

namespace GH
{
    public class AL_RewardedImpl : MonoBehaviour
    {
#if AC_APPLOVIN
        string adUnitId = "";

        public void RewardedImpl(string AdID)
        {
            adUnitId = AdID;
        }

        #region Rewarded Ad Methods


        private Action<bool> OnRewarded;
        private int rewardedRetryAttempt;

        public void InitializeRewardedAds()
        {
            UpdateAdStatus(false);

            // Attach callbacks
            MaxSdkCallbacks.Rewarded.OnAdLoadedEvent += OnRewardedAdLoadedEvent;
            MaxSdkCallbacks.Rewarded.OnAdLoadFailedEvent += OnRewardedAdLoadFailedEvent;
            MaxSdkCallbacks.Rewarded.OnAdDisplayedEvent += OnRewardedAdDisplayedEvent;
            MaxSdkCallbacks.Rewarded.OnAdClickedEvent += OnRewardedAdClickedEvent;
            MaxSdkCallbacks.Rewarded.OnAdRevenuePaidEvent += OnRewardedAdRevenuePaidEvent;
            MaxSdkCallbacks.Rewarded.OnAdHiddenEvent += OnRewardedAdHiddenEvent;
            MaxSdkCallbacks.Rewarded.OnAdDisplayFailedEvent += OnRewardedAdFailedToDisplayEvent;
            MaxSdkCallbacks.Rewarded.OnAdReceivedRewardEvent += OnRewardedAdReceivedRewardEvent;

            MaxSdkCallbacks.Rewarded.OnAdLoadedEvent += SendOnRewardLoadedPixel;
            MaxSdkCallbacks.Rewarded.OnAdLoadFailedEvent += SendOnRewardLoadFailedPixel;
            MaxSdkCallbacks.Rewarded.OnAdDisplayedEvent += SendOnRewardDisplayedPixel;
            MaxSdkCallbacks.Rewarded.OnAdClickedEvent += SendOnRewardClickedPixel;
            MaxSdkCallbacks.Rewarded.OnAdRevenuePaidEvent += SendOnRewardedAdRevenuePaidPixel;
            MaxSdkCallbacks.Rewarded.OnAdHiddenEvent += SendOnRewardlHiddenPixel;
            MaxSdkCallbacks.Rewarded.OnAdDisplayFailedEvent += SendOnRewardAdFailedToDisplayPixel;
            MaxSdkCallbacks.Rewarded.OnAdReceivedRewardEvent += SendOnRewardedAdReceivedRewardPixel;

            // Load the first RewardedAd
            LoadRewardedAd();
        }

        private void LoadRewardedAd()
        {
            ACLogger.UserDebug(": LoadRewardedAd_Applovin");
            SendLoadRewardedPixel();
            MaxSdk.LoadRewardedAd(adUnitId);
        }

        public void ShowRewardedAd(Action<bool> _OnRewarded)
        {
            OnRewarded = _OnRewarded;

            if (IsAdsDisabled())
            {
                SendRewardStatus(true);
                return;
            }

            if (HasRewardedAd())
            {
                PlayerPrefs.SetFloat("AudioListenerVolume", AudioListener.volume);
                AudioListener.volume = 0f;
                MaxSdk.ShowRewardedAd(adUnitId);

                SendShowRewardPixel();
                SendShowRewardedSmartLook();
            }
            else
            {
                //WarningPanel.Instance.ShowWarning_With("No Ad Avalible");
                PresistanceCanvas.GH_WarningManager.Instance.Prompt("No, ad avalible at this time.");

                SendRewardStatus(false);
                ACLogger.UserDebug("Ad not ready");
                //ToastHelper.ShowToast("Ad not ready", true);
            }
        }

        public bool HasRewardedAd()
        {
            bool HasAd = MaxSdk.IsRewardedAdReady(adUnitId);
            if (!HasAd)
                LoadRewardedAd();
            return HasAd;
        }

        private void OnRewardedAdLoadedEvent(string adUnitId, MaxSdk.AdInfo adInfo)
        {
            // Rewarded ad is ready to be shown. MaxSdk.IsRewardedAdReady(rewardedAdUnitId) will now return 'true'

            UpdateAdStatus(true);
            ACLogger.UserDebug("Rewarded ad loaded");

            // Reset retry attempt
            rewardedRetryAttempt = 0;
        }

        private void OnRewardedAdLoadFailedEvent(string adUnitId, MaxSdk.ErrorInfo errorCode)
        {
            UpdateAdStatus(false);

            ACLogger.UserDebug("Rewarded ad failed to load with error code: " + errorCode);

            // Rewarded ad failed to load. We recommend retrying with exponentially higher delays.


            try
            {
                rewardedRetryAttempt++;
                double retryDelay = Math.Pow(2, rewardedRetryAttempt);

                Invoke("LoadRewardedAd", (float)retryDelay);
            }
            catch
            {
                ACLogger.UserDebug("Catch: OnRewardedAdLoadFailedEvent error occured");
            }
        }

        private void OnRewardedAdDisplayedEvent(string adUnitId, MaxSdk.AdInfo info)
        {
            UpdateAdStatus(false);

            LoadRewardedAd();
            ACLogger.UserDebug("Rewarded ad displayed");
        }

        private void OnRewardedAdClickedEvent(string adUnitId, MaxSdk.AdInfo info)
        {
            UpdateAdStatus(false);

            ACLogger.UserDebug("Rewarded ad clicked" + info);
        }

        private void OnRewardedAdRevenuePaidEvent(string adUnitId, MaxSdk.AdInfo info)
        {
            UpdateAdStatus(false);

            ACLogger.UserDebug("OnRewardedAdRevenuePaidEvent" + info);
        }

        private void OnRewardedAdHiddenEvent(string adUnitId, MaxSdk.AdInfo info)
        {
            UpdateAdStatus(false);

            AudioListener.volume = PlayerPrefs.GetFloat("AudioListenerVolume", 1f);
            // Rewarded ad is hidden. Pre-load the next ad
            ACLogger.UserDebug("Rewarded ad dismissed");
            SendRewardStatus(false);
            LoadRewardedAd();
        }

        private void OnRewardedAdFailedToDisplayEvent(
            string id,
            MaxSdkBase.ErrorInfo errorCode,
            MaxSdk.AdInfo adUnitId
        )
        {
            UpdateAdStatus(false);

            // Rewarded ad failed to display. We recommend loading the next ad
            ACLogger.UserDebug("Rewarded ad failed to display with error code: " + errorCode);
            LoadRewardedAd();
            SendRewardStatus(false);
        }

        private void OnRewardedAdReceivedRewardEvent(
            string adUnitId,
            MaxSdk.Reward reward,
            MaxSdk.AdInfo info
        )
        {
            UpdateAdStatus(false);

            AudioListener.volume = PlayerPrefs.GetFloat("AudioListenerVolume", 1f);
            // Rewarded ad was displayed and user should receive the reward
            ACLogger.UserDebug("Rewarded ad received reward");
            SendRewardStatus(true);
        }

        private void SendRewardStatus(bool status)
        {
            OnRewarded?.Invoke(status);
            OnRewarded = null;
        }

        #endregion


        #region AppCentral Pixel

        int retryAttempt = 0;

        const string AdPixelType = "rewarded_pixel";

        const string LoadRewardPixel = "LoadRewardPixel";
        const string OnRewardLoadedEventPixel = "OnRewardLoadedEvent";
        const string OnRewardLoadFailedEventPixel = "OnRewardLoadFailedEvent";
        const string OnRewardDisplayedEventPixel = "OnRewardDisplayedEvent";
        const string OnRewardAdFailedToDisplayEventPixel = "OnRewardAdFailedToDisplayEvent";
        const string OnRewardClickedEventPixel = "OnRewardClickedEvent";
        const string OnRewardedAdRevenuePaidEventPixel = "OnRewardedAdRevenuePaidEvent";
        const string OnRewardHiddenEventPixel = "OnRewardHiddenEvent";
        const string OnRewardedAdReceivedRewardPixel = "OnRewardedAdReceivedRewardEvent";
        const string ShowRewardPixel = "ShowReward";

        #region PixelEvents
        private void SendLoadRewardedPixel()
        {
            AppCentralCore.AppCentralPixelController.Instance.CallAdLogPixel(
                AdPixelType,
                LoadRewardPixel,
                null
            );
        }

        private void SendOnRewardLoadedPixel(string adUnitId = "", MaxSdkBase.AdInfo s = null)
        {
            AppCentralCore.AppCentralPixelController.Instance.CallAdLogPixel(
                AdPixelType,
                OnRewardLoadedEventPixel,
                retryAttempt.ToString()
            );
            // Reset retry attempt
            retryAttempt = 0;
        }

        private void SendOnRewardLoadFailedPixel(
            string adUnitId = "",
            MaxSdk.ErrorInfo errorInfo = null
        )
        {
            AppCentralCore.AppCentralPixelController.Instance.CallAdLogPixel(
                AdPixelType,
                OnRewardLoadFailedEventPixel,
                retryAttempt.ToString()
            );
            // Increment retryAttempt
            retryAttempt++;
        }

        private void SendOnRewardDisplayedPixel(string adUnitId = "", MaxSdkBase.AdInfo s = null)
        {
            AppCentralCore.AppCentralPixelController.Instance.CallAdLogPixel(
                AdPixelType,
                OnRewardDisplayedEventPixel,
                null
            );
            AppCentralCore.AppCentralPixelController.Instance.WaitandSendAdToEnd(AdPixelType);
        }

        private void SendOnRewardAdFailedToDisplayPixel(
            string adUnitId,
            MaxSdk.ErrorInfo errorInfo = null,
            MaxSdk.AdInfo info = null
        )
        {
            AppCentralCore.AppCentralPixelController.Instance.CallAdLogPixel(
                AdPixelType,
                OnRewardAdFailedToDisplayEventPixel,
                null
            );
        }

        private void SendOnRewardClickedPixel(string adUnitId = "", MaxSdkBase.AdInfo s = null)
        {
            AppCentralCore.AppCentralPixelController.Instance.CallAdLogPixel(
                AdPixelType,
                OnRewardClickedEventPixel,
                null
            );
        }

        private void SendOnRewardedAdRevenuePaidPixel(string adUnitId, MaxSdk.AdInfo info)
        {
            AppCentralCore.AppCentralPixelController.Instance.CallAdLogPixel(
                AdPixelType,
                OnRewardedAdRevenuePaidEventPixel,
                null
            );
        }

        private void SendOnRewardlHiddenPixel(string adUnitId = "", MaxSdkBase.AdInfo s = null)
        {
            AppCentralCore.AppCentralPixelController.Instance.CallAdLogPixel(
                AdPixelType,
                OnRewardHiddenEventPixel,
                null
            );
        }

        private void SendOnRewardedAdReceivedRewardPixel(
            string adUnitId,
            MaxSdk.Reward reward,
            MaxSdk.AdInfo info
        )
        {
            AppCentralCore.AppCentralPixelController.Instance.CallAdLogPixel(
                AdPixelType,
                OnRewardedAdReceivedRewardPixel,
                null
            );
        }

        private void SendShowRewardPixel()
        {
            AppCentralCore.AppCentralPixelController.Instance.CallAdLogPixel(
                AdPixelType,
                ShowRewardPixel,
                null
            );
        }

        #endregion

        #endregion



        #region SmartLookEventsCalls

        private void SendShowRewardedSmartLook()
        {
            AppCentralCore.AppCentralSmartLookEventsInternal.SmartLookTrack_RewardedAd();
        }

        #endregion

        private void UpdateAdStatus(bool status)
        {
            // ApplovinAdManager.IsRewardedLoaded = status;
        }

        private bool IsAdsDisabled()
        {
            return false;
        }
#endif
    }
}
