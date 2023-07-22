using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppCentralAPI;
using AppCentralCore;

namespace GH
{
    public class RateUsController : MonoBehaviour
    {
        private static RateUsController instance;

        public static RateUsController Instance
        {
            get => instance;
            private set => instance = value;
        }

        public UI_DynamicRateUsMenu CurrentRateUsTheme;

        [SerializeField]
        private UI_DynamicRateUsMenu RateUsDarkTheme;

        [SerializeField]
        private UI_DynamicRateUsMenu RateUsLightTheme;

        public RateUsThemeMode rateUsThemeMode;
        public int TestLevelID;

        private void Awake()
        {
            if (!Instance)
                Instance = this;
        }

        public bool ActivateRateUS(int LevelID, Action onCloseCallback = null)
        {
            ACLogger.UserDebug(": RateUS requested");

            newSetting settings = AppCentralSettings.LoadSetting();

            if (!settings.UseRateUS)
            {
                ACLogger.UserDebug(
                    ": UseRateUS option is disable in the Settings, so returning false always"
                );
                onCloseCallback?.Invoke();
                return false;
            }

            bool isDarkMode = true;

#if UNITY_EDITOR

            switch (rateUsThemeMode)
            {
                case RateUsThemeMode.DarkMode:
                    isDarkMode = true;
                    break;
                case RateUsThemeMode.LightMode:
                    isDarkMode = false;
                    break;
            }

#else
            isDarkMode = IsDarkMode_Plugin.IsDakModeSet();
#endif

            ACLogger.UserDebug(": RateUS theme is Dark=" + isDarkMode);

            if (isDarkMode)
            {
                CurrentRateUsTheme = RateUsDarkTheme;
            }
            else
            {
                CurrentRateUsTheme = RateUsLightTheme;
            }

            return CurrentRateUsTheme.CheckForRateUS(onCloseCallback, LevelID);
        }

        void CheckRateUS()
        {
            ActivateRateUS(TestLevelID);
        }

        void ClearPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}
