using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using AppCentralAPI;
using AppCentralCore;
using System;
using GH;

namespace AppCentralTesting
{
    public class ApPCentralTestingHelper : MonoBehaviour
    {
        public LevelManager levelManager;
        public Text LevelText;
        public Button LevelStrat,
            LevelComplete,
            LevelFail,
            LevelSkip;
        public Button SL_InterstitalAdEvent,
            SL_RewardedAdEvent;

        public Button EnableMAXDebuger,
            InitializeMAX,
            ShowBannerAd,
            HideBannerAd,
            ShowAppOpenAd,
            ShowInterstialAd,
            ShowInterstialAdOnComplete,
            ShowRewardedAd;

        public Button ShowRateUs,
            CheckFlightMode;

        public Button ShowDefulatpaywall,
            ShowDynamicPaywall,
            CheckUserSubStatus;

        public Button CheckBrightSDKConcentStatus,
            OptOutBrightSDKConcent,
            ShowBrightSDKConcentDialog;

        //public Button customDiemtion01_theme1, customDiemtion01_theme2;

        public Toggle showAdAtThisLevelCompete;
        public Text isIAP_Processing;

        private void Start()
        {
            LevelStrat.interactable = true;
            LevelComplete.interactable = false;
            LevelFail.interactable = false;
            LevelSkip.interactable = false;

            levelManager.initialize();
            LevelText.text = "Level" + LevelManager.currentlevel_ID.ToString();

            ShowRateUs.onClick.AddListener(OnShowRateUS);
            CheckFlightMode.onClick.AddListener(OnShowTFprompt);

#if AC_GAMEANALYTICS

            LevelStrat.onClick.AddListener(OnLevelStrat);
            LevelComplete.onClick.AddListener(OnLevelComplete);
            LevelFail.onClick.AddListener(OnLevelFail);
            LevelSkip.onClick.AddListener(OnLevelSkip);

#endif

#if AC_APPLOVIN


            EnableMAXDebuger.onClick.AddListener(OnEnableMAXDebuger);
            InitializeMAX.onClick.AddListener(OnInitializeMAX);
            ShowBannerAd.onClick.AddListener(OnShowBannerAd);
            HideBannerAd.onClick.AddListener(OnHideBannerAd);
            ShowAppOpenAd.onClick.AddListener(OnShowAppOpenAd);
            ShowInterstialAd.onClick.AddListener(OnShowInterstitalAd);
            ShowInterstialAdOnComplete.onClick.AddListener(OnShowInterstitalOnLevelCompAd);
            showAdAtThisLevelCompete.onValueChanged.AddListener(SetshowAdAtThisLevelCompeteBool);
            showAdAtThisLevelCompete.isOn = false;
            ShowRewardedAd.onClick.AddListener(OnShowRewardedAd);

#endif

#if AC_IAPS

            ShowDefulatpaywall.onClick.AddListener(OnShowDefulatPaywall);
            ShowDynamicPaywall.onClick.AddListener(OnShowDynamicPaywall);
            CheckUserSubStatus.onClick.AddListener(OnSubscriptionStatus);
#endif

            SL_InterstitalAdEvent.onClick.AddListener(OnSL_InterstitalAdEvent);
            SL_RewardedAdEvent.onClick.AddListener(OnSL_RewardedAdEvent);

            CheckBrightSDKConcentStatus.onClick.AddListener(OnCheckBrightSDKConcentStatus);
            OptOutBrightSDKConcent.onClick.AddListener(OnOptOutBrightSDKConcent);
            ShowBrightSDKConcentDialog.onClick.AddListener(OnShowBrightSDKConcentDialog);

            LevelStrat.GetComponentInChildren<Text>().text = "OnLevelStrat";
            LevelComplete.GetComponentInChildren<Text>().text = "OnLevelComplete";
            LevelFail.GetComponentInChildren<Text>().text = "OnLevelFail";
            LevelSkip.GetComponentInChildren<Text>().text = "OnLevelSkip";

            SL_InterstitalAdEvent.GetComponentInChildren<Text>().text = "OnSL_InterstitalAdEvent";
            SL_RewardedAdEvent.GetComponentInChildren<Text>().text = "OnSL_RewardedAdEvent";

            EnableMAXDebuger.GetComponentInChildren<Text>().text = "EnableMAXDebuger";
            InitializeMAX.GetComponentInChildren<Text>().text = "InitializeMAX";
            ShowBannerAd.GetComponentInChildren<Text>().text = "ShowBannerAd";
            HideBannerAd.GetComponentInChildren<Text>().text = "HideBannerAd";
            ShowAppOpenAd.GetComponentInChildren<Text>().text = "ShowAppOpenAd";
            ShowInterstialAd.GetComponentInChildren<Text>().text = "ShowInterstialAd";
            ShowInterstialAdOnComplete.GetComponentInChildren<Text>().text ="ShowInterstialAdOnComplete";
            ShowRewardedAd.GetComponentInChildren<Text>().text = "ShowRewardedAd";

            ShowRateUs.GetComponentInChildren<Text>().text = "ShowRateUs";
            CheckFlightMode.GetComponentInChildren<Text>().text = "CheckFlightMode";

            ShowDefulatpaywall.GetComponentInChildren<Text>().text = "ShowDefulatpaywall";
            ShowDynamicPaywall.GetComponentInChildren<Text>().text = "ShowDynamicPaywall";
            CheckUserSubStatus.GetComponentInChildren<Text>().text = "CheckUserSubStatus";

            CheckBrightSDKConcentStatus.GetComponentInChildren<Text>().text ="CheckBrightSDKConcentStatus";
            OptOutBrightSDKConcent.GetComponentInChildren<Text>().text = "OptOutBrightSDKConcent";
            ShowBrightSDKConcentDialog.GetComponentInChildren<Text>().text ="ShowBrightSDKConcentDialog";



        }

