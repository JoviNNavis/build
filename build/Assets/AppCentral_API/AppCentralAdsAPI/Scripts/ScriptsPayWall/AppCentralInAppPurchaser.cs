using System;
using System.Collections.Generic;
using UnityEngine;
using AppCentralCore;

using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Security;

// Deriving the Purchaser class from IStoreListener enables it to receive messages from Unity Purchasing.
public class AppCentralInAppPurchaser : MonoBehaviour, IStoreListener
{
    const string LocalSubscriptionStatusPrefe = "LocalSubscriptionStatusPrefe";

    public static AppCentralInAppPurchaser Instance;
    public Purchase_GUI purchase_GUI = new Purchase_GUI();

    private void Awake()
    {
        if (!Instance)
            Instance = this;
    }

    public static Action OnUserSubscibedSuccessfully;
    public static Action<bool, string> SubResponce;

    private static IStoreController m_StoreController; // The Unity Purchasing system.
    private static IExtensionProvider m_StoreExtensionProvider; // The store-specific Purchasing subsystems.

    // Product identifiers for all products capable of being purchased:
    // "convenience" general identifiers for use with Purchasing, and their store-specific identifier
    // counterparts for use with and outside of Unity Purchasing. Define store-specific identifiers
    // also on each platform's publisher dashboard (iTunes Connect, Google Play Developer Console, etc.)

    // General product identifiers for the consumable, non-consumable, and subscription products.
    // Use these handles in the code to reference which product to purchase. Also use these values
    // when defining the Product Identifiers on the store. Except, for illustration purposes, the
    // kProductIDSubscription - it has custom Apple and Google identifiers. We declare their store-
    // specific mapping to Unity Purchasing's AddProduct, below.


    private bool initializePurchasingFromOutside = false;

    private List<string> AllInAppIds = new List<string>();
    private List<string> AllInAppsLocalizedPrices = new List<string>();

    private static string currentSelectedInAppId = "";

    /// <summary>
    /// string : InAppID
    /// String : InApp Localized Price againt each InApp ID
    /// </summary>
    private Dictionary<string, string> Dic_AllInAppIdDictinery = new Dictionary<string, string>();

    bool oneTimeIAPInitialization = true;

