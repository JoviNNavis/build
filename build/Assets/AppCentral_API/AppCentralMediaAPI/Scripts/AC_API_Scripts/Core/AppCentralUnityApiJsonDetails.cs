using System.Collections.Generic;

namespace AppCentralCore
{
    [System.Serializable]
    public class AppCentralUnityApiJsonDetails
    {
        public Theme theme;
        public Paywalls paywalls;
        public AppJump appJump;
        public AllowTracking allowTracking;
        public Subscription subscription;
        public Campaign campaign;
        public Rating rating;
        public Recording recording;
        public Ads ads;
        public PushNotification pushNotification;
        public UserData userData;
        public PurchasePopUp purchasePopUp;
        public BrightData brightData;
    }

    [System.Serializable]
    public class Theme
    {
        public string main;
        public bool forceTheme;

        public Theme()
        {
            main = "theme2";
            forceTheme = false;
        }
    }

    [System.Serializable]
    public class Paywalls
    {
        public Default @default;
        public Dynamic dynamic;
        public MidGame midgame;

        public Paywalls()
        {
            @default = new Default();
            dynamic = new Dynamic();
            midgame = new MidGame();
        }
    }

    [System.Serializable]
    public class Default
    {
        public int modalPresentationStyle;
        public List<string> showLocation;
        public int xCountdown;
        public List<Products> products;

        public Default()
        {
            modalPresentationStyle = 6;
            string showLocation_Defualt = "deepInGame";

            showLocation = new List<string>();

            showLocation.Add(showLocation_Defualt);
            xCountdown = 5;

            products = new List<Products>();
            Products product_01 = new Products();
            products.Add(product_01);
        }
    }

    [System.Serializable]
    public class Dynamic
    {
        public int modalPresentationStyle;
        public List<string> showLocation;
        public string showInLevels;
        public string adsInteraction;
        public int xCountdown;

        //public int midGame_xCountdown;
        public List<Products> products;

        public Dynamic()
        {
            modalPresentationStyle = 0;
            string showLocation_Defualt = "";

            showLocation = new List<string>();

            showLocation.Add(showLocation_Defualt);

            xCountdown = 5;

            products = new List<Products>();
            Products product_01 = new Products();
            products.Add(product_01);
        }
    }

    [System.Serializable]
    public class MidGame
    {
        public int modalPresentationStyle;
        public string showInLevels;
        public string adsInteraction;
        public int xCountdown;
        public List<Products> products;

        public MidGame()
        {
            modalPresentationStyle = 0;

            showInLevels = "3,5,9,14";
            adsInteraction = ""; // val: after/instead
#if UNITY_EDITOR
            adsInteraction = AppCentralAPI.MidGamePaywallAdInteraction.after.ToString();
#endif

            xCountdown = 5;

            products = new List<Products>();
            Products product = new Products();
            products.Add(product);
        }
    }

    [System.Serializable]
    public class Products
    {
        public string productId;
        public string statisticsTag;
        public string buttonText;
        public string buttonDescription;

        //public string midGame_productId;
        //public string midGame_buttonText;
        //public string midGame_buttonDescription;

        public Products()
        {
            productId = "";
            statisticsTag = "default_subscription";
            buttonText = "Subscribe";
            buttonDescription = "Start Your Subscription";

            //            midGame_productId = "";

            //#if UNITY_EDITOR
            //            midGame_productId = productId = "com.cr.candyroulette.freetrial_new_29";
            //#endif

            //            midGame_buttonText = "Subscribe";
            //            midGame_buttonDescription = "Start Your Subscription";
        }
    }

    [System.Serializable]
    public class AppJump
    {
        public string targetApp = "";
        public int periodBetweenJumps = 300;
    }

    [System.Serializable]
    public class AllowTracking
    {
        public List<string> showLocation;

        public AllowTracking()
        {
            string showlocation_Defualt = "onLoad";
            showLocation = new List<string>();
            showLocation.Add(showlocation_Defualt);
        }
    }

    [System.Serializable]
    public class Subscription
    {
        public int isSubscribed; // 0 = false, 1 = true
    }

    [System.Serializable]
    public class Campaign
    {
        public string name;
    }

    [System.Serializable]
    public class Rating
    {
        public string ratingType;
        public string appToRate;
        public string bundleID;
        public int rateOnLevel;
        public bool isBundleLeader;

        public Rating()
        {
            ratingType = "native";
            appToRate = "app";
            bundleID = "";
            rateOnLevel = 3;
            isBundleLeader = false;
        }
    }

    [System.Serializable]
    public class Recording
    {
        public int enableRecording;
        public int recordOnceIn;
    }

    [System.Serializable]
    public class Ads
    {
        public int showAds; // 0 = false (only show banner ads), 1 = true (show all types of ads)
        public bool showOnLevelStart;
        public bool extraAds;
        public int appOpenAd;

        public Ads()
        {
            showAds = 1;
            showOnLevelStart = false;
            extraAds = false;
            appOpenAd = 0;
        }
    }

    [System.Serializable]
    public class PushNotification
    {
        public List<string> showLocation;

        public PushNotification()
        {
            string showLocation_defulat = "onLoad";
            showLocation = new List<string>();
            showLocation.Add(showLocation_defulat);
        }
    }

    [System.Serializable]
    public class UserData
    {
        public int appSessionNumber;
    }

    [System.Serializable]
    public class PurchasePopUp
    {
        public string productId = "";
        public string showInLevels = "";
    }

    [System.Serializable]
    public class BrightData
    {
        public int showOnStart;
        public string showInLevels;
    }
}