        private void OnEnableMAXDebuger()
        {
            GH.ApplovinAdManager.Show_DebugPanel();
        }

        private void OnSL_RewardedAdEvent()
        {
            AppCentralSmartLookEventsInternal.SmartLookTrack_RewardedAd();
        }

        private void OnSL_InterstitalAdEvent()
        {
            AppCentralSmartLookEventsInternal.SmartLookTrack_InterstitialAd();
        }

        private void OnShowBrightSDKConcentDialog()
        {
            AppCentral.Instance.ShowBrightSDKConsentDialog();
        }

        private void OnCheckBrightSDKConcentStatus()
        {
            PresistanceCanvas.GH_WarningManager.Instance.Prompt(
                "Bright SDK user Concent Status: " + AppCentral.Instance.GetBrightSDKConsentStatus()
            );
        }

        private void OnOptOutBrightSDKConcent()
        {
            AppCentral.Instance.OptOutBrightSDKConsent();
        }

        //public void OnLevelStrat()
        //{


        //    int currentLevelID = 1; // set your own CURRENT LEVEL number here. NOTE Level number must start from 1 and not zero.

        //    int totalUniqueClearedLevelCount = 5; // set your own level number here. NOTE Level number must start from 1 and not zero.



        //    GameAnalyticsSDK.GAProgressionStatus gAProgressionStatus = GameAnalyticsSDK.GAProgressionStatus.Start; //set this value when user starts the level.

        //    GameAnalyticsSDK.GAProgressionStatus gAProgressionStatus = GameAnalyticsSDK.GAProgressionStatus.Fail; //set this value when user fails the level.

        //    GameAnalyticsSDK.GAProgressionStatus gAProgressionStatus = GameAnalyticsSDK.GAProgressionStatus.Complete; //set this value when user completes the level.



        //    LevelType levelType = LevelType.level; // For SIMPLE Levels set this

        //    LevelType levelType = LevelType.bonus; // For BONUS Levels set this



