using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppCentralAPI;
using GH;

namespace AppCentralCore
{
    public class EventListener_onLoad : MonoBehaviour
    {
        private void OnEnable()
        {
            AppCentralUnityApi_Internal.OnLoadEvent += EventReceiver;
        }

        bool oneTime = true;

        private void EventReceiver()
        {
            if (!oneTime)
                return;

            oneTime = false;


        //   ApplovinAdManager.Instance.aL_AppOpenImpl.ShowAdIfReady(); 

            ACLogger.UserDebug(": EventListener_onLoad");

            AppCentralAppJumpController.UpdateAppJumpSetting();
            AppCentralAppJumpController.TryAppJumping();

            AppCentralThemeSwitchController.InitializeThemePerms();
            AppCentralUnityApi_Internal.Instance.SetUpTheme();

            AppCentralSmartLookController.Initialize_SmartLook();
            AppCentralSmartLookController.SmartLookTrackCampaign();



            if (AppCentralUnityApi_Internal.Instance.getJsonObj().pushNotification.showLocation[0]== SHOW_LOCATION.onLoad.ToString())
            {
                ACLogger.UserDebug(": PuchNotification Prompt requesting onLoad");
                AppCentralOneSignalController.show_PushNotification_Prompt();
            }


            if (AppCentralUnityApi_Internal.IsDynamicPaywallLocationsContain(SHOW_LOCATION.onLoad))
            {
                ACLogger.UserDebug(": dynamic paywall requesting at onLoad");
                AppCentral.ShowDynamicPaywall();
            }
        }

        private void OnDisable()
        {
            AppCentralUnityApi_Internal.OnLoadEvent -= EventReceiver;
        }
    }
}
