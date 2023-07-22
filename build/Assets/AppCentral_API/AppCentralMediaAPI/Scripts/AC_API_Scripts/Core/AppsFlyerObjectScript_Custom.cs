using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppsFlyerSDK;

// This class is intended to be used the the AppsFlyerObject.prefab
namespace AppCentralCore
{
    public class AppsFlyerObjectScript_Custom : MonoBehaviour, IAppsFlyerConversionData
    {
        public static AppsFlyerObjectScript_Custom Instance;

        public void Awake()
        {
            if (true)
            {
                if (!Instance)
                {
                    Instance = this;
                }
                else
                {
                    Destroy(gameObject);
                }
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                if (!Instance)
                    Instance = this;
            }
        }

        // These fields are set from the editor so do not modify!
        //******************************//
        public string devKey;
        public string appID;
        public string UWPAppID;
        public bool isDebug;
        public bool getConversionData;

        //******************************//


        public void Setup_AppsFlyerSetting(string _devKey, string _appID)
        {
            devKey = _devKey;
            appID = _appID;
            isDebug = true;
            getConversionData = true;
        }

        public void initialize()
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                onConversionDataFail("No, Internet");
                return;
            }

            // These fields are set from the editor so do not modify!
            //******************************//
            AppsFlyer.setIsDebug(isDebug);
#if UNITY_WSA_10_0 && !UNITY_EDITOR
        AppsFlyer.initSDK(devKey, UWPAppID, getConversionData ? this : null);
#elif UNITY_STANDALONE_OSX && !UNITY_EDITOR
    AppsFlyer.initSDK(devKey, macOSAppID, getConversionData ? this : null);
#else
            AppsFlyer.initSDK(devKey, appID, getConversionData ? this : null);
#endif
            //******************************/

            AppsFlyer.startSDK();
        }

        public bool OneTime = true;

        public void onConversionDataSuccess(string conversionData)
        {
            if (!OneTime)
            {
                return;
            }

            AppsFlyer.AFLog("onConversionDataSuccess", conversionData);

            Dictionary<string, object> conversionDataDictionary =
                AppsFlyer.CallbackStringToDictionary(conversionData);
            // Get all Conversion Data and Add it to the API URL
            foreach (KeyValuePair<string, object> item in conversionDataDictionary)
            {
                if (item.Key != null && item.Value != null)
                {
                    AppCentralUnityApi_Internal.Instance.jsonController.Current_URL += "&";
                    AppCentralUnityApi_Internal.Instance.jsonController.Current_URL +=
                        "af_" + item.Key;
                    AppCentralUnityApi_Internal.Instance.jsonController.Current_URL += "=";
                    AppCentralUnityApi_Internal.Instance.jsonController.Current_URL +=
                        item.Value.ToString();

                    //AppCentralUnityApi.Instance.apiUrl += "&";
                    //AppCentralUnityApi.Instance.apiUrl += "af_" + item.Key;
                    //AppCentralUnityApi.Instance.apiUrl += "=";
                    //AppCentralUnityApi.Instance.apiUrl += item.Value.ToString();
                }
            }
            ACLogger.UserDebug(
                "API URL - " + AppCentralUnityApi_Internal.Instance.jsonController.Current_URL
            );
            AppCentralUnityApi_Internal.Instance.jsonController.AppsFlyerIsReady();
            OneTime = false;
        }

        public void onConversionDataFail(string error)
        {
            if (!OneTime)
            {
                return;
            }
            AppsFlyer.AFLog("onConversionDataFail", error);

            AppCentralUnityApi_Internal.Instance.jsonController.AppsFlyerIsReady();
            OneTime = false;
        }

        public void onAppOpenAttribution(string attributionData)
        {
            AppsFlyer.AFLog("onAppOpenAttribution", attributionData);
            Dictionary<string, object> attributionDataDictionary =
                AppsFlyer.CallbackStringToDictionary(attributionData);
            // add direct deeplink logic here
        }

        public void onAppOpenAttributionFailure(string error)
        {
            AppsFlyer.AFLog("onAppOpenAttributionFailure", error);
        }
    }
}
