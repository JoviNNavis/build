using UnityEngine;

namespace AppCentralCore
{
    public class AppCentralOneSignalController : MonoBehaviour
    {

        public static void OneSignal_PreIinitialization()
        {

#if AC_ONESIGNAL

            ACLogger.UserDebug("Initialize_OneSignal called");


            AppCentralCore.newSetting settings = AppCentralCore.AppCentralSettings.LoadSetting();

            if (!settings.UseOneSignal)
            {
                ACLogger.UserDebug("OneSignal is Disabled from AppCentral Settings");

                return;
            }

            string YOUR_ONESIGNAL_APP_ID = settings.OneSignalID;

            ACLogger.UserDebug("OneSignal is Enabled from AppCentral Settings with ONESIGNAL_APP_ID=" + YOUR_ONESIGNAL_APP_ID);

            OneSignalSDK.OneSignal.Default.Initialize(YOUR_ONESIGNAL_APP_ID);
#endif
        }

        public static async void show_PushNotification_Prompt()
        {

#if AC_ONESIGNAL
            ACLogger.UserDebug("Appcentral_API OneSignal PushNotification_Prompt called");


            newSetting settings = AppCentralSettings.LoadSetting();

            if (!settings.UseOneSignal)
            {
                ACLogger.UserDebug("Appcentral_API OneSignal PushNotification_Prompt is Disabled from AppCentral Settings");

                return;
            }

            string YOUR_ONESIGNAL_APP_ID = settings.OneSignalID;

            ACLogger.UserDebug("Appcentral_API OneSignal PushNotification_Prompt is Enabled from AppCentral Settings with ONESIGNAL_APP_ID=" + YOUR_ONESIGNAL_APP_ID);

            var result = await OneSignalSDK.OneSignal.Default.PromptForPushNotificationsWithUserResponse();

            ACLogger.UserDebug("Appcentral_API show_PushNotification_Prompt");

            if (result == OneSignalSDK.NotificationPermission.Authorized)
            {
                // user accepted
                ACLogger.UserDebug("user accepted push notifications");


            }
            else
            {
                // user didn't accept
                ACLogger.UserDebug("user didn't accept push notifications");
            }


            string action = result.ToString();
            AppCentralPixelController.Instance.SaveAppCentralPixel("push_notification_permission_pixel", new string[] { "action" }, new string[] { action });

#endif
        }

        public async void CallPushNotificationPermission()
        {
#if AC_ONESIGNAL

            var currentStatus = OneSignalSDK.OneSignal.Default.NotificationPermission;
            if (currentStatus == OneSignalSDK.NotificationPermission.NotDetermined)
            {
                ACLogger.UserDebug("called ios push notifications prompt");

                var response = await OneSignalSDK.OneSignal.Default.PromptForPushNotificationsWithUserResponse();
                if (response == OneSignalSDK.NotificationPermission.Authorized)
                {
                    // user accepted
                    ACLogger.UserDebug("user accepted push notifications");


                }
                else
                {
                    // user didn't accept
                    ACLogger.UserDebug("user didn't accept push notifications");
                }


                string action = response.ToString();
                AppCentralPixelController.Instance.SaveAppCentralPixel("push_notification_permission_pixel", new string[] { "action" }, new string[] { action });
            }
#endif
        }

    }
}