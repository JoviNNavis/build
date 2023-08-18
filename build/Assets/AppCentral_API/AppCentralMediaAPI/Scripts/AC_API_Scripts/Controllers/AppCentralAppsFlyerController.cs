using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AppCentralCore
{
    public class AppCentralAppsFlyerController : MonoBehaviour
    {
        public static void InitializeAppsFlyer()
        {

#if AC_APPSFLYER

	        ACLogger.UserDebug(": InitializeAppsFlyer is Called");

            newSetting settings = AppCentralSettings.LoadSetting();

            if (!settings.canUseAppsFlyer)
            {
                ACLogger.UserDebug(": InitializeAppsFlyer is Disabled from AppCentral Settings");
                return;
            }

            ACLogger.UserDebug(": InitializeAppsFlyer is Enable from AppCentral Settings with keys=" + ",settings.appsFlyer_devKey=" + settings.appsFlyer_devKey + ",settings.appsFlyer_appID=" + settings.IOSAppID);


            GameObject AppsFlyerObj = Instantiate(AppCentralUnityApi_Internal.Instance.appcentral_ReferenceManager.AppsFlyerObject_Custom_Prefeb);
            AppsFlyerObjectScript_Custom.Instance.Setup_AppsFlyerSetting(settings.appsFlyer_devKey, settings.IOSAppID);
            AppsFlyerObjectScript_Custom.Instance.initialize();

#else

            ACLogger.UserError(": AppsFlyer is disabled in the AppCentral SDK Setting please enable it othrwise AppCentral SDK will initilaze correctly");

#endif
        }
    }
}