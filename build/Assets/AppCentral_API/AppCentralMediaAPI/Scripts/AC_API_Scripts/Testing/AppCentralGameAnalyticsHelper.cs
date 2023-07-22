using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using AppCentralAPI;
using AppCentralCore;
using System;

namespace AppCentralTesting
{

    public class AppCentralGameAnalyticsHelper : MonoBehaviour
    {
        public LevelManager levelManager;
        public Text LevelText;
        public Button LevelStrat, LevelComplete, LevelFail, LevelSkip;
        public Button SL_InterstitalAdEvent, SL_RewardedAdEvent;
        //public Button customDiemtion01_theme1, customDiemtion01_theme2;


        private void Start()
        {
            levelManager.initialize();
            LevelText.text = "Level" + AppCentral.CurrentLevelID.ToString();

#if AC_GAMEANALYTICS

            LevelStrat.onClick.AddListener(OnLevelStrat);
            LevelComplete.onClick.AddListener(OnLevelComplete);
            LevelFail.onClick.AddListener(OnLevelFail);
            LevelSkip.onClick.AddListener(OnLevelSkip);

#endif



            SL_InterstitalAdEvent.onClick.AddListener(OnSL_InterstitalAdEvent);
            SL_RewardedAdEvent.onClick.AddListener(OnSL_RewardedAdEvent);


            LevelStrat.GetComponentInChildren<Text>().text = "OnLevelStrat";
            LevelComplete.GetComponentInChildren<Text>().text = "OnLevelComplete";
            LevelFail.GetComponentInChildren<Text>().text = "OnLevelFail";
            LevelSkip.GetComponentInChildren<Text>().text = "OnLevelSkip";

            SL_InterstitalAdEvent.GetComponentInChildren<Text>().text = "OnSL_InterstitalAdEvent";
            SL_RewardedAdEvent.GetComponentInChildren<Text>().text = "OnSL_RewardedAdEvent";

        }

        private void OnSL_RewardedAdEvent()
        {
            AppCentralSmartLookEventsInternal.SmartLookTrack_RewardedAd();
        }

        private void OnSL_InterstitalAdEvent()
        {
            AppCentralSmartLookEventsInternal.SmartLookTrack_InterstitialAd();
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
            AppCentralGameAnalyticsEvents.SendLevelProgressionEvent(GameAnalyticsSDK.GAProgressionStatus.Start,
                                                                    Progresion01.level,
                                                                    Progresion02.mode, 2);

        }
        public void OnLevelComplete()
        {
            //levelManager.Incerase_TotalLevelsPlayedSoFar();
            AppCentralGameAnalyticsEvents.SendLevelProgressionEvent(GameAnalyticsSDK.GAProgressionStatus.Complete,
                                                                    Progresion01.level,
                                                                    Progresion02.mode, 2);
            levelManager.InceraseLevel();
            ResetLevel();
        }

        public void OnLevelFail()
        {
            AppCentralGameAnalyticsEvents.SendLevelProgressionEvent(GameAnalyticsSDK.GAProgressionStatus.Fail,
                                                        Progresion01.level,
                                                        Progresion02.mode, 2);
            ResetLevel();
        }
        public void OnLevelSkip()
        {
            AppCentralGameAnalyticsEvents.SendLevelSkippedEvent("Skip",AppCentral.CurrentLevelID);
            OnLevelComplete();
        }

#endif

        private void ResetLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
