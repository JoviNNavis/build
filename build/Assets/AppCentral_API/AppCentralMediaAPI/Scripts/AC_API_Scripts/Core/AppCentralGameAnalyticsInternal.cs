using UnityEngine;
using AppCentralCore;
using AppCentralAPI;

namespace AppCentralCore
{
    public class AppCentralGameAnalyticsInternal : MonoBehaviour
    {

        #region GameAnalyticsAdEvents
        // all the ad events will be automatically connnected to ads controller
        #endregion


        #region GameAnalyticsCustomDimentions
        public static void setCustomDimention01(string theme)
        {

#if AC_GAMEANALYTICS

            ACLogger.UserDebug(": GameAnalyticsInternal_setCustomDimention01=" + theme);
            GameAnalyticsSDK.GameAnalytics.SetCustomDimension01(theme);
#endif

        }

        public static void setCustomDimention02(CustomDimension02 customDimension02)
        {
#if AC_GAMEANALYTICS
            ACLogger.UserDebug(": GameAnalyticsInternal_setCustomDimention02=" + customDimension02);
            GameAnalyticsSDK.GameAnalytics.SetCustomDimension02("notifications." + customDimension02.ToString());
#endif
        }

        #endregion


        #region GameAnalyticsAdsImpresionEvents

        public static void impresionEvents()
        {
#if AC_GAMEANALYTICS
            GameAnalyticsSDK.GameAnalyticsILRD.SubscribeHyperBidImpressions();
            GameAnalyticsSDK.GameAnalyticsILRD.SubscribeMoPubImpressions();
            GameAnalyticsSDK.GameAnalyticsILRD.SubscribeFyberImpressions();
            GameAnalyticsSDK.GameAnalyticsILRD.SubscribeIronSourceImpressions();
            GameAnalyticsSDK.GameAnalyticsILRD.SubscribeTopOnImpressions();
            GameAnalyticsSDK.GameAnalyticsILRD.SubscribeMaxImpressions();
            GameAnalyticsSDK.GameAnalyticsILRD.SubscribeAequusImpressions();
            subToAdmobImpresionEvents();
#endif
        }

        private static void subToAdmobImpresionEvents()
        {
/*            // BannerView
            bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Top);
            GameAnalyticsILRD.SubscribeAdMobImpressions(adUnitId, bannerView);

            // InterstitialAd 
            interstitialAd = new InterstitialAd(adUnitId);
            GameAnalyticsILRD.SubscribeAdMobImpressions(adUnitId, interstitialAd);

            // RewardedAd 
            rewardedAd = new RewardedAd(adUnitId);
            GameAnalyticsILRD.SubscribeAdMobImpressions(adUnitId, rewardedAd);

            // RewardedInterstitialAd
            RewardedInterstitialAd.LoadAd(adUnitId, CreateAdRequest(), (rewardedInterstitialAd, error) =>
            {
                if (error == null)
                {
                    GameAnalyticsILRD.SubscribeAdMobImpressions(adUnitId, rewardedInterstitialAd);
                }
            });*/
        }

        #endregion


        #region GameAnalyticsErrorEvents
#if AC_GAMEANALYTICS
        public static void SendAnErrorEvent(GameAnalyticsSDK.GAErrorSeverity errorSeverity, string errorMsg)
        {

            GameAnalyticsSDK.GameAnalytics.NewErrorEvent(errorSeverity, errorMsg);

    }
#endif

        #endregion

    }
}



