using System.Collections;
using Balaso;
using UnityEngine;
using System;
using AppCentralAPI;
#if UNITY_IOS
using Unity.Notifications.iOS;
#endif

namespace AppCentralCore
{
    public class AppCentralATTController : MonoBehaviour
    {
        public static Action<bool> ATT_AuthorizationStatusUpdate;

        public static void PreInitialize_ATT()
        {
            ACLogger.UserDebug("PreInitialize_ATT");

#if !UNITY_EDITOR
            AppTrackingTransparency.RegisterAppForAdNetworkAttribution();
            AppTrackingTransparency.UpdateConversionValue(3);
#endif
        }

        public static void ShowATT()
        {
            ACLogger.UserDebug("ShowATT");

            //bool IsIOS14OrAbove = false;


            //if (Application.platform == RuntimePlatform.IPhonePlayer)
            //{
            //    string[] osVersion = SystemInfo.operatingSystem.Split('.');
            //    if (osVersion.Length > 0 && (osVersion[0] == "iPhone OS 14" || osVersion[0] == "iOS 14"))
            //    {
            //        // iOS 14 or above
            //        IsIOS14OrAbove = true;
            //    }
            //}


            //if (!IsIOS14OrAbove)
            //{
            //    ACLogger.UserDebug("ShowATT request denied due to IOS lower then 14.");
            //    ATT_AuthorizationStatusUpdate?.Invoke(false);
            //    return;
            //}


            //#if !UNITY_EDITOR

            //            OnAuthorizationRequestDone(AppTrackingTransparency.AuthorizationStatus.AUTHORIZED);
            //            ACLogger.UserDebug("Appcentral_API OnAuthorizationRequestDone : EDITOR");

            //#else

            ACLogger.UserDebug(": OnAuthorizationRequestDone : DEVICE");

            AppTrackingTransparency.OnAuthorizationRequestDone += OnAuthorizationRequestDone;

            AppTrackingTransparency.AuthorizationStatus currentStatus =
                AppTrackingTransparency.TrackingAuthorizationStatus;
            ACLogger.UserDebug(
                string.Format(
                    ": Current authorization status: {0}",
                    currentStatus.ToString()
                )
            );
            //if (currentStatus != AppTrackingTransparency.AuthorizationStatus.AUTHORIZED)
            {
                ACLogger.UserDebug(": Requesting authorization...");
                AppTrackingTransparency.RequestTrackingAuthorization();
            }
            //else
            //{
            //    OnAuthorizationRequestDone(currentStatus);
            //}

            //#endif
        }

#if UNITY_IOS

        /// <summary>
        /// Callback invoked with the user's decision
        /// </summary>
        /// <param name="status"></param>
        private static void OnAuthorizationRequestDone(
            AppTrackingTransparency.AuthorizationStatus status
        )
        {
            bool IsAccess = false;

            CustomDimension02 customDimension02 = CustomDimension02.Denied;

            switch (status)
            {
                case AppTrackingTransparency.AuthorizationStatus.NOT_DETERMINED:
                    IsAccess = false;
                    customDimension02 = CustomDimension02.NotDetermined;
                    ACLogger.UserDebug(": AuthorizationStatus: NOT_DETERMINED");
                    break;
                case AppTrackingTransparency.AuthorizationStatus.RESTRICTED:
                    ACLogger.UserDebug(": AuthorizationStatus: RESTRICTED");
                    customDimension02 = CustomDimension02.Restricted;

                    IsAccess = false;

                    break;
                case AppTrackingTransparency.AuthorizationStatus.DENIED:
                    ACLogger.UserDebug(": AuthorizationStatus: DENIED");
                    customDimension02 = CustomDimension02.Denied;

                    IsAccess = false;
                    break;
                case AppTrackingTransparency.AuthorizationStatus.AUTHORIZED:

                    customDimension02 = CustomDimension02.Authorized;
                    IsAccess = true;
                    ACLogger.UserDebug(": AuthorizationStatus: AUTHORIZED");
                    break;
            }

            ATT_AuthorizationStatusUpdate(IsAccess);

            string postNotificationSettings = iOSNotificationCenter
                .GetNotificationSettings()
                .AuthorizationStatus.ToString();

#if AC_GAMEANALYTICS
            GameAnalyticsSDK.GameAnalytics.SetCustomDimension02("notifications." + customDimension02);
#endif
            //AppCentralGameAnalyticsInternal.setCustomDimention02(customDimension02);


            // Obtain IDFA
            ACLogger.UserDebug(
                string.Format(
                    ": IDFA: {0}",
                    AppTrackingTransparency.IdentifierForAdvertising()
                )
            );
        }

#endif
    }
}
