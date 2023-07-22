using UnityEngine;
using AppCentralCore;

namespace AppCentralCore
{
    public class AppCentralSmartLookEventsInternal : MonoBehaviour
    {
        #region SmartLookEvents
        public static void SmartLookTrack_InterstitialAd()
        {
#if AC_SMARTLOOK
            ACLogger.UserDebug(": SmartLookTrack_InterstitialAd=" + "interstitial_ad");

            SmartlookUnity.Smartlook.TrackCustomEvent("interstitial_ad");

#endif
        }

        public static void SmartLookTrack_RewardedAd()
        {
#if AC_SMARTLOOK
            ACLogger.UserDebug(": SmartLookTrack_RewardedAd=" + "rewarded_ad");
            SmartlookUnity.Smartlook.TrackCustomEvent("rewarded_ad");

#endif
        }

        #endregion

    }
}
