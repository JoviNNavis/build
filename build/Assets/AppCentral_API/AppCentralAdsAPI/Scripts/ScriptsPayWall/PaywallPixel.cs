using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppCentralAPI;
using UnityEngine.Purchasing;
using System;
using AppCentralCore;

namespace GH
{
    public class PaywallPixel : MonoBehaviour
    {
        private static string GUID = "";

        private void OnEnable()
        {
            UI_PayWall.PurchaserInitialize += sendPaywallPixelAtInitializePurchases;
            UI_PayWall.PaywallOpen += _OnPaywallOpen;
            UI_PayWall.PaywallClose += _OnPaywallClose;
            UI_PayWall.PaywallUserSubscribed += sendPaywallPixelFoUserSubscribed;
        }

        private void OnDisable()
        {
            UI_PayWall.PurchaserInitialize -= sendPaywallPixelAtInitializePurchases;
            UI_PayWall.PaywallOpen -= _OnPaywallOpen;
            UI_PayWall.PaywallClose -= _OnPaywallClose;
            UI_PayWall.PaywallUserSubscribed -= sendPaywallPixelFoUserSubscribed;
        }

        private void _OnPaywallOpen(PAYWALL_TYPE pAYWALL)
        {
            GUID = Guid.NewGuid().ToString();
            sendPaywallPixelForOpenClose(pAYWALL, "opened");
        }

        private void _OnPaywallClose(PAYWALL_TYPE pAYWALL)
        {
            sendPaywallPixelForOpenClose(pAYWALL, "closed");
        }

        private void sendPaywallPixelForOpenClose(PAYWALL_TYPE pAYWALL, string _action)
        {
            string dynamic_paywall_id = GUID;
            string productID = GH.UI_PayWall.Instance.PaywallProductID;
            string action = _action; // opened / closed
            string appSessionNumber = PlayerPrefs
                .GetInt("userData_app_session_number", 0)
                .ToString();
            string level = AppCentral.CurrentLevelID.ToString();
            string type = pAYWALL.ToString(); //string type = "default"; // default / dynamic / midGame / popup /

            string presentationStyle = "";
            string buttonText = "";
            string descriptionText = "";

            getPresentationStyle(
                pAYWALL,
                ref presentationStyle,
                ref buttonText,
                ref descriptionText
            );

            string[] PremsA = new string[]
            {
                "dynamic_paywall_id",
                "productID",
                "action",
                "appSessionNumber",
                "level",
                "type",
                "presentationStyle", // new added in Version 0.1.10
                "buttonText",
                "descriptionText"
            };
            string[] PremsB = new string[]
            {
                dynamic_paywall_id,
                productID,
                action,
                appSessionNumber,
                level,
                type,
                presentationStyle,
                buttonText,
                descriptionText
            };

            string debudString = "";
            for (int i = 0; i < PremsA.Length; i++)
            {
                debudString += PremsA[i] + ": " + PremsB[i] + ", ";
            }

            string pixelName = "dynamic_paywall_pixel";

            //type + "_paywall_pixel"; // standard_paywall_pixel
            // dynamic_paywall_pixel
            // midGame_paywall_pixel
            // popup_paywall_pixel

            ACLogger.UserDebug("[AppCentra] PaywallPixel=" + pixelName + "," + debudString);

            AppCentralPixelController.Instance.SaveAppCentralPixel(pixelName, PremsA, PremsB);
        }

