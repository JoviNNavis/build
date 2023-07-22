using System;
using System.Collections;
using UnityEngine;
using UnityEngine.iOS;
using UnityEngine.Networking;

namespace AppCentralCore
{
    public class EventListener_onAppJumpFinished : MonoBehaviour
    {
        private void OnEnable()
        {
            AppCentralUnityApi_Internal.OnAppJumpFinished += EventReceiver;
        }

        private void EventReceiver()
        {
            ACLogger.UserDebug("EventListener_onAppJumpFinished");

            AppCentralATTController.PreInitialize_ATT();
            AppCentralATTController.ShowATT();
        }

        private void OnDisable()
        {
            AppCentralUnityApi_Internal.OnAppJumpFinished -= EventReceiver;
        }
    }
}
