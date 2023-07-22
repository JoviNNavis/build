using System.Collections;
using UnityEngine;
using UnityEngine.iOS;
using UnityEngine.Networking;
using UnityEngine.Events;
using System;
using AppCentralAPI;

namespace AppCentralCore
{
    public class AppCentral_JSON_Controller : MonoBehaviour
    {
        public API_URL Current_API_URL;

        [System.Serializable]
        public class API_Variables
        {
            public string idfv;
            public string appVersion;
            public string package;
            public string idfa;
            public string deviceModel;
            public string generation;
            public int numOfFailedApiRequestRetries = 0;
            public int timeout = 5;
            public string apiUrl = "";
            public string apiText = "";

            [HideInInspector]
            public bool isRoblox = false;
        }

        [SerializeField]
        private AppCentralUnityApiJsonDetails jsonObject = new AppCentralUnityApiJsonDetails();

        public AppCentralUnityApiJsonDetails JsonObject
        {
            get => jsonObject;
        }

        public static event Action<AppCentral_JSON_Controller> appCentral_JSON_Controller;

        public const string localFolderPath_EDITOR_Testing =
            "Assets/AppCentral_API/AppCentralMediaAPI/Editor/";
        public const string FileName = "AppCentral_API";
        private string FilePath = "";

        public const string API_URL_PathPref = "API_URL_PathPref";

        [HideInInspector]
        public String Current_URL;

        // Api variables

        public API_Variables aPI_Variables = new API_Variables();

        public void Initialize_AppCentral_API()
        {
            ACLogger.UserDebug(": Initialization of Appcentral API called");

            SetFileSaveAddress();
            Initialize_API_URL_Perms();
            AppCentralPrefsManager.PopulatePlayerPrefsWithDefaultSettings(aPI_Variables);

#if UNITY_EDITOR
            //AppsFlyerIsReady();
#endif
        }

        public void AppsFlyerIsReady()
        {
            CheckServerForLatestVersion(ServerSideCallbackReceiver);
        }

        private void ServerSideCallbackReceiver(AppCentralUnityApiJsonDetails callback)
        {
            if (callback == null)
            {
                ACLogger.UserDebug(": Couldnot receive JSON data from server");

                jsonObject = ReadDataFromLocalMemory();
            }
            else
            {
                ACLogger.UserDebug(
                    ": SuccessfullyReceived data from server with API= " + callback
                );

                jsonObject = callback;
                UpdateDataToLocalMemory();
            }

            appCentral_JSON_Controller(this);
        }

        private void Initialize_API_URL_Perms() // SET apiUrl variables
        {
            aPI_Variables.idfv = Device.vendorIdentifier.ToString();
            aPI_Variables.appVersion = Application.version;
            aPI_Variables.package = Application.identifier;
            aPI_Variables.deviceModel = SystemInfo.deviceModel.ToString();
            aPI_Variables.generation = Device.generation.ToString();

#if AC_NEW_API || AC_OLDER_API

            aPI_Variables.isRoblox = true;
#else
            aPI_Variables.isRoblox = false;
#endif

#if UNITY_EDITOR

            aPI_Variables.apiUrl = Set_apiUrl_Path(Current_API_URL);
#else
            aPI_Variables.apiUrl = Set_apiUrl_Path(API_URL.Appcentral_WhenLive);
#endif
        }

        private AppCentralUnityApiJsonDetails ReadDataFromLocalMemory()
        {
            if (FileHandler.IsFileExist(FilePath))
            {
                ACLogger.UserDebug(
                    ": Previous File Existes in local directory, so reading form it"
                );

                jsonObject = FileHandler.ReadFromJSON<AppCentralUnityApiJsonDetails>(FilePath);
                ACLogger.UserDebug(": Local API Responce=" + JsonObject);
            }
            else
            {
                ACLogger.UserDebug(
                    ": Previous File do not existes, so creating new one with defulat setings"
                );
                UpdateDataToLocalMemory();
            }

            return JsonObject;
        }

        public void UpdateDataToLocalMemory()
        {
            ACLogger.UserDebug(": Saving All Data to locally");

            FileHandler.SaveToJSON<AppCentralUnityApiJsonDetails>(JsonObject, FilePath);
            AppCentralPrefsManager.SaveAppCentralUnityApiDataToPlayerPrefs(
                aPI_Variables,
                jsonObject
            );
        }

        private void CheckServerForLatestVersion(
            UnityAction<AppCentralUnityApiJsonDetails> callback
        )
        {
            StartCoroutine(GetRequest(callback));
        }

