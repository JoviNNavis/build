using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppCentralAPI;

namespace AppCentralCore
{
    public class EventListener_deepInGame : MonoBehaviour
    {
        private void OnEnable()
        {
            AppCentralUnityApi_Internal.DeepinGameEvent += EventReceiver;
        }

        private void EventReceiver()
        {
            /*            if (AppCentralUnityApi_Internal.Instance.getJsonObj().allowTracking.showLocation[0] == SHOW_LOCATION.deepInGame.ToString())
                        {
                            ACLogger.UserDebug("AppCentral_API ATT requesting deepInGame");
                            AppCentralATTController.ShowATT();
                        }*/

            if (
                AppCentralUnityApi_Internal.Instance.getJsonObj().pushNotification.showLocation[0]
                == SHOW_LOCATION.deepInGame.ToString()
            )
            {
                ACLogger.UserDebug("AppCentral_API PuchNotification Prompt requesting deepInGame");
                AppCentralOneSignalController.show_PushNotification_Prompt();
            }

            if (
                AppCentralUnityApi_Internal.IsDynamicPaywallLocationsContain(
                    SHOW_LOCATION.deepInGame
                )
            )
            {
                AppCentral.ShowDynamicPaywall();
            }
        }

        private void OnDisable()
        {
            AppCentralUnityApi_Internal.DeepinGameEvent -= EventReceiver;
        }
    }
}
