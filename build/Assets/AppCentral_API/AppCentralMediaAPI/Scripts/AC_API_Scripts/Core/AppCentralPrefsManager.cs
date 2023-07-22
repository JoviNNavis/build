using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AppCentralCore
{
    public class AppCentralPrefsManager : MonoBehaviour
    {
        public const string brightdata_show_on_start = "brightdata_show_on_start";
        public const string brightdata_show_in_levels = "brightdata_show_in_levels";
        public const string userData_app_session_number = "userData_app_session_number";

        public const string app_open_ad = "app_open_ad";


        public static void PopulatePlayerPrefsWithDefaultSettings(
            AppCentral_JSON_Controller.API_Variables aPI_Variables
        )
        {
            ACLogger.UserDebug(": PopulatePlayerPrefsWithDefaultSettings ");

            ////// Save default values to PlayerPrefs
            // Theme
            PlayerPrefs.SetString("theme_main", "theme2");
            //// Paywall
            /// Default
            PlayerPrefs.SetInt("paywall_modal_presentation_style", 6);
            PlayerPrefs.SetString("paywall_show_location", "deepInGame");
            PlayerPrefs.SetInt("paywall_xCountdown", 5);
            // Product
            PlayerPrefs.SetString(
                "paywall_product_id",
                aPI_Variables.package + ".default_subscription"
            );
            PlayerPrefs.SetString("paywall_statistics_tag", "default_subscription");
            PlayerPrefs.SetString("paywall_button_text", "Subscribe");
            PlayerPrefs.SetString("paywall_button_description", "Start Your Subscription");
            /// Dynamic - this is the only variable needed to know that there is no dynamic paywall
            PlayerPrefs.SetString("paywall_dynamic_show_location", "");
            /// MidGame - this is the only variable needed to know that there is no midgame paywall
            PlayerPrefs.SetString("paywall_midgame_show_in_levels", "");
            // AppJump
            PlayerPrefs.SetString("appjump_target_app", "");
            PlayerPrefs.SetInt("appjump_period_between_jumps", 300);
            // AllowTracking
            PlayerPrefs.SetString("allowtracking_show_location", "onLoad");
            // Subscription
            PlayerPrefs.SetInt("isSubscribed", 0);
            // Campaign
            PlayerPrefs.SetString("campaign_name", "");
            // Rating
            PlayerPrefs.SetInt("rateOnLevel", 3);
            PlayerPrefs.SetString("ratingType", "native");
            PlayerPrefs.SetString("appToRate", "app");
            PlayerPrefs.SetInt("isBundleLeader", 0);
            // Recording
            //PlayerPrefs.SetInt("recording_recordOnceIn", 20); // ** not being saved intentionally
            // Ads
            PlayerPrefs.SetInt("ads_showAds", 1);
            PlayerPrefs.SetInt("ads_showOnLevelStart", 0);
            PlayerPrefs.SetInt("ads_extraAds", 0);
            // Push Notifications
            PlayerPrefs.SetString("pushNotifications_show_location", "onLoad");
            // User-Data
            PlayerPrefs.SetInt(userData_app_session_number, 0);

            // Purchase Popup
            PlayerPrefs.SetString("purchasePopUp_showInLevels", "");

            // Ads
            PlayerPrefs.SetInt(app_open_ad, 0);
        }

        public static void SaveAppCentralUnityApiDataToPlayerPrefs(
            AppCentral_JSON_Controller.API_Variables aPI_Variables,
            AppCentralCore.AppCentralUnityApiJsonDetails jsonObject
        )
        {
            ACLogger.UserDebug(
                "AppCentral_API SaveAppCentralUnityApiDataToPlayerPre= " + aPI_Variables.apiText
            );

            try
            {
                ////// Save data recieved from AppCentral to PlayerPrefs
                // Theme
                PlayerPrefs.SetString("theme_main", jsonObject.theme.main);
                //// Paywall
                /// Default
                PlayerPrefs.SetInt(
                    "paywall_modal_presentation_style",
                    jsonObject.paywalls.@default.modalPresentationStyle
                );
                PlayerPrefs.SetString(
                    "paywall_show_location",
                    jsonObject.paywalls.@default.showLocation[0]
                );
                PlayerPrefs.SetInt("paywall_xCountdown", jsonObject.paywalls.@default.xCountdown);
                // Product
                PlayerPrefs.SetString(
                    "paywall_product_id",
                    jsonObject.paywalls.@default.products[0].productId
                );
                PlayerPrefs.SetString(
                    "paywall_statistics_tag",
                    jsonObject.paywalls.@default.products[0].statisticsTag
                );
                PlayerPrefs.SetString(
                    "paywall_button_text",
                    jsonObject.paywalls.@default.products[0].buttonText
                );
                PlayerPrefs.SetString(
                    "paywall_button_description",
                    jsonObject.paywalls.@default.products[0].buttonDescription
                );
                /// Dynamic
                //if (aPI_Variables.apiText.Contains("dynamic\":{\"modalPresentationStyle"))
                if (!string.IsNullOrEmpty(jsonObject.paywalls.dynamic.products[0].productId))
                {
                    ACLogger.UserDebug(
                        ": paywall_dynamic_product_id"
                            + jsonObject.paywalls.dynamic.products[0].productId
                    );
                    // Dynamic paywall exists - save dynamic paywall data
                    PlayerPrefs.SetInt(
                        "pawall_dynamic_modal_presentation_style",
                        jsonObject.paywalls.dynamic.modalPresentationStyle
                    );
                    PlayerPrefs.SetString(
                        "paywall_dynamic_show_location",
                        jsonObject.paywalls.dynamic.showLocation[0]
                    );
                    PlayerPrefs.SetInt(
                        "paywall_dynamic_xCountdown",
                        jsonObject.paywalls.dynamic.xCountdown
                    );
                    // Product
                    PlayerPrefs.SetString(
                        "paywall_dynamic_product_id",
                        jsonObject.paywalls.dynamic.products[0].productId
                    );
                    PlayerPrefs.SetString(
                        "paywall_dynamic_statistics_tag",
                        jsonObject.paywalls.dynamic.products[0].statisticsTag
                    );
                    PlayerPrefs.SetString(
                        "paywall_dynamic_button_text",
                        jsonObject.paywalls.dynamic.products[0].buttonText
                    );
                    PlayerPrefs.SetString(
                        "paywall_dynamic_button_description",
                        jsonObject.paywalls.dynamic.products[0].buttonDescription
                    );

                    setupDynamicPaywallShowLocations(jsonObject.paywalls.dynamic.showLocation);
                }
                else
                {
                    // Dynamic paywall doesn't exist - reset paywall_dynamic_show_location
                    PlayerPrefs.SetString("paywall_dynamic_show_location", "");
                    setupDynamicPaywallShowLocations(jsonObject.paywalls.dynamic.showLocation);
                }
                /// MidGame
                //if (aPI_Variables.apiText.Contains("midgame\":{\"modalPresentationStyle"))
                if (!string.IsNullOrEmpty(jsonObject.paywalls.midgame.products[0].productId))
                {
                    ACLogger.UserDebug(
                        ": paywall_midgame_product_id"
                            + jsonObject.paywalls.midgame.products[0].productId
                    );

                    PlayerPrefs.SetInt(
                        "pawall_midgame_modal_presentation_style",
                        jsonObject.paywalls.midgame.modalPresentationStyle
                    );
                    PlayerPrefs.SetString(
                        "paywall_midgame_show_in_levels",
                        jsonObject.paywalls.midgame.showInLevels
                    );
                    PlayerPrefs.SetString(
                        "paywall_midgame_ads_interaction",
                        jsonObject.paywalls.midgame.adsInteraction
                    );
                    PlayerPrefs.SetInt(
                        "paywall_midgame_xCountdown",
                        jsonObject.paywalls.midgame.xCountdown
                    );
                    // Product
                    PlayerPrefs.SetString(
                        "paywall_midgame_product_id",
                        jsonObject.paywalls.midgame.products[0].productId
                    );
                    PlayerPrefs.SetString(
                        "paywall_midgame_statistics_tag",
                        jsonObject.paywalls.midgame.products[0].statisticsTag
                    );
                    PlayerPrefs.SetString(
                        "paywall_midgame_button_text",
                        jsonObject.paywalls.midgame.products[0].buttonText
                    );
                    PlayerPrefs.SetString(
                        "paywall_midgame_button_description",
                        jsonObject.paywalls.midgame.products[0].buttonDescription
                    );
                }
                else
                {
                    // Midgame paywall doesn't exist - reset paywall_midgame_show_in_levels
                    PlayerPrefs.SetString("paywall_midgame_show_in_levels", "");
                }

                // AppJump
                PlayerPrefs.SetString("appjump_target_app", jsonObject.appJump.targetApp);
                PlayerPrefs.SetInt(
                    "appjump_period_between_jumps",
                    jsonObject.appJump.periodBetweenJumps
                );
                // AllowTracking
                PlayerPrefs.SetString(
                    "allowtracking_show_location",
                    jsonObject.allowTracking.showLocation[0]
                );
                // Subscription
                PlayerPrefs.SetInt("isSubscribed", jsonObject.subscription.isSubscribed);
                // Campaign
                PlayerPrefs.SetString("campaign_name", jsonObject.campaign.name);
                // Rating
                PlayerPrefs.SetInt("rateOnLevel", jsonObject.rating.rateOnLevel);
                PlayerPrefs.SetString("ratingType", jsonObject.rating.ratingType);
                PlayerPrefs.SetString("appToRate", jsonObject.rating.appToRate);
                PlayerPrefs.SetString("bundleID", jsonObject.rating.bundleID);
                if (jsonObject.rating.isBundleLeader)
                    PlayerPrefs.SetInt("isBundleLeader", 1);
                else
                    PlayerPrefs.SetInt("isBundleLeader", 0);
                // Recording
                PlayerPrefs.SetInt("recording_enableRecording",jsonObject.recording.enableRecording);
                PlayerPrefs.SetInt("recording_recordOnceIn", jsonObject.recording.recordOnceIn);
                // Ads
                PlayerPrefs.SetInt("ads_showAds", jsonObject.ads.showAds);
                if (jsonObject.ads.showOnLevelStart)
                    PlayerPrefs.SetInt("ads_showOnLevelStart", 1);
                else
                    PlayerPrefs.SetInt("ads_showOnLevelStart", 0);
                if (jsonObject.ads.extraAds)
                    PlayerPrefs.SetInt("ads_extraAds", 1);
                else
                    PlayerPrefs.SetInt("ads_extraAds", 0);
                // Push Notifications
                PlayerPrefs.SetString(
                    "pushNotifications_show_location",
                    jsonObject.pushNotification.showLocation[0]
                );
                // User-Data
                PlayerPrefs.SetInt(
                    userData_app_session_number,
                    jsonObject.userData.appSessionNumber
                );

                ACLogger.UserDebug(": playerPrefs - AppCentral settings");

                // Purchase Popup
                PlayerPrefs.SetString(
                    "purchasePopUp_showInLevels",
                    jsonObject.purchasePopUp.showInLevels
                );
                PlayerPrefs.SetString(
                    "purchasePopUp_productID",
                    jsonObject.purchasePopUp.productId
                );

                //Bright Data
                PlayerPrefs.SetInt(brightdata_show_on_start, jsonObject.brightData.showOnStart);
                PlayerPrefs.SetString(
                    brightdata_show_in_levels,
                    jsonObject.brightData.showInLevels
                );

                // Ads
                PlayerPrefs.SetInt(app_open_ad, jsonObject.ads.appOpenAd);
            }
            catch
            {
                // Json received is incorrect
                ACLogger.UserDebug(": playerPrefs - Json received is incorrect");
            }
        }











        private const string contains_onLoad_pref = "contains_onLoad";
        private const string contains_startPlay_pref = "contains_startPlay";
        private const string contains_deepInGame_pref = "contains_deepInGame";
        private const string contains_adsUnavailable_pref = "contains_adsUnavailable";

        private static void setupDynamicPaywallShowLocations(List<string> ShowLocations)
        {
            bool contains_onLoad = false;
            bool contains_startPlay = false;
            bool contains_deepInGame = false;
            bool contains_adsUnavailable = false;

            contains_onLoad = ShowLocations.Contains("onLoad");
            contains_startPlay = ShowLocations.Contains("startPlay");
            contains_deepInGame = ShowLocations.Contains("deepInGame");
            contains_adsUnavailable = ShowLocations.Contains("adsUnavailable");

            ACLogger.UserDebug("contains_onLoad:" + contains_onLoad);
            ACLogger.UserDebug("contains_startPlay:" + contains_startPlay);
            ACLogger.UserDebug("contains_deepInGame:" + contains_deepInGame);
            ACLogger.UserDebug("contains_adsUnavailable:" + contains_adsUnavailable);

            if (contains_onLoad)
                PlayerPrefs.SetInt(contains_onLoad_pref, 1);
            else
                PlayerPrefs.SetInt(contains_onLoad_pref, 0);

            if (contains_startPlay)
                PlayerPrefs.SetInt(contains_startPlay_pref, 1);
            else
                PlayerPrefs.SetInt(contains_startPlay_pref, 0);

            if (contains_deepInGame)
                PlayerPrefs.SetInt(contains_deepInGame_pref, 1);
            else
                PlayerPrefs.SetInt(contains_deepInGame_pref, 0);

            if (contains_adsUnavailable)
                PlayerPrefs.SetInt(contains_adsUnavailable_pref, 1);
            else
                PlayerPrefs.SetInt(contains_adsUnavailable_pref, 0);
        }

        public static bool IsContain_onLoad()
        {
            bool DoesContains = false;

            if (PlayerPrefs.GetInt(contains_onLoad_pref, 0) == 1)
                DoesContains = true;

            return DoesContains;
        }

        public static bool IsContain_startPLay()
        {
            bool DoesContains = false;

            if (PlayerPrefs.GetInt(contains_startPlay_pref, 0) == 1)
                DoesContains = true;

            return DoesContains;
        }

        public static bool IsContain_deepInGame()
        {
            bool DoesContains = false;

            if (PlayerPrefs.GetInt(contains_deepInGame_pref, 0) == 1)
                DoesContains = true;

            return DoesContains;
        }

        public static bool IsContain_adsUnavailable()
        {
            bool DoesContains = false;

            if (PlayerPrefs.GetInt(contains_adsUnavailable_pref, 0) == 1)
                DoesContains = true;

            return DoesContains;
        }
    }
}
