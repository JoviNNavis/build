
using UnityEngine;

namespace AppCentralCore
{
    public class AppCentralGameAnalyticsController : MonoBehaviour
    {
        public static void InitializeGameAnalytics()
        {

#if AC_GAMEANALYTICS

            ACLogger.UserDebug(": GA InitializeGameAnalytics called");

            newSetting settings = AppCentralSettings.LoadSetting();

            if (!settings.canUseGameAnalytics)
            {
                ACLogger.UserDebug(": GameAnalytics is Disabled from AppCentral Settings");
                return;
            }

            ACLogger.UserDebug(": GameAnalytics is Enabled from AppCentral Settings with IDs= " + "settings.gameAnalyticsIosGameKey=" + settings.gameAnalyticsIosGameKey + "settings.gameAnalyticsIosSecretKey=" + settings.gameAnalyticsIosSecretKey);

            GameObject GA_obj = Instantiate(AppCentralUnityApi_Internal.Instance.appcentral_ReferenceManager.GameAnalytics_Prefeb);

            GameAnalyticsSDK.GameAnalytics.Initialize();

#endif
        }
    }
}