        //    // AppCentral may require you to have multiple GameMode.
        //    // GameMode can refer to different WORLD or GAMEPLAY-SETTINGS.
        //    //
        //    // If (you have multiple world setup in your game)
        //    // {
        //    //      set the "gameMode" to "GameMode.mode"
        //    // }else
        //    // {
        //    //      set the "gameMode" to "GameMode.@default"
        //    // }


        //    GameMode gameMode = GameMode.@default;  // If you have only one GameMode setup in your game then set this.

        //    GameMode gameMode = GameMode.mode;  // If you have multiple GameMode setup in your game then set this.

        //    int gameModeNumber = 1; // if you have more then one GameMode set this value accordingly.  NOTE Level number must start from 1 and not zero.



        //    AppCentralGameAnalyticsEvents.SendLevelProgressionEvent(gAProgressionStatus,levelType, currentLevelID,gameMode, gameModeNumber,totalUniqueClearedLevelCount);

















        //    AppCentralGameAnalyticsEvents.SendLevelProgressionEvent(GameAnalyticsSDK.GAProgressionStatus.Start,LevelType.level, LevelManager.currentlevel_ID,GameMode.mode, 2,LevelManager.TotalLevels_PlayedByUserSoFar);


        //    AppCentralSmartLookEvents.SmartLookTrackLevelStart(LevelManager.currentlevel_ID);

        //}


#if AC_GAMEANALYTICS

        public void OnLevelStrat()
        {
            LevelStrat.interactable = false;

            LevelComplete.interactable = true;
            LevelFail.interactable = true;
            LevelSkip.interactable = true;

            AppCentralAPI.AppCentral.SetLevelStartID(LevelManager.currentlevel_ID);

            AppCentralGameAnalyticsEvents.SendLevelProgressionEvent(
                GameAnalyticsSDK.GAProgressionStatus.Start,
                Progresion01.level,
                Progresion02.mode,
                2
            );

            AppCentralSmartLookEvents.SmartLookTrackLevelStart(LevelManager.currentlevel_ID);
        }

        public void OnLevelStrat2()
        {
            int currentLevelID = 1; // set your own CURRENT LEVEL number here. NOTE Level number must start from 1 and not zero.
            int totalUniqueClearedLevelCount = 5; // set your own level number here. NOTE Level number must start from 1 and not zero.
            GameAnalyticsSDK.GAProgressionStatus gAProgressionStatus = GameAnalyticsSDK
                .GAProgressionStatus
                .Start; //set this value when user starts the level.
            //GameAnalyticsSDK.GAProgressionStatus gAProgressionStatus = GameAnalyticsSDK.GAProgressionStatus.Fail; //set this value when user fails the level.
            //GameAnalyticsSDK.GAProgressionStatus gAProgressionStatus = GameAnalyticsSDK.GAProgressionStatus.Complete; //set this value when user completes the level.
            Progresion01 levelType = Progresion01.level; // For SIMPLE Levels set this
            //LevelType levelType = LevelType.bonus; // For BONUS Levels set this
            // AppCentral may require you to have multiple GameMode.
            // GameMode can refer to different WORLD or GAMEPLAY-SETTINGS.
            //
            // If (you have multiple world setup in your game)
            // {
            // set the "gameMode" to "GameMode.mode"
            // }else
            // {
            // set the "gameMode" to "GameMode.@default"
            // }
            Progresion02 gameMode = Progresion02.@default; // If you have only one GameMode setup in your game then set this.
            //GameMode gameMode = GameMode.mode; // If you have multiple GameMode setup in your game then set this.
            int gameModeNumber = 1; // if you have more then one GameMode set this value accordingly. NOTE Level number must start from 1 and not zero.

            AppCentralGameAnalyticsEvents.SendLevelProgressionEvent(
                gAProgressionStatus,
                levelType,
                gameMode,
                gameModeNumber
            );
        }

        public void OnLevelComplete()
        {
            //levelManager.Incerase_TotalLevelsPlayedSoFar();
            AppCentral.ShowInterstitalAdAtLevelCompletion(true, LevelCompeteCallback);

        }

