using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System;
using AppCentralCore;

namespace GH
{
    public class UI_DynamicRateUsMenu : MonoBehaviour
    {
        public enum RateUsMenuType
        {
            None,
            RatingPanel,
            WeAppreciate_TakeLocalreview,
            WeAppreciate_TakeAppStoreNative,
            ReviewWrtting_Panel
        }

        public const int RateUsStatus_NotShownYet = 0;
        public const int RateUsStatus_AlradyFrstShown = 1;
        public const int RateUsStatus_Recieived = 5;

        public UI_DynamicRateUsMenu_Stars Ratingpanel_Starts;
        public UI_DynamicRateUsMenu_Stars LocalReviewpanel_Starts;
        public UI_DynamicRateUsMenu_Stars Reviewpanel_Starts;

        public GameObject MainRateUSPanel;
        public GameObject RateUs_LaterPanel,
            WeAppreciate_TakeLocalreview,
            WeAppreciate_TakeAppStoreNative,
            CommonPanel,
            reviewPanel;

        public const string LastRateed_levelPref = "LastRateed_level";

        //public string isBundleLeader_Pref = "isBundleLeader";
        public const string RateUs_Status_Pref = "RateUs_Status";

        public GameObject LaterButton;
        public GameObject FeedbackPanel;

        public Action RateUSFinish_Event;

        public InputField inputField_Tittle;
        public InputField inputField_Review;

        public int ServerRateUs_LevelID = 0;
        public int currentLevelID = 1;
        public bool IsBundleLeadertest = true;

        public static int UserRating = 0;

        private Sprite GameIcon;
        private string GameName;

        public Image GameIconImg_Common;
        public Image GameIconImg_Other;
        public Text GameNameTxt;

        public bool CheckForRateUS(Action onCloseCallback, int LevelID)
        {
            AppCentralCore.newSetting setting = AppCentralCore.AppCentralSettings.LoadSetting();

            GameIcon = setting.GameIcon;

            GameIconImg_Common.sprite = GameIcon;
            GameIconImg_Other.sprite = GameIcon;

            GameName = setting.GameName;
            GameNameTxt.text = GameName;

            RateUSFinish_Event = onCloseCallback;
            
            bool IsRateUsShowing = false;

            int CurrentDayID = LevelID;

            int rateonlevel = AppCentralUnityApi_Internal
                .Instance
                .jsonController
                .JsonObject
                .rating
                .rateOnLevel;

            if (!PlayerPrefs.HasKey(LastRateed_levelPref))
            {
                // ACLogger.UserDebug("AAAAAAAAAA__1111111111");

                PlayerPrefs.SetInt(LastRateed_levelPref, rateonlevel);
                //PlayerPrefs.SetInt(LastRateed_levelPref, UI_Handler.CurrentDayID);
                PlayerPrefs.GetInt(RateUs_Status_Pref, RateUsStatus_NotShownYet);
            }

            // ACLogger.UserDebug("AAAAAAAAAA_LastRateed_levelPref" + PlayerPrefs.GetInt(LastRateed_levelPref));
            // ACLogger.UserDebug("AAAAAAAAAA_RateUs_Status_Pref" + PlayerPrefs.GetInt(RateUs_Status_Pref));



            ACLogger.UserDebug(
                ": RateUS shown last time at level="
                    + PlayerPrefs.GetInt(LastRateed_levelPref)
            );
            ACLogger.UserDebug(": RateUS value from server is=" + rateonlevel);
            ACLogger.UserDebug(": RateUS requested at level=" + CurrentDayID);

            if (
                PlayerPrefs.GetInt(RateUs_Status_Pref) == RateUsStatus_NotShownYet
                && rateonlevel == CurrentDayID
            )
            {
                Activate(RateUsMenuType.RatingPanel);
                PlayerPrefs.SetInt(LastRateed_levelPref, CurrentDayID);
                PlayerPrefs.SetInt(RateUs_Status_Pref, RateUsStatus_AlradyFrstShown);
                IsRateUsShowing = true;
            }
            else if (
                PlayerPrefs.GetInt(LastRateed_levelPref) + 10 == CurrentDayID
                && PlayerPrefs.GetInt(RateUs_Status_Pref) == RateUsStatus_AlradyFrstShown
            )
            {
                //Debug.LogError("AAAAAAAAAA_BBBBBBBBBBB");

                Activate(RateUsMenuType.RatingPanel);
                PlayerPrefs.SetInt(LastRateed_levelPref, CurrentDayID);
                PlayerPrefs.SetInt(RateUs_Status_Pref, RateUsStatus_AlradyFrstShown);
                IsRateUsShowing = true;
            }
            else { }

            //Debug.Log("AAAAAAAAAA_LastRateed_levelPref_2" + PlayerPrefs.GetInt(LastRateed_levelPref));
            //Debug.Log("AAAAAAAAAA_RateUs_Status_Pref+3" + PlayerPrefs.GetInt(RateUs_Status_Pref));


            if (IsRateUsShowing)
            {
                ACLogger.UserDebug(": RateUS Is showing");
                gameObject.SetActive(true);
            }
            else
            {
                ACLogger.UserDebug(": RateUS will not showing this time");
                Deactiavte();
            }

            return IsRateUsShowing;
        }