        private void sendPaywallPixelFoUserSubscribed(PAYWALL_TYPE pAYWALL, PurchaseEventArgs args)
        {
            try
            {
                // Try saving subscription status pixel
                string dynamic_paywall_id = "0";
                if (pAYWALL != PAYWALL_TYPE.@default)
                    dynamic_paywall_id = GUID;

                string level = AppCentral.CurrentLevelID.ToString();
                string type = UI_PayWall.Instance.pAYWALL_TYPE.ToString();
                //string type =  default/ Dynamic/ midGame / popup
                string transactionID = args.purchasedProduct.transactionID;
                string productID = args.purchasedProduct.definition.id;
                string action = "userPurchased";
                string appSessionNumber = PlayerPrefs
                    .GetInt("userData_app_session_number", 0)
                    .ToString();

                string presentationStyle = "";
                string buttonText = "";
                string descriptionText = "";

                getPresentationStyle(
                    pAYWALL,
                    ref presentationStyle,
                    ref buttonText,
                    ref descriptionText
                );

                string[] PremsA = new string[]
                {
                    "dynamic_paywall_id",
                    "transactionID",
                    "productID",
                    "action",
                    "appSessionNumber",
                    "level",
                    "type",
                    "presentationStyle", // new added in Version 0.1.10
                    "buttonText",
                    "descriptionText"
                };
                string[] PremsB = new string[]
                {
                    dynamic_paywall_id,
                    transactionID,
                    productID,
                    action,
                    appSessionNumber,
                    level,
                    type,
                    presentationStyle,
                    buttonText,
                    descriptionText
                };

                string debudString = "";
                foreach (var item in PremsB)
                {
                    debudString += ", " + item;
                }

                //string pixelName = "subscription_status_pixel";
                string pixelName = "subscription_status";

                ACLogger.UserDebug("[AppCentra] PaywallPixel=" + pixelName + "," + debudString);

                AppCentralPixelController.Instance.SaveAppCentralPixel(pixelName, PremsA, PremsB);
            }
            catch { }
        }

        private void sendPaywallPixelAtInitializePurchases(IStoreController storeController)
        {
            ACLogger.UserDebug(
                "[AppCentra] PaywallPixel=" + "sendPaywallPixelAtInitializePurchases"
            );

            foreach (var product in storeController.products.all)
            {
                if (product.hasReceipt)
                {
                    string transactionID = product.transactionID;
                    string productID = product.definition.id;
                    string action = "start";
                    string appSessionNumber = PlayerPrefs
                        .GetInt("userData_app_session_number", 0)
                        .ToString();

                    string[] PremsA = new string[]
                    {
                        "transactionID",
                        "productID",
                        "action",
                        "appSessionNumber"
                    };
                    string[] PremsB = new string[]
                    {
                        transactionID,
                        productID,
                        action,
                        appSessionNumber
                    };

                    string debudString = "";
                    foreach (var item in PremsB)
                    {
                        debudString += ", " + item;
                    }

                    //string pixelName = "IAP_initialzed_pixel";
                    string pixelName = "subscription_status";

                    ACLogger.UserDebug("[AppCentra] PaywallPixel=" + pixelName + "," + debudString);

                    AppCentralPixelController.Instance.SaveAppCentralPixel(
                        pixelName,
                        PremsA,
                        PremsB
                    );
                }
            }
        }

        private void getPresentationStyle(
            PAYWALL_TYPE pAYWALL_TYPE,
            ref string presentationStyle,
            ref string buttonText,
            ref string descriptionText
        )
        {
            presentationStyle = "";
            buttonText = "";
            descriptionText = "";

            switch (pAYWALL_TYPE)
            {
                case PAYWALL_TYPE.@default:
                {
                    // presentationStyle = PlayerPrefs
                    //     .GetInt("paywall_modal_presentation_style")
                    //     .ToString();
                    // buttonText = PlayerPrefs.GetString("paywall_button_text");
                    // descriptionText = PlayerPrefs.GetString("paywall_button_description");
                    break;
                }
                case PAYWALL_TYPE.dynamic:
                {
                    presentationStyle = PlayerPrefs
                        .GetInt("pawall_dynamic_modal_presentation_style")
                        .ToString();
                    buttonText = PlayerPrefs.GetString("paywall_dynamic_button_text");
                    descriptionText = PlayerPrefs.GetString("paywall_dynamic_button_description");
                    break;
                }
                case PAYWALL_TYPE.midGame:
                {
                    presentationStyle = PlayerPrefs
                        .GetInt("pawall_midgame_modal_presentation_style")
                        .ToString();
                    buttonText = PlayerPrefs.GetString("paywall_midgame_button_text");
                    descriptionText = PlayerPrefs.GetString("paywall_midgame_button_description");
                    break;
                }
                case PAYWALL_TYPE.popup:
                {
                    break;
                }
            }
        }
    }
}
