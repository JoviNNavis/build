using UnityEngine;
using System;

namespace AppCentralCore
{
    public class AppCentralAppJumpController : MonoBehaviour
    {
        // This is an EXAMPLE variable, the periodBetweenJumps value should be taken from 'appJump.periodBetweenJumps'.
       static long periodBetweenJumps = 300; // This value is the default value and an example of what you get from 'appJump.periodBetweenJumps',  
                                       // when comparing to the 'difference.Ticks' we will multiply 'appJump.periodBetweenJumps'
                                       // value by 10,000,000 (bringing it back to a suitable format).

        //  Ticks timing information:
        //  Ticks per day            864,000,000,000
        //  Ticks per hour            36,000,000,000
        //  Ticks per minute             600,000,000
        //  Ticks per second              10,000,000
        //  Ticks per millisecond             10,000

       static  private string appjump_target_app;
/*        private void OnEnable()
        {
            AppCentralUnityApi_Internal.OnAppLaunch += UpdateAppJumpSetting;
            AppCentralUnityApi_Internal.OnLoadEvent += UpdateAppJumpSetting;

            AppCentralUnityApi_Internal.OnAppLaunch += CheckChainJumping;
            AppCentralUnityApi_Internal.OnLoadEvent += TryAppJumping;
        }*/


        public static void UpdateAppJumpSetting()
        {
            periodBetweenJumps = AppCentralUnityApi_Internal.Instance.getJsonObj().appJump.periodBetweenJumps;
            appjump_target_app = AppCentralUnityApi_Internal.Instance.getJsonObj().appJump.targetApp;
        }

        // run this function before AppCentral unity-api call
        public static void CheckChainJumping()
        {
            ACLogger.UserDebug("CheckChainJumping Called");

            var absoluteURL = Application.absoluteURL;

            ACLogger.UserDebug("[ChainJumping] absoluteURL=" + absoluteURL);

            if (absoluteURL == "")
            {
                //app opened from icon
            }
            else
            {
                //app opened form jumping
                if (absoluteURL.Split(new[] { '?' }, 2).Length > 1)
                {
                    string jumpTarget = absoluteURL.Split(new[] { '?' }, 2)[1];
                    //jumping
                    Application.OpenURL(jumpTarget);
                }
                else
                {
                    //no more jumping targets
                }
            }

            AppCentralUnityApi_Internal.AppJumpFinished();
        }

        public static void TryAppJumping()
        {

            ACLogger.UserDebug("Appcentral_API TryAppJumping Called");

#if UNITY_EDITOR
            //appjump_target_app = "";
#endif

            ACLogger.UserDebug(": AppJumpController appjump_target_app=" + appjump_target_app);


            // Check if targetApp isn't null, otherwise there is no point of appJumping
            if (!string.IsNullOrEmpty(appjump_target_app))
            {
                // Check if lastJumpDate is empty, if it is empty - do appJump, if it is not - compare dates and do AppJump if needed.
                if (string.IsNullOrEmpty(PlayerPrefs.GetString("lastJumpDate")))
                {
                    DoAppJump();
                }
                else
                {
                    CompareDates();
                }
            }
        }

        private static void CompareDates()
        {
            // Get the current time
            DateTime currentDate = System.DateTime.Now;

            ACLogger.UserDebug(": AppJumpController currentDate=" + currentDate);

            // Get the lastJumpDate
            long temp = Convert.ToInt64(PlayerPrefs.GetString("lastJumpDate"));
            ACLogger.UserDebug(": AppJumpController temp=" + temp);

            // Convert the lastJumpDate from binary to a DataTime variable
            DateTime lastJumpDate = DateTime.FromBinary(temp);
            ACLogger.UserDebug(": AppJumpController lastJumpDate=" + lastJumpDate);

            // Use the Subtract method to get the difference between the currentDate and 
            // lastJumpDate and store the result as a TimeSpan variable
            TimeSpan difference = currentDate.Subtract(lastJumpDate);
            ACLogger.UserDebug(": AppJumpController difference=" + difference);

            // Set periodBetweenJumps according to AppCentralUnityApi
            periodBetweenJumps = (long)PlayerPrefs.GetInt("appjump_period_between_jumps");
            //periodBetweenJumps = (long)AppCentralUnityApi_Internal.Instance.getJsonObj().appJump.periodBetweenJumps;
            ACLogger.UserDebug(": AppJumpController periodBetweenJumps=" + periodBetweenJumps);

            // Check if the 'difference' (between the currentTime and the lastJumpDate) is larger than the periodBetweenJumps
            if (difference.Ticks > (periodBetweenJumps * 10000000))
            { 
                DoAppJump();
            }
        }

        private static void DoAppJump()
        {
            // Save currenttime as lastJumpDate
            SaveCurrentTimeToPrefs();

            // Do AppJump, jump target is set in AppCentralUnityApi
            Application.OpenURL(appjump_target_app);

            ACLogger.UserDebug(": AppJumpController DoAppJump to appjump_target_app=" + appjump_target_app);
        }

        private static void SaveCurrentTimeToPrefs()
        {
            //Save the current system time as a string in the player prefs class
            PlayerPrefs.SetString("lastJumpDate", System.DateTime.Now.ToBinary().ToString());
        }

        /*        private void OnDisable()
                {
                    AppCentralUnityApi_Internal.OnAppLaunch -= UpdateAppJumpSetting;
                    AppCentralUnityApi_Internal.OnLoadEvent -= UpdateAppJumpSetting;

                    AppCentralUnityApi_Internal.OnAppLaunch -= CheckChainJumping;
                    AppCentralUnityApi_Internal.OnLoadEvent -= TryAppJumping;
                }*/

    }
}