        private string Set_apiUrl_Path(API_URL aPI_URL)
        {
            string SDK_Version = AppCentralSDKVersionTracker.ACSDKVersion;

            string LiveAPIURL =
                // $"https://paywall.bundlize.com/?sdkVersion=4.0.0&version={aPI_Variables.appVersion}&package={aPI_Variables.package}&idfv={aPI_Variables.idfv}&deviceModel={aPI_Variables.deviceModel}&generation={aPI_Variables.generation}&isRoblox={aPI_Variables.isRoblox}";
                $"https://paywall.bundlize.com/?sdkVersion={SDK_Version}&version={aPI_Variables.appVersion}&package={aPI_Variables.package}&idfv={aPI_Variables.idfv}&deviceModel={aPI_Variables.deviceModel}&generation={aPI_Variables.generation}&isRoblox={aPI_Variables.isRoblox}";

            string TestingIDFA_APIURL =
                $"https://paywall.bundlize.com/?sdkVersion={SDK_Version}&version={aPI_Variables.appVersion}&package={aPI_Variables.package}&idfv={"testing"}&deviceModel={aPI_Variables.deviceModel}&generation={aPI_Variables.generation}&isRoblox={aPI_Variables.isRoblox}";
            ;
            string EditorTestingURL_LocalServer = "http://192.168.100.3/AppCentral_API.json";
            string GitTestingURL =
                "https://raw.githubusercontent.com/NomanAliKhokhar/GamyInteractive_InHouseAds_Json_Files/master/AC_API_Dummy";

            switch (aPI_URL)
            {
                case API_URL.Appcentral_WhenLive:
                    aPI_Variables.apiUrl = LiveAPIURL;
                    Current_API_URL = API_URL.Appcentral_WhenLive;
                    break;
                case API_URL.GitPersonal_DontUse:
                    aPI_Variables.apiUrl = GitTestingURL;
                    Current_API_URL = API_URL.GitPersonal_DontUse;
                    break;
                case API_URL.LocalServer_DontUse:
                    aPI_Variables.apiUrl = EditorTestingURL_LocalServer;
                    Current_API_URL = API_URL.LocalServer_DontUse;
                    break;
                case API_URL.TestingIdfa_EditorTesting:
                    aPI_Variables.apiUrl = TestingIDFA_APIURL;
                    Current_API_URL = API_URL.TestingIdfa_EditorTesting;
                    break;
                default:
                    aPI_Variables.apiUrl = LiveAPIURL;
                    Current_API_URL = API_URL.Appcentral_WhenLive;
                    break;
            }

            // switch (aPI_URL)
            // {
            //     case API_URL.Appcentral_WhenLive:
            //         //aPI_Variables.apiUrl = $"https://paywall.bundlize.com/?sdkVersion=4.0.0&version={aPI_Variables.appVersion}&package={aPI_Variables.package}&idfv={aPI_Variables.idfv}&deviceModel={aPI_Variables.deviceModel}&generation={aPI_Variables.generation}"; // from
            //         aPI_Variables.apiUrl =
            //             $"https://paywall.bundlize.com/?sdkVersion=4.0.0&version={aPI_Variables.appVersion}&package={aPI_Variables.package}&idfv={aPI_Variables.idfv}&deviceModel={aPI_Variables.deviceModel}&generation={aPI_Variables.generation}&isRoblox={aPI_Variables.isRoblox}"; // this
            //         Current_API_URL = API_URL.Appcentral_WhenLive;
            //         break;
            //     case API_URL.GitPersonal_DontUse:
            //         aPI_Variables.apiUrl =
            //             "https://raw.githubusercontent.com/NomanAliKhokhar/GamyInteractive_InHouseAds_Json_Files/master/AC_API_Dummy";
            //         Current_API_URL = API_URL.GitPersonal_DontUse;
            //         break;
            //     case API_URL.LocalServer_DontUse:
            //         aPI_Variables.apiUrl = "http://192.168.100.3/AppCentral_API.json";
            //         Current_API_URL = API_URL.LocalServer_DontUse;
            //         break;
            //     case API_URL.TestingIdfa_EditorTesting:
            //         //idfa = "testing";
            //         //idfv = "testing";
            //         aPI_Variables.idfa = "testing";
            //         aPI_Variables.idfv = "testing";
            //         aPI_Variables.apiUrl =
            //             $"https://paywall.bundlize.com/?sdkVersion=4.0.0&version={aPI_Variables.appVersion}&package={aPI_Variables.package}&idfv={aPI_Variables.idfv}&deviceModel={aPI_Variables.deviceModel}&generation={aPI_Variables.generation}&isRoblox={aPI_Variables.isRoblox}";
            //         Current_API_URL = API_URL.TestingIdfa_EditorTesting;
            //         break;
            //     default:
            //         aPI_Variables.apiUrl =
            //             $"https://paywall.bundlize.com/?sdkVersion=4.0.0&version={aPI_Variables.appVersion}&package={aPI_Variables.package}&idfv={aPI_Variables.idfv}&deviceModel={aPI_Variables.deviceModel}&generation={aPI_Variables.generation}";
            //         Current_API_URL = API_URL.Appcentral_WhenLive;
            //         break;
            // }

            PlayerPrefs.SetInt(API_URL_PathPref, (int)aPI_URL);
            return aPI_Variables.apiUrl;
        }

