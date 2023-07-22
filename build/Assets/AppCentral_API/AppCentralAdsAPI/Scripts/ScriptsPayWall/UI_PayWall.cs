using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Purchasing;
using System;
using TMPro;
using System.Linq;
using AppCentralCore;
using AppCentralAPI;
using UnityEngine.Purchasing;
using UnityEngine.Video;

namespace GH
{
    public class UI_PayWall : MonoBehaviour
    {
        private static UI_PayWall instance;
        public static UI_PayWall Instance
        {
            get => instance;
            private set => instance = value;
        }

        public static Action<IStoreController> PurchaserInitialize;
        public static Action<PAYWALL_TYPE> PaywallOpen;
        public static Action<PAYWALL_TYPE> PaywallClose;
        public static Action<PAYWALL_TYPE, PurchaseEventArgs> PaywallUserSubscribed;

        public PAYWALL_TYPE pAYWALL_TYPE;
        public String PaywallProductID;

        public GameObject Common;
        public GameObject Defualt_Paywall;
        public GameObject Dynamic_Paywall;
        public InAppButton_Cell inAppButtonCell_DynamicPaywall;
        public VideoPlayer DynamicPayWallVideoPlayer;

        public TextMeshProUGUI DefulayPyawall_AppName,
            DynamicPyawall_AppName;

        public int countdownTime = 0;
        public GameObject ExitButton;
        public GameObject CountDown;
        public Text CountDown_text;

        public Button TermsOfUse_Btn,
            PrivacyPolicy_Btn,
            RestorePurchase_Btn;

        private void Awake()
        {
            if (!Instance)
                Instance = this;
            Deactivate();
        }

        private void Start()
        {
            ExitButton.GetComponent<Button>().onClick.AddListener(ClosePaywallFromBtn);

            TermsOfUse_Btn.onClick.AddListener(TermsofUse_Btn_Pressed);
            PrivacyPolicy_Btn.onClick.AddListener(PrivacyPolicy_Btn_Pressed);
            RestorePurchase_Btn.onClick.AddListener(RestorePurchase_Btn_Pressed);
        }

        private bool Activate()
        {
            StopAllCoroutines();
            newSetting setting = AppCentralSettings.LoadSetting();

            DefulayPyawall_AppName.text = DynamicPyawall_AppName.text = setting.GameName;

            if (AppCentralInAppPurchaser.Instance.IsInitialized())
            {
                Common.SetActive(true);
                return true;
            }
            else
            {
                AppCentralInAppPurchaser.Instance.InitializeAllPurchases();
                PresistanceCanvas.GH_WarningManager.Instance.Prompt(
                    "InApp purchases are not initialized, please try again later"
                );
                return false;
            }
        }

        public void Deactivate()
        {
            Common.SetActive(false);
            Defualt_Paywall.SetActive(false);
            Dynamic_Paywall.SetActive(false);
        }

        private void OnEnable()
        {
            PaywallUserSubscribed += CloseAnyPaywallIfOpenAfterSubscription;
        }

        private void OnDisable()
        {
            PaywallUserSubscribed -= CloseAnyPaywallIfOpenAfterSubscription;
        }

        public static void OnPurchaserInitialize(IStoreController storeController)
        {
            PurchaserInitialize?.Invoke(storeController);
        }

        public static void OnPaywallOpen(PAYWALL_TYPE pAYWALL)
        {
            PaywallOpen?.Invoke(pAYWALL);
        }

        public static void OnPaywallClose(PAYWALL_TYPE pAYWALL)
        {
            PaywallClose?.Invoke(pAYWALL);
        }

        public static void OnPaywallUserSubscribed(PAYWALL_TYPE pAYWALL, PurchaseEventArgs args)
        {
            PaywallUserSubscribed?.Invoke(pAYWALL, args);
        }

