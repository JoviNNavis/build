using System;
using System.Collections;
using UnityEngine;
using UnityEngine.iOS;
using UnityEngine.Networking;

namespace AppCentralCore
{
    public class EventListener_onAppLaunch : MonoBehaviour
    {
        private void OnEnable()
        {
            AppCentralUnityApi_Internal.OnAppLaunch += EventReceiver;
        }

        private void EventReceiver()
        {

            ACLogger.UserDebug("EventListener_onAppLaunch");

            AppCentralAppJumpController.UpdateAppJumpSetting();
            AppCentralAppJumpController.CheckChainJumping();
        }

        private void OnDisable()
        {
            AppCentralUnityApi_Internal.OnAppLaunch -= EventReceiver;
        }
    }
}