        public RateUsMenuType currentRateUsStatus;

        void Activate(RateUsMenuType rateUsMenuType)
        {
            currentRateUsStatus = rateUsMenuType;
            //CheckDarkOrLightMode();

            CommonPanel.SetActive(false);
            RateUs_LaterPanel.SetActive(false);
            WeAppreciate_TakeLocalreview.SetActive(false);
            WeAppreciate_TakeAppStoreNative.SetActive(false);
            reviewPanel.SetActive(false);

            gameObject.SetActive(true);
            MainRateUSPanel.SetActive(true);

            switch (rateUsMenuType)
            {
                case RateUsMenuType.None:
                    break;
                case RateUsMenuType.RatingPanel:

                    Ratingpanel_Starts.Initialize();

                    LaterButton.SetActive(true);

                    CommonPanel.SetActive(true);
                    RateUs_LaterPanel.SetActive(true);

                    break;
                case RateUsMenuType.WeAppreciate_TakeLocalreview:

                    LocalReviewpanel_Starts.Initialize(UserRating);
                    CommonPanel.SetActive(false);
                    WeAppreciate_TakeLocalreview.SetActive(true);

                    break;
                case RateUsMenuType.WeAppreciate_TakeAppStoreNative:

                    CommonPanel.SetActive(true);
                    Ratingpanel_Starts.Initialize(UserRating);
                    WeAppreciate_TakeAppStoreNative.SetActive(true);

                    break;
                case RateUsMenuType.ReviewWrtting_Panel:

                    Reviewpanel_Starts.Initialize(UserRating);
                    reviewPanel.SetActive(true);

                    break;
                default:
                    break;
            }
        }

        public Color LightMode_Color = Color.white;
        public Image RateUs_BG;

        public Color DarkMode_Color_Header = Color.black;
        public Color DarkMode_Color_WriteReview = Color.black;
        public Image ReviewHeader_BG;
        public Image ReviewWritten_BG;
        public bool isDarkMode_test;

        #region MainRateUs

        public void RateUsLater_ButonPressed()
        {
            ACLogger.UserError("RateUsLater_ButonPressed");
            PlayerPrefs.SetInt(RateUs_Status_Pref, RateUsStatus_AlradyFrstShown);
            Deactiavte();
        }

        public void ChoseStars(int p)
        {
            LaterButton.SetActive(false);
        }

        public void ReveivedPlayerRating()
        {
            Activate(RateUsMenuType.WeAppreciate_TakeAppStoreNative);

            //if (UserRating < 4)
            //{
            //    Activate(RateUsMenuType.WeAppreciate_TakeLocalreview);
            //}
            //else
            //{
            //    Activate(RateUsMenuType.WeAppreciate_TakeAppStoreNative);
            //}
        }

        #endregion


        public void Submit_rating()
        {
            if (UserRating < 4)
            {
                Activate(RateUsMenuType.WeAppreciate_TakeLocalreview);
            }
            else
            {
                GotoRateUsPage();
                Send_RattinginfoTo_Appcentral();
                Deactiavte();
            }

            Send_RattinginfoTo_Appcentral();
        }

        public void Activate_ReviewWrtting_Panel()
        {
            Activate(RateUsMenuType.ReviewWrtting_Panel);
        }

