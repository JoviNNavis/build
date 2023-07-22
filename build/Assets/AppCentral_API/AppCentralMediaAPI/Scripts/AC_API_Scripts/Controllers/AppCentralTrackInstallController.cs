using System;
using System.Collections;
using UnityEngine;
using UnityEngine.iOS;
using UnityEngine.Networking;
namespace AppCentralCore
{
    public class AppCentralTrackInstallController : MonoBehaviour
    {
        // Start is called before the first frame update
        public static void TrackInstall()
        {
            ACLogger.UserDebug(": Appcentral_API TrackInstall called");

            String trackUrl = $"https://vnc412s287.execute-api.us-east-1.amazonaws.com/default/unity-tracker?v=3.5&action=start&appid={Application.identifier}&installID={GetInstallID()}&idfa={Device.advertisingIdentifier}&idfv={Device.vendorIdentifier}";

            // Get Install Time, If it doesn't exist set it to be an empty string.
            String installTime = PlayerPrefs.GetString("installTime", "");
            // Check if installTime is an empty string, if it is ->                                
            if (installTime == "")
            {
                installTime = System.DateTime.Now.ToString("o");
                PlayerPrefs.SetString("installTime", installTime);
            }
            // Add installTime to trackUrl
            trackUrl += "&installTime=" + installTime;

            // Get Campaign name, If it doesn't exist set it to be an empty string. 
            // It is fetched in the API call, so it can never happen the 1st time the app is opened.
            String campaignName = PlayerPrefs.GetString("campaignName", "");
            // Add campaignName to trackUrl
            trackUrl += "&campaignName=" + campaignName;

            UnityWebRequest.Get(trackUrl).SendWebRequest();
            ACLogger.UserDebug(": TrackInstall Web request Sent");

        }

        static String GetInstallID()
        {
            String installIdKey = "install_id_key";
            String installID = PlayerPrefs.GetString(installIdKey, "no_guid");
            if (installID == "no_guid")
            {
                installID = System.Guid.NewGuid().ToString();
                PlayerPrefs.SetString(installIdKey, installID);
            }
            return installID;
        }
    }
}