        public void ActivateDynaimcPaywall(Action OnCloseCallback = null)
        {
            ACLogger.UserDebug(": DynaimcPaywall requested");
            DynamicPaywallCloseEvent = OnCloseCallback;
            newSetting settings = AppCentralSettings.LoadSetting();

            if (!settings.UseInApps)
            {
                ACLogger.UserDebug(
                    ": UseInApps option is disable in the Settings, So not showing any paywall"
                );
                DynamicPaywallClosedEvent();
                return;
            }

            ACLogger.UserDebug(
                ": User Subscription Status=" + AppCentralInAppPurchaser.IsSubscriptionActive()
            );
            ACLogger.UserDebug(
                ": paywall_dynamic_show_location="
                    + PlayerPrefs.GetString("paywall_dynamic_show_location")
            );

            if (Defualt_Paywall.activeInHierarchy || Dynamic_Paywall.activeInHierarchy)
            {
                ACLogger.UserDebug(
                    ": There is already a paywall opened, So not showing any DynaimcPaywall"
                );
                DynamicPaywallClosedEvent();
                return;
            }

            if (AppCentralInAppPurchaser.IsPurchaseProcessing)
            {
                ACLogger.UserDebug(
                    ": There is already an IAP in processing, So not showing any DynaimcPaywall."
                );
                DynamicPaywallClosedEvent();
                return;
            }

            if (!AppCentralInAppPurchaser.IsSubscriptionActive_ThisGameUser())
            {
                if (PlayerPrefs.GetString("paywall_dynamic_show_location", "") != "")
                {
                    if (Activate())
                    {

                        StartCoroutine(PayWall_CloseTimer(AppCentralCore.AppCentralUnityApi_Internal.Instance.jsonController.JsonObject.paywalls.dynamic.xCountdown));

                        DynamicPayWallVideoPlayer.clip = AppCentralSettings.LoadSetting().videoClip;

                        Defualt_Paywall.SetActive(false);
                        inAppButtonCell_DynamicPaywall.InApp_Subscription =
                            InApp_Subscription.DynamicPaywall;
                        Dynamic_Paywall.SetActive(true);

                        pAYWALL_TYPE = AppCentralAPI.PAYWALL_TYPE.dynamic;


                        OnPaywallOpen(pAYWALL_TYPE);
                    }
                }
                else
                {
                    ACLogger.UserDebug(
                        "paywall_dynamic_show_location is empty, so no need to show dynamic paywall"
                    );
                    DynamicPaywallClosedEvent();
                }

            }
            else
            {
                DynamicPaywallClosedEvent();

                ACLogger.UserDebug("Dynamic Paywall is not showing up as user already subscribed to Local scubscription.");

            }
        }

        Action DynamicPaywallCloseEvent;

        public void ActivateMidGamePaywall(Action OnCloseCallback = null)
        {
            DynamicPaywallCloseEvent = OnCloseCallback;
            ACLogger.UserDebug(": MidGamePaywall requested");

            newSetting settings = AppCentralSettings.LoadSetting();

            if (!settings.UseInApps)
            {
                ACLogger.UserDebug(
                    ": UseInApps option is disable in the Settings, So not showing any paywall"
                );
                DynamicPaywallClosedEvent();
                return;
            }

            if (Defualt_Paywall.activeInHierarchy || Dynamic_Paywall.activeInHierarchy)
            {
                ACLogger.UserDebug(
                    ": There is already a paywall opened, So not showing any MidGamePaywall"
                );

                DynamicPaywallClosedEvent();
                return;
            }

            if (AppCentralInAppPurchaser.IsPurchaseProcessing)
            {
                ACLogger.UserDebug(
                    ": There is already an IAP in processing, So not showing any MidGamePaywall."
                );

                DynamicPaywallClosedEvent();

                return;
            }

            if (!AppCentralInAppPurchaser.IsSubscriptionActive_ThisGameUser()) //Removed this check in Verion 0.1.8
            {
                if (Activate())
                {
                    DynamicPayWallVideoPlayer.clip = AppCentralSettings.LoadSetting().videoClip;

                    Defualt_Paywall.SetActive(false);
                    inAppButtonCell_DynamicPaywall.InApp_Subscription = InApp_Subscription.MidGame;
                    Dynamic_Paywall.SetActive(true);

                    pAYWALL_TYPE = AppCentralAPI.PAYWALL_TYPE.midGame;

                    StartCoroutine(
                        PayWall_CloseTimer(
                            AppCentralCore
                                .AppCentralUnityApi_Internal
                                .Instance
                                .jsonController
                                .JsonObject
                                .paywalls
                                .dynamic
                                .xCountdown
                        )
                    );
                    OnPaywallOpen(pAYWALL_TYPE);
                }
                else
                {
                    DynamicPaywallClosedEvent();
                }
            }
            else
            {
                DynamicPaywallClosedEvent();
                ACLogger.UserDebug("MidGamePaywall is not showing up as user already subscribed to Local scubscription.");
            }
        }

        public void ActivateDefulatPaywall()
        {
            if (AppCentralInAppPurchaser.IsSubscriptionActive())
                return;

            if (AppCentralInAppPurchaser.IsPurchaseProcessing)
            {
                ACLogger.UserDebug(
                    ": There is already an IAP in processing, So not showing any DefulatPaywall."
                );

                PresistanceCanvas.GH_WarningManager.Instance.Prompt(
                    "A purchase is already in process."
                );

                return;
            }

            Activate();

            Defualt_Paywall.SetActive(true);
            Dynamic_Paywall.SetActive(false);

            pAYWALL_TYPE = AppCentralAPI.PAYWALL_TYPE.@default;
            //StartCoroutine(PayWall_CloseTimer(AppCentralUnityApi.Instance.jsonObject.paywalls.@default.xCountdown));
            StartCoroutine(PayWall_CloseTimer(0));

            AppCentralPixelController.Instance.SaveAppCentralPixel(
                "shop_clicked_pixel",
                new string[] { "shop" },
                new string[] { "clicked" }
            );

            OnPaywallOpen(pAYWALL_TYPE);
        }

