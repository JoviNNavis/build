using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AppCentralCore
{
    public class AppCentralPurchsePopUpController : MonoBehaviour
    {
        public void Start()
        {
            AppCentralUnityApi_Internal.OnLevelStartEvent += IsValidLevelForPurchaserPopUp;
        }

        public void IsValidLevelForPurchaserPopUp(int currentLevelIndex)
        {
            ACLogger.UserDebug(": PurchsePopUp requested");

            newSetting settings = AppCentralSettings.LoadSetting();

            if (!settings.UseInApps)
            {
                ACLogger.UserDebug(
                    ": UseInApps option is disable in the Settings, So not showing PurchsePopUp"
                );
                return;
            }

            // if (AppCentralInAppPurchaser.IsSubscriptionActive())
            // {
            //     ACLogger.UserDebug(": PurchsePopUp is not showing: User already subscribed.");
            //     return;
            // }

            bool isValid = false;

            //string requestedLevels = AppCentralUnityApi_Internal.Instance.jsonController.JsonObject.purchasePopUp.showInLevels;
            //string productId = AppCentralUnityApi_Internal.Instance.jsonController.JsonObject.purchasePopUp.productId;

            string requestedLevels = PlayerPrefs.GetString("purchasePopUp_showInLevels");
            string productId = PlayerPrefs.GetString("purchasePopUp_productID");

            ACLogger.UserDebug(": PurchsePopUp requestedLevels=" + requestedLevels);
            ACLogger.UserDebug(": PurchsePopUp productId=" + productId);

            if (!string.IsNullOrEmpty(requestedLevels))
            {
                var indivisualLevels = requestedLevels.Split(',');

                foreach (var level in indivisualLevels)
                {
                    if (currentLevelIndex.ToString() == level)
                    {
                        isValid = true;
                        break;
                    }
                }
            }

            if (string.IsNullOrEmpty(productId))
            {
                isValid = false;
            }

            if (isValid)
            {
                ACLogger.UserDebug(": PurchsePopUp Open Request Send.");

//#if !UNITY_EDITOR

//                GH.UI_PayWall.Instance.ActivatePurchaserPopup(productId, subscriptionRsponce);
//#else

//                // ACLogger.UserDebug(": PurchsePopUp not opening in EDITOR.");
//#endif

                GH.UI_PayWall.Instance.ActivatePurchaserPopup(productId, subscriptionRsponce);

            }
        }

        public void subscriptionRsponce(bool result, string productId)
        {
            Time.timeScale = 1;
            ACLogger.UserDebug(
                ": PurchsePopUp responce received: Result="
                    + result
                    + ",Reason="
                    + productId
            );
        }
    }
}