        public void Send_Review()
        {
            Update_RateUsStausTo_Received();
            Send_ReviewinfoTo_Appcentral();
            StartCoroutine(ShowRateUSFeedback());
            //Deactiavte();
        }

        public void NotNow_ButtonPressed()
        {
            PlayerPrefs.SetInt(RateUs_Status_Pref, RateUsStatus_AlradyFrstShown);
            Deactiavte();
        }

        public void Ok_ButtonPresses_LocalReviewPrompt_Panel()
        {
            //Deactiavte();
            Update_RateUsStausTo_Received();
            StartCoroutine(ShowRateUSFeedback());
        }

        void GotoRateUsPage()
        {
#if UNITY_IOS
            // UnityEngine.iOS.Device.RequestStoreReview();
            SendUserToActualValidRateUsLocation();
#endif
            Update_RateUsStausTo_Received();
        }

        const string app = "app";
        const string bundle = "bundle";
        const string appToRate_pref = "appToRate";
        const string bundleID_pref = "bundleID";

        void SendUserToActualValidRateUsLocation()
        {
            string appToRate = PlayerPrefs.GetString(appToRate_pref);
            string bundleIDToRate = PlayerPrefs.GetString(bundleID_pref);

            ACLogger.UserDebug(": appToRate:" + appToRate);
            ACLogger.UserDebug(": bundleIDToRate:" + bundleIDToRate);

            if (appToRate == app)
            {
                UnityEngine.iOS.Device.RequestStoreReview();
            }
            else if (appToRate == bundle)
            {
                string bundleRatingURL =
                    $"itms-apps:itunes.apple.com/app-bundle/id{bundleIDToRate}?action=write-review";

                ACLogger.UserDebug(": bundle rating url:" + bundleRatingURL);

                Application.OpenURL(bundleRatingURL);
            }
        }

        void Update_RateUsStausTo_Received()
        {
            PlayerPrefs.SetInt(RateUs_Status_Pref, RateUsStatus_Recieived);
            ACLogger.UserDebug("Rating=" + "Received");
        }

        void Send_RattinginfoTo_Appcentral()
        {
            ACLogger.UserDebug("Rating=" + "Snedto appcentral=" + UserRating);

            AppCentralCore.AppCentralPixelController.Instance.SaveAppCentralPixel(
                "rating_pixel",
                new string[] { "rating" },
                new string[] { UserRating.ToString() }
            );
        }

        void Send_ReviewinfoTo_Appcentral()
        {
            string totalReview =
                "Tittle: " + inputField_Tittle.text + ", Review: " + inputField_Review.text;
            string ReviewTittleTxt = inputField_Tittle.text;
            string totalReviewTxt = inputField_Review.text;

            AppCentralCore.AppCentralPixelController.Instance.SaveAppCentralPixel(
                "review_pixel",
                new string[] { "title", "review" },
                new string[] { ReviewTittleTxt, totalReviewTxt }
            );

            ACLogger.UserDebug("Rating=" + " review Send to appcentral=" + totalReview);
        }

        void Deactiavte()
        {
            CommonPanel.SetActive(false);
            RateUs_LaterPanel.SetActive(false);
            WeAppreciate_TakeLocalreview.SetActive(false);
            WeAppreciate_TakeAppStoreNative.SetActive(false);
            reviewPanel.SetActive(false);

            gameObject.SetActive(false);
            RaiseRateUsCloseEvent();

        }


        private void RaiseRateUsCloseEvent()
        {
            RateUSFinish_Event?.Invoke();
            
            ACLogger.UserDebug("OnRateUsClose_Callback");
            
            RateUSFinish_Event = null;
        }

        IEnumerator ShowRateUSFeedback()
        {
            CommonPanel.SetActive(false);
            MainRateUSPanel.SetActive(false);
            RateUs_LaterPanel.SetActive(false);
            WeAppreciate_TakeLocalreview.SetActive(false);
            WeAppreciate_TakeAppStoreNative.SetActive(false);
            reviewPanel.SetActive(false);

            yield return new WaitForSeconds(0.5f);

            FeedbackPanel.SetActive(true);

            yield return new WaitForSeconds(1.5f);

            FeedbackPanel.SetActive(false);
            Deactiavte();
        }
    }
}
