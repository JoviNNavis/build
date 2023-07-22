using System;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.iOS;
using UnityEngine.UI;
using TMPro;
using AppCentralCore;

public class InAppButton_Cell : MonoBehaviour
{

    public InApp_Subscription InApp_Subscription;

    public Button button;
    public TextMeshProUGUI Button_Discription;
    public TextMeshProUGUI Sucscribtion_Discription;
    public TextMeshProUGUI Price_Discription;

    public string ProductinAppID;


    private void Start()
    {
        button.onClick.AddListener(onButtonClick);
    }

    public void OnEnable()
    {
        ProductinAppID = "";

        newSetting setting = AppCentralSettings.LoadSetting();

        String Button_dic = Button_Discription.text;
        String Sub_Discription = Sucscribtion_Discription.text;
        String Price_Disc = Price_Discription.text;



        switch (InApp_Subscription)
        {
            case InApp_Subscription.AppCentralGames_AdFree:

                Price_Disc = AppCentralInAppPurchaser.Instance.getProductLocalizedPrice(setting.AC_AllGamesAdFreeInAppID);
                ProductinAppID = setting.AC_AllGamesAdFreeInAppID;
                break;
            case InApp_Subscription.AllBundle_Adfree:

                Price_Disc = AppCentralInAppPurchaser.Instance.getProductLocalizedPrice(setting.AC_AllBundleAdfreeInAppID);
                ProductinAppID = setting.AC_AllBundleAdfreeInAppID;

                break;
            case InApp_Subscription.DynamicPaywall:

                SetupDynamicPaywallVariales(ref Button_dic, ref Sub_Discription, ref Price_Disc, ref ProductinAppID);

                break;
            case InApp_Subscription.MidGame:

                SetupMidGamePaywallVariales(ref Button_dic, ref Sub_Discription, ref Price_Disc, ref ProductinAppID);

                break;
            default:
                break;
        }


        GH.UI_PayWall.Instance.PaywallProductID = ProductinAppID;

        if (Button_dic != "") Button_Discription.text = Button_dic;
        if (Sub_Discription != "") Sucscribtion_Discription.text = Sub_Discription;
        if (Price_Disc != "") Price_Discription.text = Price_Disc;

    }

    private void SetupDynamicPaywallVariales(ref string Button_dic, ref string Sub_Discription, ref string Price_Disc, ref string ProductinAppID)
    {
        Dynamic dynamicPaywall = AppCentralUnityApi_Internal.Instance.jsonController.JsonObject.paywalls.dynamic;

        if (dynamicPaywall.products != null)
        {
            if (dynamicPaywall.products.Count > 0)
            {
                if (dynamicPaywall.products[0].productId != "")
                {
                    ProductinAppID = dynamicPaywall.products[0].productId;

                    Button_dic = dynamicPaywall.products[0].buttonText;
                    Sub_Discription = dynamicPaywall.products[0].buttonDescription;
                    Price_Disc = AppCentralInAppPurchaser.Instance.getProductLocalizedPrice(ProductinAppID);

                    GH.UI_PayWall.Instance.PaywallProductID = ProductinAppID;

                }
                else
                {
                    PresistanceCanvas.GH_WarningManager.Instance.Prompt("Dynamic Product InApp Id string is empty, please check Product value at server");
                }
            }
            else
            {
                PresistanceCanvas.GH_WarningManager.Instance.Prompt("Dynamic Product is zero or null, please check Product value at server");
            }
        }
        else
        {
            PresistanceCanvas.GH_WarningManager.Instance.Prompt("Dynamic Product is zero or null, please check Product value at server");
        }
    }

    private void SetupMidGamePaywallVariales(ref string Button_dic, ref string Sub_Discription, ref string Price_Disc, ref string ProductinAppID)
    {
        MidGame midGamePaywall = AppCentralUnityApi_Internal.Instance.jsonController.JsonObject.paywalls.midgame;

        if (midGamePaywall.products != null)
        {
            if (midGamePaywall.products.Count > 0)
            {
                if (midGamePaywall.products[0].productId != "")
                {
                    ProductinAppID = midGamePaywall.products[0].productId;

                    Button_dic = midGamePaywall.products[0].buttonText;
                    Sub_Discription = midGamePaywall.products[0].buttonDescription;
                    Price_Disc = AppCentralInAppPurchaser.Instance.getProductLocalizedPrice(ProductinAppID);

                    GH.UI_PayWall.Instance.PaywallProductID = ProductinAppID;

                }
                else
                {
                    PresistanceCanvas.GH_WarningManager.Instance.Prompt("Dynamic Product InApp Id string is empty, please check Product value at server");
                }
            }
            else
            {
                PresistanceCanvas.GH_WarningManager.Instance.Prompt("Dynamic Product is zero or null, please check Product value at server");
            }
        }
        else
        {
            PresistanceCanvas.GH_WarningManager.Instance.Prompt("Dynamic Product is zero or null, please check Product value at server");
        }
    }

    


    public void onButtonClick()
    {
        AppCentralInAppPurchaser.Instance.BuySubscription(ProductinAppID, responceReceiver);
    }

    public void responceReceiver(bool b, string s)
    {

    }




}
public enum InApp_Subscription
{
    AppCentralGames_AdFree,
    AllBundle_Adfree,
    DynamicPaywall,
    Defulat,
    MidGame,
    PurchasePopup
}