        private IEnumerator GetRequest(UnityAction<AppCentralUnityApiJsonDetails> callback)
        {
            ACLogger.UserDebug(": trying to get a successful api response");
            // Get idfa
            aPI_Variables.idfa = Device.advertisingIdentifier.ToString();
            // Add Idfa and numOfRequestRetries to the apiUrl before sending it to the server

            string apiUrlWithRetries = "";
            switch (Current_API_URL)
            {
                case API_URL.Appcentral_WhenLive:
                    apiUrlWithRetries =
                        aPI_Variables.apiUrl
                        + "&idfa="
                        + aPI_Variables.idfa
                        + "&numOfRequestRetries="
                        + aPI_Variables.numOfFailedApiRequestRetries;
                    break;
                case API_URL.LocalServer_DontUse:
                    apiUrlWithRetries = Set_apiUrl_Path(API_URL.LocalServer_DontUse);
                    //apiUrlWithRetries = Set_apiUrl_Path((API_URL)PlayerPrefs.GetInt(API_URL_PathPref));
                    break;
                case API_URL.GitPersonal_DontUse:
                    apiUrlWithRetries = Set_apiUrl_Path(API_URL.GitPersonal_DontUse);
                    //apiUrlWithRetries = Set_apiUrl_Path((API_URL)PlayerPrefs.GetInt(API_URL_PathPref));
                    break;
                case API_URL.TestingIdfa_EditorTesting:
                    apiUrlWithRetries =
                        aPI_Variables.apiUrl
                        + "&idfa="
                        + aPI_Variables.idfa
                        + "&numOfRequestRetries="
                        + aPI_Variables.numOfFailedApiRequestRetries;
                    break;
                default:
                    apiUrlWithRetries =
                        aPI_Variables.apiUrl
                        + "&idfa="
                        + aPI_Variables.idfa
                        + "&numOfRequestRetries="
                        + aPI_Variables.numOfFailedApiRequestRetries;
                    break;
            }

            Current_URL = apiUrlWithRetries;
            ACLogger.UserDebug(": apiUrl=" + apiUrlWithRetries);

            UnityWebRequest uwr = UnityWebRequest.Get(apiUrlWithRetries);
            // Set request timeout
            uwr.timeout = aPI_Variables.timeout;
            // Wait for api response
            yield return uwr.SendWebRequest();

            if (uwr.result != UnityWebRequest.Result.Success)
            {
                // Api request didn't get a Successful result
                ACLogger.UserDebug(": API request - Error While Sending: " + uwr.error);

                // This is the 1st retry to get a successful response from the Api
                if (aPI_Variables.numOfFailedApiRequestRetries < 1)
                {
                    // Shorten the timeout
                    aPI_Variables.timeout = 5;
                    aPI_Variables.numOfFailedApiRequestRetries++;
                    // Try getting a successful response from the Api again
                    StartCoroutine(GetRequest(callback));
                }
                else
                {
                    // This is not the 1st retry to get a successful response from the Api
                    ACLogger.UserDebug(
                        ": Wasn't able to complete the API request succesfuly, Start game."
                    );
                    // Reset numOfFailedApiRequestRetries
                    aPI_Variables.numOfFailedApiRequestRetries = 0;

                    // Notify event - received a non-successful response from AppCentralAPI
                    //RecievedAppCentralApiResponse?.Invoke(false);

                    callback?.Invoke(null);
                }
            }
            else
            {
                // Api request got a Successful result
                try
                {
                    aPI_Variables.apiText = uwr.downloadHandler.text;
                    ACLogger.UserDebug(
                        ": API request - Received: " + aPI_Variables.apiText
                    );
                    AppCentralUnityApiJsonDetails jsonObject =
                        JsonUtility.FromJson<AppCentralUnityApiJsonDetails>(aPI_Variables.apiText);

                    //SaveAppCentralUnityApiDataToPlayerPrefs();
                    //PopulateJsobObjWithDefaultSettings();
                    //AppJumpController.Instance.TryAppJumping();
                    // Reset numOfFailedApiRequestRetries


                    aPI_Variables.numOfFailedApiRequestRetries = 0;
                    // Notify event - received successful response from AppCentralAPI
                    //RecievedAppCentralApiResponse?.Invoke(true);

                    callback.Invoke(jsonObject);
                }
                catch (System.Exception e)
                {
                    ACLogger.UserDebug(
                        ": Got a Successful response from the API but still something went wrong - "
                            + e.ToString()
                    );
                    // Notify event - received a non-successful response from AppCentralAPI
                    //RecievedAppCentralApiResponse?.Invoke(false);

                    callback.Invoke(null);
                }
            }
        }

        private void SetFileSaveAddress()
        {
#if UNITY_EDITOR
            FilePath = localFolderPath_EDITOR_Testing + FileName + ".json";
#else
            FilePath = Application.persistentDataPath + "/" + FileName + ".json";
#endif
        }
    }
}