        private void LevelCompeteCallback()
        {
            AppCentral.OnLevelComplete();
            AppCentralGameAnalyticsEvents.SendLevelProgressionEvent(
                GameAnalyticsSDK.GAProgressionStatus.Complete,
                Progresion01.level,
                Progresion02.mode,
                2
            );
            levelManager.InceraseLevel();
            //ResetLevel();
            OnLevelStrat();
        }

        public void OnLevelFail()
        {
            AppCentralGameAnalyticsEvents.SendLevelProgressionEvent(
                GameAnalyticsSDK.GAProgressionStatus.Fail,
                Progresion01.level,
                Progresion02.mode,
                2
            );
            ResetLevel();
        }

        public void OnLevelSkip()
        {
            AppCentralGameAnalyticsEvents.SendLevelSkippedEvent("Skip", AppCentral.CurrentLevelID);
            levelManager.InceraseLevel();
            ResetLevel();
        }

#endif

#if AC_APPLOVIN

        public void OnInitializeMAX()
        {
            ApplovinAdManager.Instance.InitializeAppLovin();
        }

        public void OnShowBannerAd()
        {
            AppCentral.ShowBannerAd();
        }

        public void OnShowAppOpenAd()
        {
            ApplovinAdManager.Instance.aL_AppOpenImpl.ShowAdIfReady();
        }   
        
        public void OnShowInterstitalAd()
        {
            AppCentral.ShowInterstitalAd();
        }

        bool canShowInter = false;

        private void SetshowAdAtThisLevelCompeteBool(bool status)
        {
            canShowInter = status;
            ACLogger.UserDebug("canShowInter=" + canShowInter);
        }

        public void OnShowInterstitalOnLevelCompAd()
        {
            AppCentral.ShowInterstitalAdAtLevelCompletion(
                canShowInter,
                OnShowInterstitalOnLevelCompAd_Callback
            );
        }

        private void OnShowInterstitalOnLevelCompAd_Callback()
        {
            ACLogger.UserDebug("OnShowInterstitalOnLevelCompAd_Callback");
        }

        public void OnShowRewardedAd()
        {
            AppCentral.ShowRewardedAd(RewaredResponce);
        }

        public void RewaredResponce(bool rewardStatus)
        {
            ACLogger.UserDebug(": Receive Reward Status " + rewardStatus);
            PresistanceCanvas.GH_WarningManager.Instance.Prompt(
                "Reward Receive Status=" + rewardStatus
            );

            if (rewardStatus)
            {
                // Reward to player
            }
            else
            {
                // Don't Reward to player
            }
        }

        public void OnHideBannerAd()
        {
            AppCentral.HideBannerAd();
        }

#endif

#if AC_IAPS

        public void OnShowDefulatPaywall()
        {
            AppCentral.ShowDefulatPaywall();
        }

        public void OnShowDynamicPaywall()
        {
            AppCentral.ShowDynamicPaywall();
        }

        public void OnShowMidGamePaywall()
        {
            AppCentral.ShowMidGamePaywall();
        }

        public void OnSubscriptionStatus()
        {
            PresistanceCanvas.GH_WarningManager.Instance.Prompt(
                "User Sub Status is" + AppCentral.IsSuscriptionActive()
            );

            if (AppCentral.IsSuscriptionActive())
            {
                ACLogger.UserDebug(": User Subscribed to one of the subscription");
            }
            else
            {
                ACLogger.UserDebug(": User Didn't Subscribed to any of the subscription");
            }
        }
#endif

        public void OnShowRateUS()
        {
            AppCentral.ShowRateUsMenu();
        }

        public void OnShowTFprompt()
        {
            AppCentral.CheckInternetConnection();
        }

        private void ResetLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        void Update()
        {
            if (AppCentralInAppPurchaser.IsPurchaseProcessing)
            {
                isIAP_Processing.text = "IAP is Processing";
            }
            else
            {
                isIAP_Processing.text = "No, IAP is Processing";
            }
        }
    }
}
