using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AppCentralCore
{
    public class EventListener_ATT_StatusUpdate : MonoBehaviour
    {
        private void OnEnable()
        {
            AppCentralATTController.ATT_AuthorizationStatusUpdate += ATTUpdate_EventReceiver;
        }

        private void ATTUpdate_EventReceiver(bool ATT_status)
        {
            ACLogger.UserDebug(": EventListener_ATT_StatusUpdate");


#if AC_APPLOVIN

            ACLogger.UserDebug(": InitializeAppLovin");

            GH.ApplovinAdManager.Instance.InitializeAppLovin();
#endif

            AppCentralTrackInstallController.TrackInstall();
            AppCentralAppsFlyerController.InitializeAppsFlyer();
            AppCentralGameAnalyticsController.InitializeGameAnalytics();
            AppCentralOneSignalController.OneSignal_PreIinitialization();



#if UNITY_EDITOR

            // it is just for testing purposes in side editor.
            AppCentralUnityApi_Internal.Instance.jsonController.AppsFlyerIsReady();

#endif
        }

        private void OnDisable()
        {
            AppCentralATTController.ATT_AuthorizationStatusUpdate -= ATTUpdate_EventReceiver;
        }
    }
}