    private void RecievedAppCentralApiResponse(bool isSuccesfulResponse)
    {
        if (oneTimeIAPInitialization)
        {
            oneTimeIAPInitialization = false;

            newSetting setting = AppCentralSettings.LoadSetting();

            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                ACLogger.UserWarning(
                    ":[IAP] OnIAPInitialization failed due to internet connection."
                );
                AppCentralUnityApi_Internal.Instance.OnIAPInitializationCompleted();
            }

            if (!setting.UseInApps)
            {
                AppCentralUnityApi_Internal.Instance.OnIAPInitializationCompleted();
                return;
            }

            InitilizeAllInAppIds(setting);

            if (!IsInitialized())
            {
                // Begin to configure our connection to Purchasing
                InitializeAllPurchases();
                initializePurchasingFromOutside = true;
            }
        }
        else
        {
            return;
        }
    }

    private void InitilizeAllInAppIds(newSetting setting)
    {
        if (!setting.UseInApps)
            return;

        Dynamic dynamicPaywall = AppCentralUnityApi_Internal
            .Instance
            .jsonController
            .JsonObject
            .paywalls
            .dynamic;
        MidGame midGamePaywall = AppCentralUnityApi_Internal
            .Instance
            .jsonController
            .JsonObject
            .paywalls
            .midgame;
        PurchasePopUp purchasePopUp = AppCentralUnityApi_Internal
            .Instance
            .jsonController
            .JsonObject
            .purchasePopUp;

        if (dynamicPaywall.products[0].productId != "")
        {
            AllInAppIds.Add(dynamicPaywall.products[0].productId);
        }

        if (midGamePaywall.products[0].productId != "")
        {
            AllInAppIds.Add(midGamePaywall.products[0].productId);
        }

        if (!string.IsNullOrEmpty(purchasePopUp.productId))
        {
            AllInAppIds.Add(purchasePopUp.productId);
        }

        AllInAppIds.Add(setting.AC_AllGamesAdFreeInAppID);
        AllInAppIds.Add(setting.AC_AllBundleAdfreeInAppID);

        foreach (var ID in setting.DynamicSubscriptionInAppID)
        {
            AllInAppIds.Add(ID);
        }

        foreach (var ID in setting.MidGameSubscriptionInAppID)
        {
            AllInAppIds.Add(ID);
        }

        // Putting InApp product IDs in Dictionery for Organization;
        for (int i = 0; i < AllInAppIds.Count; i++)
        {
            if (!Dic_AllInAppIdDictinery.ContainsKey(AllInAppIds[i]))
            {
                Dic_AllInAppIdDictinery.Add(AllInAppIds[i], "");
                ACLogger.UserDebug(": subscription valid InAppID=" + AllInAppIds[i]);
            }
        }
    }

    public string getProductLocalizedPrice(string productID)
    {
        if (!Dic_AllInAppIdDictinery.ContainsKey(productID))
        {
            ACLogger.UserError(
                ": Paywall productID="
                    + productID
                    + " not mtach please check product IDs in the Appcentral setting Menu"
            );
        }

        return Dic_AllInAppIdDictinery[productID];
    }

    private void OnEnable()
    {
        AppCentralUnityApi_Internal.RecievedAppCentralApiResponse += RecievedAppCentralApiResponse;
    }

    private void OnDisable()
    {
        AppCentralUnityApi_Internal.RecievedAppCentralApiResponse -= RecievedAppCentralApiResponse;
    }

    public void InitializeAllPurchases()
    {
        // If we have already connected to Purchasing ...
        if (IsInitialized())
        {
            // ... we are done here.
            return;
        }

        // Create a builder, first passing in a suite of Unity provided stores.
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        // Add a product to sell / restore by way of its identifier, associating the general identifier
        // with its store-specific identifiers.
        //builder.AddProduct(kProductIDConsumable, ProductType.Consumable);
        // Continue adding the non-consumable product.
        //builder.AddProduct(kProductIDNonConsumable, ProductType.NonConsumable);
        // And finish adding the subscription product. Notice this uses store-specific IDs, illustrating
        // if the Product ID was configured differently between Apple and Google stores. Also note that
        // one uses the general kProductIDSubscription handle inside the game - the store-specific IDs
        // must only be referenced here.

        foreach (var iAP in AllInAppIds)
        {
            builder.AddProduct(
                iAP,
                ProductType.Subscription,
                new IDs() { { iAP, AppleAppStore.Name } }
            );
        }

        UnityPurchasing.Initialize(this, builder);

        foreach (var ID in AllInAppIds)
        {
            ACLogger.UserDebug(": IAP Subscription ID =" + ID);
        }
    }

    public bool IsInitialized()
    {
        // Only say we are initialized if both the Purchasing references are set.
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }

    public void BuyConsumable()
    {
        // Buy the consumable product using its general identifier. Expect a response either
        // through ProcessPurchase or OnPurchaseFailed asynchronously.
    }

    public void BuyNonConsumable()
    {
        // Buy the non-consumable product using its general identifier. Expect a response either
        // through ProcessPurchase or OnPurchaseFailed asynchronously.
    }

    private static bool isPurchaseProcessing = false;
    private static bool isSubInitialitedByPlayer = false;

    public static bool IsPurchaseProcessing
    {
        get => isPurchaseProcessing;
        private set => isPurchaseProcessing = value;
    }

    public void BuySubscription(string inAppId, Action<bool, string> subscriptionRsponce)
    {
        // #if UNITY_EDITOR
        //         purchase_GUI.showEditorIAP_Window(inAppId, subscriptionRsponce);
        // #else
        SubResponce = subscriptionRsponce;

        IsPurchaseProcessing = true;
        isSubInitialitedByPlayer = true;
        // Buy the subscription product using its the general identifier. Expect a response either
        // through ProcessPurchase or OnPurchaseFailed asynchronously.
        // Notice how we use the general product identifier in spite of this ID being mapped to
        // custom store-specific identifiers above.
        currentSelectedInAppId = inAppId;
        BuyProductID(inAppId);

        Invoke("forceDisableIsPurchaseProcessingBool", 6);
        // #endif
    }

    void forceDisableIsPurchaseProcessingBool()
    {
        IsPurchaseProcessing = false;
        ACLogger.UserDebug("Force set IsPurchaseProcessing bool to false");
    }

    public void BuySubscription_Editor(string inAppId, Action<bool, string> subscriptionRsponce)
    {
        SubResponce = subscriptionRsponce;

        IsPurchaseProcessing = true;
        isSubInitialitedByPlayer = true;
        // Buy the subscription product using its the general identifier. Expect a response either
        // through ProcessPurchase or OnPurchaseFailed asynchronously.
        // Notice how we use the general product identifier in spite of this ID being mapped to
        // custom store-specific identifiers above.
        currentSelectedInAppId = inAppId;

        purchase_GUI.showEditorIAP_Window(inAppId, subscriptionRsponce);
        BuyProductID(inAppId);
    }

    void BuyProductID(string productId)
    {
        // If Purchasing has been initialized ...
        if (IsInitialized())
        {
            // ... look up the Product reference with the general product identifier and the Purchasing
            // system's products collection.
            Product product = m_StoreController.products.WithID(productId);
            // If the look up found a product for this device's store and that product is ready to be sold ...
            if (product != null && product.availableToPurchase)
            {
                ACLogger.UserDebug(
                    string.Format("Purchasing product asychronously: '{0}'", product.definition.id)
                );
                // ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed
                // asynchronously.
#if UNITY_EDITOR

                PresistanceCanvas.GH_IAP_PopUpPurchaser_Dummy.Instance.ShowPooUpPurchaser(
                    productId
                );

#else
                m_StoreController.InitiatePurchase(product);

#endif

                SubResponce?.Invoke(true, productId);
            }
            // Otherwise ...
            else
            {
                // ... report the product look-up failure situation
                ACLogger.UserDebug(
                    "BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase"
                );
                SubResponce?.Invoke(false, productId);
            }
        }
        // Otherwise ...
        else
        {
            // ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or
            // retrying initiailization.
            SubResponce?.Invoke(false, productId);
            InitializeAllPurchases();
            ACLogger.UserDebug("BuyProductID FAIL. Not initialized.");
        }
    }

    // Restore purchases previously made by this customer. Some platforms automatically restore purchases, like Google.
    // Apple currently requires explicit purchase restoration for IAP, conditionally displaying a password prompt.
    public void RestorePurchases()
    {
        // If Purchasing has not yet been set up ...
        if (!IsInitialized())
        {
            // ... report the situation and stop restoring. Consider either waiting longer, or retrying initialization.
            ACLogger.UserDebug("RestorePurchases FAIL. Not initialized.");
            InitializeAllPurchases();
            return;
        }

        // If we are running on an Apple device ...
        if (
            Application.platform == RuntimePlatform.IPhonePlayer
            || Application.platform == RuntimePlatform.OSXPlayer
        )
        {
            // ... begin restoring purchases
            ACLogger.UserDebug("RestorePurchases started ...");

            // Fetch the Apple store-specific subsystem.
            var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
            // Begin the asynchronous process of restoring purchases. Expect a confirmation response in
            // the Action<bool> below, and ProcessPurchase if there are previously purchased products to restore.
            apple.RestoreTransactions(
                (result) =>
                {
                    // The first phase of restoration. If no more responses are received on ProcessPurchase then
                    // no purchases are available to be restored.
                    ACLogger.UserDebug(
                        "RestorePurchases continuing: "
                            + result
                            + ". If no further messages, no purchases available to restore."
                    );
                }
            );
        }
        // Otherwise ...
        else
        {
            // We are not running on an Apple device. No work is necessary to restore purchases.
            ACLogger.UserDebug(
                "RestorePurchases FAIL. Not supported on this platform. Current = "
                    + Application.platform
            );
        }
    }

    //
    // --- IStoreListener
    //

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        // Purchasing has succeeded initializing. Collect our Purchasing references.
        ACLogger.UserDebug(": IAP Purchaser OnInitialized: PASS");

        // Overall Purchasing system, configured with products for this application.
        m_StoreController = controller;
        // Store specific subsystem, for accessing device-specific store features.
        m_StoreExtensionProvider = extensions;

        for (int i = 0; i < AllInAppIds.Count; i++)
        {
            string LocalizedPrice =
                "Only "
                + m_StoreController.products.WithID(AllInAppIds[i]).metadata.localizedPriceString
                + " per month";
            AllInAppsLocalizedPrices.Add(LocalizedPrice);
            Dic_AllInAppIdDictinery[AllInAppIds[i]] = LocalizedPrice;
            ACLogger.UserDebug(
                ": Localized price for inApp Id =" + AllInAppIds + " is " + LocalizedPrice
            );
        }

        GH.UI_PayWall.OnPurchaserInitialize(controller);

        AppCentralUnityApi_Internal.Instance.OnIAPInitializationCompleted();
    }

    public void OnInitializeFailed(InitializationFailureReason error )
    {
        // Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
        ACLogger.UserDebug("IAP OnInitializeFailed InitializationFailureReason:" + error);
        AppCentralUnityApi_Internal.Instance.OnIAPInitializationCompleted();
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        //// A consumable product has been purchased by this user.
        //if (String.Equals(args.purchasedProduct.definition.id, kProductIDConsumable, StringComparison.Ordinal))
        //{
        //    ACLogger.UserDebug(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
        //    // The consumable item has been successfully purchased, add 100 coins to the player's in-game score.
        //}
        //// Or ... a non-consumable product has been purchased by this user.
        //else if (String.Equals(args.purchasedProduct.definition.id, kProductIDNonConsumable, StringComparison.Ordinal))
        //{
        //    ACLogger.UserDebug(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
        //    // TODO: The non-consumable item has been successfully purchased, grant this item to the player.
        //}
        //// Or ... a subscription product has been purchased by this user.
        //else



        if (!String.IsNullOrEmpty(args.purchasedProduct.definition.id))
        {
            OnPurchaseSuccess(args);
        }
        // Or ... an unknown product has been purchased by this user. Fill in additional products here....
        else
        {
            OnPurchaseCancleOrFail(args);
        }

        // Return a flag indicating whether this product has completely been received, or if the application needs
        // to be reminded of this purchase at next app launch. Use PurchaseProcessingResult.Pending when still
        // saving purchased products to the cloud, and when that save is delayed.
        IsPurchaseProcessing = false;
        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseSuccess(PurchaseEventArgs args)
    {
        PlayerPrefs.SetInt("purchaseMade", 1);
        PlayerPrefs.SetInt("adsRemoved", 1);
        PlayerPrefs.SetInt("isSubscribed", 1);
        PlayerPrefs.SetInt(LocalSubscriptionStatusPrefe, 1);

        AppCentralUnityApi_Internal.Instance.jsonController.JsonObject.subscription.isSubscribed = 1;


#if !UNITY_EDITOR

        ACLogger.UserDebug(
            string.Format(
                "ProcessPurchase: PASS. Product: '{0}'",
                args.purchasedProduct.definition.id
            )
        );

#endif

        ACLogger.UserDebug("user Subscribed successfully");

        // TODO: The subscription item has been successfully purchased, grant this to the player.

        // #if !UNITY_EDITOR
        // #else
        //         ACLogger.UserDebug(
        //             "User subscribed in the EDITOR. " + "Pixel Will only be sent when run on device."
        //         );
        // #endif
        Debug.Log("isSubInitialitedByPlayer: " + isSubInitialitedByPlayer);

            OnUserSubscibedSuccessfully?.Invoke();

        if (isSubInitialitedByPlayer)
        {
            Debug.Log("isSubInitialitedByPlayer_A: " + isSubInitialitedByPlayer);

            SendAppCentralSubscriptionPixel(args);
        }
        else
        {
            Debug.Log("isSubInitialitedByPlayer_B: " + isSubInitialitedByPlayer);
        }

        IsPurchaseProcessing = false;
        isSubInitialitedByPlayer = false;
    }

    public void OnPurchaseCancleOrFail(PurchaseEventArgs args)
    {
#if !UNITY_EDITOR
        ACLogger.UserDebug(
            string.Format(
                "ProcessPurchase: FAIL. Unrecognized product: '{0}'",
                args.purchasedProduct.definition.id
            )
        );
#endif

        ACLogger.UserDebug("Purchase cancled or failed.");
        IsPurchaseProcessing = false;
        isSubInitialitedByPlayer = false;

    }

    private static void SendAppCentralSubscriptionPixel(PurchaseEventArgs args)
    {
        try
        {
            PresistanceCanvas.GH_WarningManager.Instance.Prompt("User Subscribed");
            GH.UI_PayWall.OnPaywallUserSubscribed(GH.UI_PayWall.Instance.pAYWALL_TYPE, args);
        }
        catch (IAPSecurityException)
        {
            ACLogger.UserDebug("Invalid receipt, not unlocking content");
        }
    }

    private static void SendAppCentralSubscriptionPixelOlder(PurchaseEventArgs args)
    {
        try
        {
            // On Apple stores, receipts contain multiple products.

            string transactionID = args.purchasedProduct.transactionID;
            string product_ID = args.purchasedProduct.definition.id;

            AppCentralPixelController.Instance.SaveAppCentralPixel(
                "subscription_status_pixel",
                new string[] { "transactionID", "productID" },
                new string[] { transactionID, product_ID }
            );
        }
        catch (IAPSecurityException)
        {
            ACLogger.UserDebug("Invalid receipt, not unlocking content");
        }
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        // A product purchase attempt did not succeed. Check failureReason for more detail. Consider sharing
        // this reason with the user to guide their troubleshooting actions.
        ACLogger.UserDebug(
            string.Format(
                "OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}",
                product.definition.storeSpecificId,
                failureReason
            )
        );
        IsPurchaseProcessing = false;
        OnUserSubscibedSuccessfully?.Invoke();
    }

    public static bool IsSubscriptionActive()
    {
        bool userSubscribed = false;

        #region "Local/AppCentral ServerCheck"

        int LocalSub_Verification = PlayerPrefs.GetInt(LocalSubscriptionStatusPrefe, 0);

        int ServerSub_Verification = AppCentralUnityApi_Internal
            .Instance
            .jsonController
            .JsonObject
            .subscription
            .isSubscribed;



        if (LocalSub_Verification == 1 || ServerSub_Verification == 1)
        {
            userSubscribed = true;
        }
        else
        {
            userSubscribed = false;
        }

        #endregion

        #region "Store Checking"


        if (!userSubscribed && m_StoreController != null)
        {
            foreach (var product in m_StoreController.products.all)
            {
                if (product.hasReceipt)
                {
                    userSubscribed = true;
                    PlayerPrefs.SetInt(LocalSubscriptionStatusPrefe , 1);
                    LocalSub_Verification = 1;
                    break;
                }
            }
        }

        #endregion

        ACLogger.UserDebug(": LocalSub_Verification=" + LocalSub_Verification);
        ACLogger.UserDebug(": ServerSub_Verification=" + ServerSub_Verification);

        return userSubscribed;
    }

    public static bool IsSubscriptionActive_ThisGameUser()
    {
        bool userSubscribed = false;

        #region "Local/AppCentral ServerCheck"

        int localSubscriptionStatusPrefe = PlayerPrefs.GetInt(LocalSubscriptionStatusPrefe, 0);

        ACLogger.UserDebug(": localSubscriptionStatusPrefe=" + localSubscriptionStatusPrefe);

        if (localSubscriptionStatusPrefe == 1)
        {
            userSubscribed = true;
        }
        else
        {
            userSubscribed = false;
        }

        #endregion

        return userSubscribed;
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        ((IStoreListener)Instance).OnInitializeFailed(error, message);
    }
}