        public void ActivatePurchaserPopup(
            string ProductID,
            Action<bool, string> subscriptionRsponce
        )
        {
            // if (AppCentralInAppPurchaser.IsSubscriptionActive())
            // {
            //     subscriptionRsponce?.Invoke(false, "User already Subscribed");
            //     return;
            // }


            ACLogger.UserDebug(": PurchaserPopup requested");

            newSetting settings = AppCentralSettings.LoadSetting();

            if (!settings.UseInApps)
            {
                ACLogger.UserDebug(
                    ": UseInApps option is disable in the Settings, So not showing any paywall"
                );
                subscriptionRsponce?.Invoke(
                    false,
                    ": UseInApps option is disable in the Settings, So not showing any paywall"
                );
                return;
            }

            if (Defualt_Paywall.activeInHierarchy || Dynamic_Paywall.activeInHierarchy)
            {
                ACLogger.UserDebug(
                    ": There is already a paywall opened, So not showing any PurchaserPopup"
                );

                subscriptionRsponce?.Invoke(false, ": another paywal already oppened.");
                return;
            }

            if (AppCentralInAppPurchaser.IsPurchaseProcessing)
            {
                ACLogger.UserDebug(
                    ": There is already an IAP in processing, So not showing any PurchaserPopup."
                );

                subscriptionRsponce?.Invoke(false, "AppCentralInAppPurchaser.IsPurchaseProcessing");
                return;
            }

            Defualt_Paywall.SetActive(false);
            Dynamic_Paywall.SetActive(false);


            if (!AppCentralInAppPurchaser.IsSubscriptionActive_ThisGameUser())
            {

                if (AppCentralInAppPurchaser.Instance.IsInitialized())
                {
                    PaywallProductID = ProductID;
                    pAYWALL_TYPE = AppCentralAPI.PAYWALL_TYPE.popup;
                    OnPaywallOpen(pAYWALL_TYPE);
                    AppCentralInAppPurchaser.Instance.BuySubscription(ProductID, subscriptionRsponce);
                }
                else
                {
                    AppCentralInAppPurchaser.Instance.InitializeAllPurchases();
                    //PresistanceCanvas.GH_WarningManager.Instance.Prompt("InApp purchases are not initialized, please try again later");
                    subscriptionRsponce?.Invoke(false, "IAP is not initialized");
                }
            }else
            {
                subscriptionRsponce?.Invoke(false, "User already subscribed to local subscription.");
            }
        }

        IEnumerator PayWall_CloseTimer(int countDown)
        {
            ACLogger.UserDebug(": showing paywall with close count down value=" + countDown);

            ExitButton.SetActive(false);
            CountDown.SetActive(true);

            int count = countDown;

            CountDown_text.text = count.ToString();

            if (countDown != 0)
            {
                for (int i = count; i >= 0; i--)
                {
                    CountDown_text.text = i.ToString();
                    yield return new WaitForSeconds(1);
                }
            }

            CountDown.SetActive(false);
            ExitButton.SetActive(true);
        }

        public static bool IsSubscriptionActive()
        {
            return AppCentralInAppPurchaser.IsSubscriptionActive();
        }

        private void DynamicPaywallClosedEvent()
        {
            DynamicPaywallCloseEvent?.Invoke();
            DynamicPaywallCloseEvent = null;
        }

        private void CloseAnyPaywallIfOpenAfterSubscription(
            PAYWALL_TYPE pAYWALL,
            PurchaseEventArgs args
        )
        {
            ClosePaywallFromBtn();
        }

        public void ClosePaywallFromBtn()
        {

            OnPaywallClose(pAYWALL_TYPE);
            Deactivate();
            DynamicPaywallClosedEvent();
        }

        public void PurchaseSuccesfull()
        {
            Deactivate();
            ClosePaywallFromBtn();
        }

        public void TermsofUse_Btn_Pressed()
        {
            AppCentralCore.newSetting setting = AppCentralCore.AppCentralSettings.LoadSetting();
            Application.OpenURL(setting.URLTermsOfConditions);
        }

        public void PrivacyPolicy_Btn_Pressed()
        {
            AppCentralCore.newSetting setting = AppCentralCore.AppCentralSettings.LoadSetting();
            Application.OpenURL(setting.URLPrivacyPolicy);
        }

        public void RestorePurchase_Btn_Pressed()
        {
            AppCentralInAppPurchaser.Instance.RestorePurchases();
        }
    }
}
