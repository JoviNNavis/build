using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppCentralAPI;

namespace AppCentralCore
{
    public class EventListener_startPlay : MonoBehaviour
    {
        private void OnEnable()
        {
            AppCentralUnityApi_Internal.StartPlayEvent += EventReceiver;
        }

        private void EventReceiver()
        {
            /*            if (AppCentralUnityApi_Internal.Instance.getJsonObj().allowTracking.showLocation[0] == SHOW_LOCATION.startPlay.ToString())
                        {
                            ACLogger.UserDebug("AppCentral_API ATT requesting startPlay");
                            AppCentralATTController.ShowATT();
                        }*/

            if (AppCentralUnityApi_Internal.Instance.getJsonObj().pushNotification.showLocation[0] == SHOW_LOCATION.startPlay.ToString())
            {
                ACLogger.UserDebug("AppCentral_API PuchNotification Prompt requesting startPlay");
                AppCentralOneSignalController.show_PushNotification_Prompt();
            }

            // if (
            //     AppCentralUnityApi_Internal.Instance.getJsonObj().paywalls.dynamic.showLocation[0]
            //     == SHOW_LOCATION.startPlay.ToString()
            // )
            if (
                AppCentralUnityApi_Internal.IsDynamicPaywallLocationsContain(
                    SHOW_LOCATION.startPlay
                )
            )
            {
                ACLogger.UserDebug("AppCentral_API PuchNotification Prompt requesting startPlay");
                AppCentral.ShowDynamicPaywall();
            }
        }

        private void OnDisable()
        {
            AppCentralUnityApi_Internal.StartPlayEvent -= EventReceiver;
        }
    }
}
