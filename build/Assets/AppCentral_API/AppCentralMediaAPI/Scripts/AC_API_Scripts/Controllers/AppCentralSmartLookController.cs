using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.iOS;

namespace AppCentralCore
{
    public class AppCentralSmartLookController : MonoBehaviour
    {
        public const string recording_recordOnceIn_Pref = "recording_recordOnceIn";
        public const string session_number_Pref = "session_number";
        public const string usedData_app_session_number_Pref = "usedData_app_session_number";

        public static void Initialize_SmartLook()
        {

#if AC_SMARTLOOK


            ACLogger.UserDebug(": Initialize_SmartLook called");

            newSetting settings = AppCentralSettings.LoadSetting();

            if (!settings.canUseSmartLook)
            {
                ACLogger.UserDebug(": SmartLook is Disabled from AppCentral Settings");
                return;
            }

            string API_KEY = settings.SmartLookKey; // Spin The ball

            ACLogger.UserDebug(": SmartLook is enabled from AppCentral Settings with SmartLookKey=" + API_KEY);

#if UNITY_IOS

            try
            {
                #region Smartlook

                int enableRecording = PlayerPrefs.GetInt("recording_enableRecording", 0);

                ACLogger.UserDebug(": SmartLook recording_enableRecording=" + enableRecording);


                if (enableRecording == 1)
                {
                    // Get the num that indicates how often to record
                    int recordOnceIn = PlayerPrefs.GetInt(recording_recordOnceIn_Pref, 1); // - 1st time opening the app will always record
                    ACLogger.UserDebug(": SmartLook recordOnceIn=" + recordOnceIn);


                    // Get the current session number
                    int sessionNum = PlayerPrefs.GetInt(session_number_Pref, 1);
                    if (sessionNum % recordOnceIn == 0 || sessionNum == 1)
                    {
                        ACLogger.UserDebug(": SmartLook recording this time");


                        // Setup and Start Recording SmartLook session
                        SmartlookUnity.SetupOptionsBuilder builder =
                            new SmartlookUnity.SetupOptionsBuilder(API_KEY);
                        SmartlookUnity.Smartlook.SetupAndStartRecording(builder.Build());
                        // Reset session (to make sure it's a new session every time the app opens after being closed)
                        SmartlookUnity.Smartlook.ResetSession(false);
                        // Set Session Identifier
                        SmartlookUnity.Smartlook.SetUserIdentifier(
                            Device.vendorIdentifier.ToString()
                        );
                        // Ignore automatic navigation events
                        SmartlookUnity.Smartlook.setEventTrackingMode(
                            SmartlookUnity.Smartlook.EventTrackingMode.IGNORE_NAVIGATION_INTERACTION
                        );
                    }
                    // Increment Session Number
                    sessionNum++;
                    PlayerPrefs.SetInt(session_number_Pref, sessionNum);
                }
                #endregion
            }
            catch {
                ACLogger.UserDebug(": SmartLook recording caught exception raised");
            }
#endif

#endif
        }

        public static void SmartLookTrackCampaign()
        {
#if AC_SMARTLOOK

            SmartlookUnity.Smartlook.TrackCustomEvent(
                "campaign",
                PlayerPrefs.GetString("campaign_name")
            );

#endif
        }
    }
}
