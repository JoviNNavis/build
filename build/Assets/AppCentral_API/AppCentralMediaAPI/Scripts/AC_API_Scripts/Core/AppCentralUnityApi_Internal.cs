using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.iOS;
using AppCentralAPI;
using System.Linq;
using UnityEngine;
using System;

namespace AppCentralCore
{
    public class AppCentralUnityApi_Internal : MonoBehaviour
    {
        // AppCentralUnityApi singleton, Instance
        private static AppCentralUnityApi_Internal _instance;
        public static AppCentralUnityApi_Internal Instance
        {
            get { return _instance; }
        }

        public Appcentral_ReferenceManager appcentral_ReferenceManager;

        [SerializeField]
        public AppCentral_JSON_Controller jsonController;

        //// Events
        public static event Action<string> ThemeChangedEvent;
        public static event Action<bool> RecievedAppCentralApiResponse;
        public static event Action IAPInitializationCompleted;
        public static event Action OnAppLaunch;
        public static event Action OnAppJumpFinished;
        public static event Action OnLoadEvent;
        public static event Action StartPlayEvent;
        public static event Action DeepinGameEvent;

        public static event Action<int> OnLevelStartEvent;
        public static event Action<int> OnLevelCompleteEvent;

        ////----------------------------------||  Lifecycle Methods  ||----------------------------------//


        private bool IsOnLoadEventHappened = false;

        private void Awake()
        {
            if (!_instance)
                _instance = this;
        }

        IEnumerator Start()
        {
            // Set Target Frame Rate / FPS to 60
            Application.targetFrameRate = 60;
            yield return new WaitForSeconds(0.5f);
            AppLaunched();
            jsonController.Initialize_AppCentral_API();
        }

        private void OnEnable()
        {
            AppCentral_JSON_Controller.appCentral_JSON_Controller += OnJSON_InitializationComplete;
            SceneManager.sceneLoaded += onGamePlaySceneLoaded;
        }

        private void OnJSON_InitializationComplete(
            AppCentral_JSON_Controller appCentral_JSON_VersionManager
        )
        {
            ACLogger.UserDebug(": OnJSON_InitializationComplete");

            jsonController = appCentral_JSON_VersionManager;

            RecievedAppCentralApiResponse?.Invoke(true);

            //OnLoaded();
        }

        private void onGamePlaySceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (IsOnLoadEventHappened)
            {
                ACLogger.UserDebug("New scene loaded: " + scene.name);
                StartPlay();
            }
        }

        bool oneTime = true;

        public void OnIAPInitializationCompleted()
        {
            if (oneTime)
            {
                ACLogger.UserDebug(": OnIAPInitializationCompleted");

                IAPInitializationCompleted?.Invoke();
                OnLoaded();
                oneTime = false;
            }
        }

        private void AppLaunched()
        {
            OnAppLaunch?.Invoke();
        }

        public static void AppJumpFinished()
        {
            OnAppJumpFinished?.Invoke();
        }

        private void OnLoaded()
        {
            ACLogger.UserDebug(": OnLoaded");
            OnLoadEvent?.Invoke();
        }

        bool onetimeStartPlay = true;

        public void StartPlay()
        {
            if (onetimeStartPlay)
            {
                onetimeStartPlay = false;
                StartPlayEvent?.Invoke();
                ACLogger.UserDebug(": StartPlay Event Raised");
            }
        }

        public void levelStart(int CurrentLevelID)
        {
            AppCentralUnityApi_Internal.setlevelPref(CurrentLevelID);

            ACLogger.UserDebug("Level start Event Raised: " + CurrentLevelID);
            AppCentralSmartLookEvents.SmartLookTrackLevelStart(CurrentLevelID);
            OnLevelStartEvent?.Invoke(CurrentLevelID);
        }

        public void levelComplete(int CurrentLevelID)
        {
            ACLogger.UserDebug("Level Complete Event Raised: " + CurrentLevelID);
            OnLevelCompleteEvent?.Invoke(CurrentLevelID);

        }

        bool onetimeDeepInGame = true;

        public void DeepInGame()
        {
            if (onetimeDeepInGame)
            {
                ACLogger.UserDebug(": DeepInGame Event Raised");
                onetimeDeepInGame = false;
                DeepinGameEvent?.Invoke();

            }
        }

        public void SetUpTheme()
        {
            ThemeChangedEvent?.Invoke(jsonController.JsonObject.theme.main);
        }

        public AppCentralUnityApiJsonDetails getJsonObj()
        {
            return jsonController.JsonObject;
        }

        public void SaveJSONData()
        {
            jsonController.UpdateDataToLocalMemory();
        }

        public static bool IsCurrentLevelIsValideToShowMidGamePaywall(
            string levelsList,
            int CurrentLevelIndex
        )
        {
            bool canShow = false;

            List<string> allLevels = levelsList.Split(',').ToList<string>();

            if (allLevels.Contains(CurrentLevelIndex.ToString()))
            {
                canShow = true;
            }

            return canShow;
        }

        public static bool IsDynamicPaywallLocationsContain(SHOW_LOCATION sHOW_LOCATION)
        {
            bool IsContain = false;

            switch (sHOW_LOCATION)
            {
                case SHOW_LOCATION.onLoad:
                    IsContain = AppCentralPrefsManager.IsContain_onLoad();
                    break;
                case SHOW_LOCATION.startPlay:
                    IsContain = AppCentralPrefsManager.IsContain_startPLay();
                    break;
                case SHOW_LOCATION.deepInGame:
                    IsContain = AppCentralPrefsManager.IsContain_deepInGame();
                    break;
                case SHOW_LOCATION.adsUnavailable:
                    IsContain = AppCentralPrefsManager.IsContain_adsUnavailable();
                    break;
            }

            return IsContain;
        }

        #region UserGameprefs




        public static void setlevelPref(int levelID)
        {
            PlayerPrefs.SetInt("AC_Levelinfo", levelID);
        }

        public static int getLevelPref()
        {
            return PlayerPrefs.GetInt("AC_Levelinfo", 0);
        }

        public static void setTotalLevelCompletePref(int levelID)
        {
            PlayerPrefs.SetInt("AC_AllCompletedLevels", levelID);
        }

        public static int getTotalLevelCompletePref()
        {
            return PlayerPrefs.GetInt("AC_AllCompletedLevels", 0);
        }

        #endregion








        private void OnDisable()
        {
            AppCentral_JSON_Controller.appCentral_JSON_Controller -= OnJSON_InitializationComplete;
            SceneManager.sceneLoaded -= onGamePlaySceneLoaded;
        }
    }
}
