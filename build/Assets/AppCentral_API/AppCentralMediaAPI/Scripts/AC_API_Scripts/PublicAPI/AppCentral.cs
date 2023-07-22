using UnityEngine;
using AppCentralCore;
using System;
using GH;
using UnityEngine.Purchasing;

namespace AppCentralAPI
{
    public class AppCentral : MonoBehaviour
    {
        #region Singleton

        private static AppCentral _instance;

        public static AppCentral Instance
        {
            get => _instance;
        }

        private static int currentLevelID = 0;

        public static int CurrentLevelID
        {
            get => AppCentralUnityApi_Internal.getLevelPref();
            private set => currentLevelID = value;
        }

        private void Awake()
        {
            // AppCentralUnityApi singleton pattern
            if (Instance != null && Instance != this)
                DestroyImmediate(gameObject);
            else
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        #endregion

        //--------------------------------------------------------

        #region GamePlayEvents

        public static void StartPlay()
        {
            AppCentralUnityApi_Internal.Instance.StartPlay();
        }

        public static void DeepInGame()
        {
            AppCentralUnityApi_Internal.Instance.DeepInGame();
        }

        #endregion

        //--------------------------------------------------------

        #region Paywall

        public static Action PayWallOpen;
        public static Action PayWallClosed;
        public static Action UserSubscribed;

        public static void SetPlayerPrefs()
        {
            AppCentralCore.AppCentralUnityApi_Internal.Instance.jsonController.UpdateDataToLocalMemory();
        }

        public static bool IsSuscriptionActive()
        {
            return AppCentralInAppPurchaser.IsSubscriptionActive();
        }

        public static void ShowDefulatPaywall()
        {
            GH.UI_PayWall.Instance.ActivateDefulatPaywall();
        }

        public static void ShowDynamicPaywall(Action OnCloseCallback = null)
        {
            GH.UI_PayWall.Instance.ActivateDynaimcPaywall(OnCloseCallback);
        }

        public static void ShowMidGamePaywall(Action OnCloseCallback = null)
        {
            GH.UI_PayWall.Instance.ActivateMidGamePaywall(OnCloseCallback);
        }

        private static void ShowMidGamePaywall_AfterInterstitial()
        {
            ShowMidGamePaywall(MidGamePaywallCloseEvent);
        }

        private static void ShowDynamicPaywall_WillCloseCallBack(Action OnCloseCallback)
        {
            ShowDynamicPaywall(OnCloseCallback);
        }

        #endregion

        //--------------------------------------------------------

        #region AppLovin


        /// <summary>
        /// call this method to show Banner Ad
        /// </summary>
        public static void ShowBannerAd()
        {
#if AC_APPLOVIN

            GH.ApplovinAdManager.Instance.ShowBanner();
#endif
        }

        public static void HideBannerAd()
        {
#if AC_APPLOVIN

            GH.ApplovinAdManager.Instance.HideBanner();
#endif
        }

        /// <summary>
        /// call this mouthed to show interstitial Ad.
        /// if the interstitial ad is unavailable then it will check for dynamic paywall show location.
        /// If dynamic paywall SHOW_LOCATION has "UnAvailable" then dynamic paywall will show up.
        /// </summary>
        public static void ShowInterstitalAd()
        {
#if AC_APPLOVIN
            if (GH.ApplovinAdManager.Instance.IsInterstitialLoaded)
            {
                GH.ApplovinAdManager.Instance.ShowInterstitial();
            }
            else
            {
                if (
                    AppCentralUnityApi_Internal.IsDynamicPaywallLocationsContain(
                        SHOW_LOCATION.adsUnavailable
                    )
                )
                    ShowDynamicPaywall();
            }
#endif
        }

        private static void ShowInterstitalAd(
            Action OnCloseCallback,
            bool isMidGamePaywallIsOnBack = false
        )
        {
#if AC_APPLOVIN

            bool IsInterstitialLoaded = GH.ApplovinAdManager.Instance.IsInterstitialLoaded;
            bool CanShowDynamicPaywall =
                !isMidGamePaywallIsOnBack
                && AppCentralUnityApi_Internal.IsDynamicPaywallLocationsContain(
                    SHOW_LOCATION.adsUnavailable
                );

            ACLogger.UserDebug(": IsInterstitialLoaded:" + IsInterstitialLoaded);
            ACLogger.UserDebug(": CanShowDynamicPaywall:" + CanShowDynamicPaywall);

            if (IsInterstitialLoaded)
            {
                ACLogger.UserDebug(": IsInterstitialLoadedA:" + IsInterstitialLoaded);
                GH.ApplovinAdManager.Instance.ShowInterstitial(OnCloseCallback);
            }
            else if (CanShowDynamicPaywall)
            {
                ACLogger.UserDebug(": CanShowDynamicPaywallA:" + CanShowDynamicPaywall);
                ShowDynamicPaywall_WillCloseCallBack(OnCloseCallback);
            }
            else
            {
                ACLogger.UserDebug(": NULL:");
                OnCloseCallback?.Invoke();
            }
#else
            OnCloseCallback?.Invoke();
#endif
        }

        /// <summary>
        /// This method handles both the Midgame Paywall and Interstial Ad at level complete. depending on the perms from the server and your game.
        // 1. if there's no interstitial, the mid-game paywall will be displayed regardless of the adsInteraction variable.
        // 2. if there is an interstitial, if the adsInteraction equals "after", the midgame paywall will be shown after the interstitial.
        // 3. if there is an interstitial, if the adsInteraction equals "instead", the mid-game paywall will be shown instead of the interstitial.

        /// <param name="currentLevelIndex"> send your current level completion ID</param>
        /// <param name="showInterstialAdAtThisLevel"> "true" if you want to show interstitial ad at this level, "false" if you don't want to show interstitial ad at this level </param>
        /// <param name="OnCloseCallback">: you send an ACTION and listen to when "ShowInterstitialAdAtLevelCompletion" completes after closing interstitial/MidGame paywall/once both close. </param>
        /// </summary>
        public static void ShowInterstitalAdAtLevelCompletion(
            bool showInterstialAdAtThisLevel,
            Action OnCloseCallback = null
        )
        {
#if AC_APPLOVIN

            int currentLevelIndex = CurrentLevelID;

            Debug.Log("currentLevelIndex:" + currentLevelIndex);

            bool isMidPaywallShowed = false;

            string paywall_midgame_show_in_levels = "paywall_midgame_show_in_levels";
            string paywall_midgame_ads_interaction = "paywall_midgame_ads_interaction";

            string paywall_midgame_show_in_levelsPrefValue = PlayerPrefs.GetString(
                paywall_midgame_show_in_levels,
                ""
            );
            string paywall_midgame_ads_interactionPrefValue = PlayerPrefs.GetString(
                paywall_midgame_ads_interaction,
                ""
            );

            ACLogger.UserDebug(
                ": paywall_midgame_show_in_levelsPrefValue="
                    + paywall_midgame_show_in_levelsPrefValue
            );
            ACLogger.UserDebug(
                ": paywall_midgame_ads_interactionPrefValue="
                    + paywall_midgame_ads_interactionPrefValue
            );

            if (!string.IsNullOrEmpty(paywall_midgame_show_in_levelsPrefValue))
            {
                ACLogger.UserDebug(": ShowInterstitalAdAtLevelCompletion_A");
                if (
                    AppCentralUnityApi_Internal.IsCurrentLevelIsValideToShowMidGamePaywall(
                        paywall_midgame_show_in_levelsPrefValue,
                        currentLevelIndex
                    )
                )
                {
                    ACLogger.UserDebug(": ShowInterstitalAdAtLevelCompletion_B");

                    if (
                        paywall_midgame_ads_interactionPrefValue
                        == MidGamePaywallAdInteraction.instead.ToString()
                    )
                    {
                        ACLogger.UserDebug(": ShowInterstitalAdAtLevelCompletion_C");

                        ShowMidGamePaywall(OnCloseCallback);
                        isMidPaywallShowed = true;
                    }
                    else if (
                        paywall_midgame_ads_interactionPrefValue
                        == MidGamePaywallAdInteraction.after.ToString()
                    )
                    {
                        ACLogger.UserDebug(": ShowInterstitalAdAtLevelCompletion_D");

                        if (
                            showInterstialAdAtThisLevel
                            && GH.ApplovinAdManager.Instance.IsInterstitialLoaded
                        )
                        {
                            ACLogger.UserDebug(": ShowInterstitalAdAtLevelCompletion_E");

                            MidGamePaywallCloseEvent = OnCloseCallback;
                            ShowInterstitalAd(ShowMidGamePaywall_AfterInterstitial, true);
                        }
                        else
                        {
                            ACLogger.UserDebug(": ShowInterstitalAdAtLevelCompletion_F");

                            ShowMidGamePaywall(OnCloseCallback);
                        }

                        isMidPaywallShowed = true;
                    }
                }
                else if (showInterstialAdAtThisLevel)
                {
                    ACLogger.UserDebug(": ShowInterstitalAdAtLevelCompletion_G");

                    ShowInterstitalAd(OnCloseCallback);
                }
                else
                {
                    ACLogger.UserDebug(": ShowInterstitalAdAtLevelCompletion_H");

                    OnCloseCallback?.Invoke();
                }
            }
            else if (showInterstialAdAtThisLevel)
            {
                ACLogger.UserDebug(": ShowInterstitalAdAtLevelCompletion_I");

                ShowInterstitalAd(OnCloseCallback);
            }
            else
            {
                ACLogger.UserDebug(": ShowInterstitalAdAtLevelCompletion_J");

                OnCloseCallback?.Invoke();
            }

#else
            OnCloseCallback?.Invoke();
#endif
        }

        public static void ShowRewardedAd(Action<bool> rewardReceiveCallback)
        {
#if AC_APPLOVIN
            GH.ApplovinAdManager.Instance.ShowRewardedAd(rewardReceiveCallback);
#endif
        }

        #endregion

        //--------------------------------------------------------

        #region RateUS

        public static bool ShowRateUsMenu()
        {
            return GH.RateUsController.Instance.ActivateRateUS(CurrentLevelID);
        }

        public static bool ShowRateUsMenu(Action onCloseCallback)
        {
            return GH.RateUsController.Instance.ActivateRateUS(CurrentLevelID, onCloseCallback);
        }

        #endregion

        //--------------------------------------------------------

        #region InternetConnectivity

        // call this method ONCE on every level complete
        public static void CheckInternetConnection()
        {
            PresistanceCanvas.GH_InternetConnectionPanel.Instance.IsInternetAvalible();
        }

        #endregion

        //--------------------------------------------------------

        #region GamePlayParameters

        public static void SetLevelStartID(int LevelID)
        {
            currentLevelID = LevelID;
            AppCentralUnityApi_Internal.Instance.levelStart(LevelID);

        }

        public static void OnLevelComplete()
        {
            AppCentralUnityApi_Internal.Instance.levelComplete(currentLevelID);
        }

        #endregion

        //--------------------------------------------------------

        #region AppCentralPixel


        public static void SendShopButtonClickEventToPixel()
        {
            AppCentralPixelController.Instance.SaveAppCentralPixel(
                "shop_clicked_pixel",
                new string[] { "shop" },
                new string[] { "clicked" }
            );
        }

        public static void SendUserSkinChoiceEventToPixel(int userSkinChoiseIndex)
        {
            string character = "default";

            if (userSkinChoiseIndex != 0) // 0 means user chose a defulat charcter
            {
                character = userSkinChoiseIndex.ToString();
            }

            AppCentralPixelController.Instance.SaveAppCentralPixel(
                "character_choice_pixel",
                new string[] { "character" },
                new string[] { character }
            );
        }

        public static void SendBuildEventToPixel(
            PixelBuildActionType pixelBuildActionType,
            PixelBuildEndType pixelBuildEndType
        )
        {
            string action = pixelBuildActionType.ToString(); // start || end
            string end_type = "";

            if (pixelBuildEndType != PixelBuildEndType.none)
            {
                end_type = pixelBuildEndType.ToString();
            }

            string session_id = "";
            if (action == "start")
                session_id = Guid.NewGuid().ToString(); // Generate a random UID

            AppCentralPixelController.Instance.SaveAppCentralPixel(
                "build_pixel",
                new string[] { "action", "end_type", "session_id" },
                new string[] { action, end_type, session_id }
            );
        }

        public static void SendBonusStageEventToPixel(
            PixelBonusActionType pixelBonusActionType,
            int bonusStageNumber
        )
        {
            string action = pixelBonusActionType.ToString(); // start_rewarded || start_play || skipped
            string number = bonusStageNumber.ToString(); // bonus stage number

            AppCentralPixelController.Instance.SaveAppCentralPixel(
                "bonus_stage_pixel",
                new string[] { "action", "number" },
                new string[] { action, number }
            );
        }

        public static void SendGenderChoiceToPixel(
            Gender gender,
            GenderPopUpShowLocation genderPopUpShow
        )
        {
            AppCentralPixelController.Instance.SaveAppCentralPixel(
                "gender_choice_pixel",
                new string[] { "gender", "choice_location" },
                new string[] { gender.ToString(), genderPopUpShow.ToString() }
            );
        }

        #endregion

        //--------------------------------------------------------

        #region BRIGHT-SDK

        // Listen to this event to receive event when user BrightData consent status changes
        public static Action<BrightDataUserConsentChoice> OnBrightSDKConsentStatusChange;

        // Get user BrightData consent status
        // TRUE = user agreed to the consent
        // FASLE = User decline the consent
        public bool GetBrightSDKConsentStatus()
        {
            return AppCentralBrightDataController.GetConcentStatus();
        }

        // Show Bright SDK Consent Dialog
        public void ShowBrightSDKConsentDialog()
        {
            AppCentralBrightDataController.ShowConcent();
        }

        // user chose to OptOut of Bright Data SDK consent
        public void OptOutBrightSDKConsent()
        {
            AppCentralBrightDataController.UserOptOut();
        }

        #endregion















        //--------------------------------------------------------


        #region Paywall_ExtentedSection

        static Action MidGamePaywallCloseEvent;

        bool isPaywallOpened = false;

        private void OnEnable()
        {
            UI_PayWall.PaywallOpen += OnPaywallOpen;
            UI_PayWall.PaywallClose += OnPaywallClose;
            UI_PayWall.PaywallUserSubscribed += OnUserSubscribed;
        }

        private void OnDisable()
        {
            UI_PayWall.PaywallOpen -= OnPaywallOpen;
            UI_PayWall.PaywallClose -= OnPaywallClose;
            UI_PayWall.PaywallUserSubscribed -= OnUserSubscribed;
        }

        private void OnPaywallOpen(PAYWALL_TYPE pAYWALL_TYPE)
        {
            if (pAYWALL_TYPE != PAYWALL_TYPE.popup)
                HideBannerAd();

            PayWallOpen?.Invoke();

            ACLogger.UserDebug(":[Paywall] OnPaywallOpen Event:" + pAYWALL_TYPE.ToString());
        }

        private void OnPaywallClose(PAYWALL_TYPE pAYWALL_TYPE)
        {
            ShowBannerAd();
            PayWallClosed?.Invoke();
            ACLogger.UserDebug(":[Paywall] OnPaywallClose Event:" + pAYWALL_TYPE.ToString());
        }

        private void OnUserSubscribed(
            PAYWALL_TYPE pAYWALL_TYPE,
            PurchaseEventArgs purchaseEventArgs
        )
        {
            HideBannerAd();
            UserSubscribed?.Invoke();
            ACLogger.UserDebug(":[Paywall] OnUserSubscribed Event:" + pAYWALL_TYPE.ToString());
        }

        #endregion
    }
}
