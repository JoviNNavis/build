using System.Collections.Generic;
using System.Collections;
using AppCentralAPI;
using UnityEngine;
using System.Linq;
using System;
using Brdsdk;

namespace AppCentralCore
{
    public class AppCentralBrightDataController : MonoBehaviour
    {
        private const string ACBD_UserConsentPref = "ACBD_UserConsent";

        int CurrentlevelID = 0;
        bool isInitialized = false;

        void OnEnable()
        {
            AppCentralUnityApi_Internal.RecievedAppCentralApiResponse +=
                RecievedAppCentralApiResponse;
            AppCentralUnityApi_Internal.OnLevelCompleteEvent += OnLevelCompleteEvent;
        }

        void OnDisable()
        {
            AppCentralUnityApi_Internal.RecievedAppCentralApiResponse -=
                RecievedAppCentralApiResponse;

            AppCentralUnityApi_Internal.OnLevelCompleteEvent -= OnLevelCompleteEvent;
        }

        void RecievedAppCentralApiResponse(bool responce)
        {
            bool IsEnableFromServer =
                PlayerPrefs.GetInt(AppCentralPrefsManager.brightdata_show_on_start) == 1
                    ? true
                    : false;

            CurrentlevelID = 0; // If one of the values in brightdata_show_in_levels is "0", show the BrightData permission as soon as the loading screen ends and the game scene loads (Make sure the brightData initializaiton is happening before showing the dynamic paywall when the showLocation is equal to onLoad).

            bool IsAValidLevel = IsValideLevel(CurrentlevelID);

            ACLogger.UserDebug("BrightData SDK IsEnableFromServer: " + IsEnableFromServer);
            ACLogger.UserDebug("BrightData SDK IsAValidLevel: " + IsAValidLevel);

            if (IsEnableFromServer)
            {
                InitializeBrightData();
            }

            if (IsAValidLevel)
            {
                OnLevelCompleteEvent(0);
            }
        }

        void OnLevelCompleteEvent(int levelID)
        {
            CurrentlevelID = levelID;

            bool IsEnableFromServer =
                PlayerPrefs.GetInt(AppCentralPrefsManager.brightdata_show_on_start) == 1
                    ? true
                    : false;
            bool IsAValidLevel = IsValideLevel(CurrentlevelID);

            ACLogger.UserDebug("BrightData SDK IsEnableFromServer: " + IsEnableFromServer);
            ACLogger.UserDebug("BrightData SDK IsAValidLevel: " + IsAValidLevel);

            if (!isInitialized)
            {
                InitializeBrightData();
                ACLogger.UserDebug("BrightData SDK Is Not Initialized");
                return;
            }


            if (IsEnableFromServer && IsAValidLevel && !GetConcentStatus())
            {
                ShowConcent();
            }
        }

        void InitializeBrightData()
        {
            /// Initialize the SDK
            string language = null;
            bool skip_consent = true;
            BrdsdkBridge.init(
                "To support this app",
                "I Agree",
                "I disagree",
                choiceChanged,
                skip_consent,
                language
            );
            isInitialized = true;
            ACLogger.UserDebug("SDK Status: " + BrdsdkBridge.get_choice());
        }

        /// <summary>
        /// The method is called when user's choice is changed.
        /// </summary>
        /// <param name="choice">Value representing the user's choice:
        /// - BrdsdkBridge.CHOICE_NONE - the consent screen is not yet shown;
        /// - BrdsdkBridge.CHOICE_AGREED - user accepted the consent screen;
        /// - BrdsdkBridge.CHOICE_DISAGREED - user declined the consent screen.
        /// </param>
        void choiceChanged(int choice)
        {
            ACLogger.UserDebug("SDK Status: " + BrdsdkBridge.get_choice());
            BrightDataUserConsentChoice _choice = BrightDataUserConsentChoice.declined;
            if (choice == BrdsdkBridge.CHOICE_AGREED)
            {
                ACLogger.UserDebug("Skip ads (SDK is enabled)");
                _choice = BrightDataUserConsentChoice.accepted;
            }
            else
            {
                ACLogger.UserDebug("TODO Initialize ads (SDK is disabled)");
                _choice = BrightDataUserConsentChoice.declined;
            }

            AppCentralAPI.AppCentral.OnBrightSDKConsentStatusChange?.Invoke(_choice);
            SendBrightDataConsentPixel(_choice, CurrentlevelID);
        }

        public static void UserOptOut()
        {
            BrdsdkBridge.opt_out();
        }

        public static void ShowConcent()
        {
            BrdsdkBridge.show_consent();
        }

        public static string ShowConcentStatus()
        {
            int choice = BrdsdkBridge.get_choice();
            if (choice == BrdsdkBridge.CHOICE_AGREED)
            {
                return "CHOICE_AGREED";
            }
            else
            {
                return "CHOICE_DISAGREED/CHOICE_NONE";
            }
        }

        public static bool GetConcentStatus()
        {
            int choice = BrdsdkBridge.get_choice();
            if (choice == BrdsdkBridge.CHOICE_AGREED)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsValideLevel_Old(int CurrentLevelIndex)
        {
            bool canShow = false;

            string levelsList = PlayerPrefs.GetString(
                AppCentralPrefsManager.brightdata_show_in_levels
            );

            if (!string.IsNullOrEmpty(levelsList))
            {
                List<string> allLevels = levelsList.Split(',').ToList<string>();

                if (allLevels.Contains(CurrentLevelIndex.ToString()))
                {
                    canShow = true;
                }
            }

            return canShow;
        }

        private bool IsValideLevel(int CurrentLevelIndex)
        {
            bool canShow = false;

            string levelsList = PlayerPrefs.GetString(
                AppCentralPrefsManager.brightdata_show_in_levels
            );

            bool containComas = levelsList.Contains(',');

            ACLogger.UserDebug("BrightData SDK CurrentLevelIndex: " + CurrentLevelIndex);
            ACLogger.UserDebug("BrightData SDK levelsList: " + levelsList);
            ACLogger.UserDebug("BrightData SDK containComas: " + containComas);

            if (!string.IsNullOrEmpty(levelsList))
            {
                if (containComas)
                {
                    List<string> allLevels = levelsList.Split(',').ToList<string>();

                    if (allLevels.Contains(CurrentLevelIndex.ToString()))
                    {
                        canShow = true;
                    }
                }
                else
                {
                    if (CurrentLevelIndex == int.Parse(levelsList))
                    {
                        canShow = true;
                    }
                }
            }

            ACLogger.UserDebug("BrightData SDK canShow: " + canShow);
            return canShow;
        }

        private void SendBrightDataConsentPixel(BrightDataUserConsentChoice userChoice, int levelID)
        {
            string type = "BrightData";
            string choice = userChoice.ToString(); // accepted/declined
            string choiceLocation = levelID.ToString(); // 0 = onStart, other integers refer to the level number
            int sessionNum = PlayerPrefs.GetInt(
                AppCentralPrefsManager.userData_app_session_number,
                1
            ); // the number of the current session

            if (levelID == 0)
            {
                choiceLocation = "onStart";
            }

            AppCentralPixelController.Instance.SaveAppCentralPixel(
                "permission_pixel",
                new string[] { "type", "action", "choice_location", "session_number" },
                new string[] { type, choice, choiceLocation, sessionNum.ToString() }
            );
        }
    }
}
