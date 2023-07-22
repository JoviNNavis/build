using System;
using System.Collections;
using UnityEngine;
using UnityEngine.iOS;
using UnityEngine.Networking;
using AppCentralAPI;

namespace AppCentralCore
{
    public class AppCentralThemeSwitchController : MonoBehaviour
    {
        public static Action OnInitializeThemePerms;

        public static void InitializeThemePerms_old()
        {
            // ACLogger.UserDebug(": InitializeThemePerms called");

            // // Set currentTheme to current_theme if was saved, if not set to default
            // string currentTheme = PlayerPrefs.GetString("current_theme", "default");
            // // Set themeToChangeTo to theme_main if was saved, if not set to default
            // string themeToChangeTo = PlayerPrefs.GetString("theme_main", "default");

            // // Check if theme_main was recieved at some point
            // if (themeToChangeTo == "default" || themeToChangeTo == "theme2")
            // {
            //     // No theme to change to
            //     ACLogger.UserDebug(
            //         ": no theme was set to theme_main at player prefs, don't change theme"
            //     );
            //     // OnInitializeThemePerms?.Invoke(false, currentTheme);
            //     OnInitializeThemePerms?.Invoke(false, currentTheme);

            //     return;
            // }

            // // If currentTheme is the same as the themeToChangeTo do nothing.
            // // or If currentTheme is theme1, don't change the theme (unless forceJump is true).
            // if (
            //     currentTheme == themeToChangeTo
            //     || (
            //         currentTheme == "theme1"
            //         && AppCentralUnityApi_Internal.Instance.getJsonObj().theme.forceTheme == false
            //     )
            // )
            // {
            //     ACLogger.UserDebug(
            //         ": theme to change to is the same as the current theme or the current theme is theme1, don't change theme. current theme - "
            //             + currentTheme
            //     );
            //     OnInitializeThemePerms?.Invoke(false, currentTheme);
            //     return;
            // }

            // AppCentralUnityApi_Internal.Instance.getJsonObj().theme.main = themeToChangeTo;
            // AppCentralUnityApi_Internal.Instance.SaveJSONData();

            // AppCentralGameAnalyticsInternal.setCustomDimention01(themeToChangeTo);

            // // Call the event - theme needs to be changed

            // //ThemeChangedEvent?.Invoke(themeToChangeTo);

            // // Set Current Theme
            // PlayerPrefs.SetString("current_theme", themeToChangeTo);

            // ACLogger.UserDebug(": InitializeThemePerms theme string set to " + themeToChangeTo);

            // OnInitializeThemePerms?.Invoke(true, themeToChangeTo);
        }

        public static void InitializeThemePerms_old_CHEN()
        {
            // ACLogger.UserDebug(": InitializeThemePerms called");

            // // Set currentTheme to current_theme if was saved, if not set to default
            // string currentTheme = PlayerPrefs.GetString("current_theme", "theme2");
            // // Set themeToChangeTo to theme_main if was saved, if not set to default
            // string themeToChangeTo = PlayerPrefs.GetString("theme_main", "theme2");

            // themeToChangeTo = AppCentralUnityApi_Internal.Instance.getJsonObj().theme.main;

            // // Check if theme_main was recieved at some point
            // if (themeToChangeTo == "theme2")
            // {
            //     // No theme to change to
            //     ACLogger.UserDebug(
            //         ": no theme was set to theme_main at player prefs, don't change theme"
            //     );
            //     // OnInitializeThemePerms?.Invoke(false, currentTheme);
            //     OnInitializeThemePerms?.Invoke(false, themeToChangeTo);
            //     return;
            // }

            // // If currentTheme is the same as the themeToChangeTo do nothing.
            // // or If currentTheme is theme1, don't change the theme (unless forceJump is true).
            // if (
            //     currentTheme == themeToChangeTo
            //     || (
            //         currentTheme == "theme1"
            //         && AppCentralUnityApi_Internal.Instance.getJsonObj().theme.forceTheme == false
            //     )
            // )
            // {
            //     ACLogger.UserDebug(
            //         ": theme to change to is the same as the current theme or the current theme is theme1, don't change theme. current theme - "
            //             + currentTheme
            //     );
            //     OnInitializeThemePerms?.Invoke(false, currentTheme);
            //     return;
            // }

            // AppCentralUnityApi_Internal.Instance.getJsonObj().theme.main = themeToChangeTo;
            // AppCentralUnityApi_Internal.Instance.SaveJSONData();

            // AppCentralGameAnalyticsInternal.setCustomDimention01(themeToChangeTo);

            // // Call the event - theme needs to be changed

            // //ThemeChangedEvent?.Invoke(themeToChangeTo);

            // // Set Current Theme
            // PlayerPrefs.SetString("current_theme", themeToChangeTo);

            // ACLogger.UserDebug(": InitializeThemePerms theme string set to " + themeToChangeTo);

            // OnInitializeThemePerms?.Invoke(true, themeToChangeTo);
        }

        public static void InitializeThemePerms_old2()
        {
            // ACLogger.UserDebug(": InitializeThemePerms called");

            // // Set currentTheme to current_theme if was saved, if not set to default
            // string currentTheme = PlayerPrefs.GetString("current_theme", "default");
            // // Set themeToChangeTo to theme_main if was saved, if not set to default
            // string themeToChangeTo = PlayerPrefs.GetString("theme_main", "default");

            // themeToChangeTo = AppCentralUnityApi_Internal.Instance.getJsonObj().theme.main;

            // // Check if theme_main was recieved at some point
            // if (themeToChangeTo == "default" || themeToChangeTo == "theme2")
            // {
            //     // No theme to change to
            //     ACLogger.UserDebug(
            //         ": no theme was set to theme_main at player prefs, don't change theme"
            //     );
            //     OnInitializeThemePerms?.Invoke(false, "default");
            //     return;
            // }

            // AppCentralUnityApi_Internal.Instance.getJsonObj().theme.main = themeToChangeTo;
            // AppCentralUnityApi_Internal.Instance.SaveJSONData();

            // AppCentralGameAnalyticsInternal.setCustomDimention01(themeToChangeTo);

            // PlayerPrefs.SetString("current_theme", themeToChangeTo);

            // ACLogger.UserDebug(": InitializeThemePerms theme string set to " + themeToChangeTo);

            // OnInitializeThemePerms?.Invoke(true, themeToChangeTo);
        }

        public static void InitializeThemePerms()
        {
            // Set currentTheme to current_theme if was saved, if not set to default
            string currentTheme = PlayerPrefs.GetString("current_theme", "theme2");
            // Set themeToChangeTo to theme_main if was saved, if not set to default
            string themeToChangeTo = PlayerPrefs.GetString("theme_main", "theme2");

            // Check if theme_main was recieved at some point
            if (themeToChangeTo == "theme2")
            {
                // No theme to change to
                ACLogger.UserDebug("no theme was set to theme_main at player prefs, don't change theme");

                OnInitializeThemePerms?.Invoke();

                return;
            }

            // If currentTheme is the same as the themeToChangeTo do nothing.
            // or If currentTheme is theme1, don't change the theme (unless forceJump is true).
            if (
                currentTheme == themeToChangeTo
                || (
                    currentTheme == "theme1"
                    && AppCentralUnityApi_Internal.Instance.getJsonObj().theme.forceTheme == false
                )
            )
            {
                ACLogger.UserDebug(
                    "theme to change to is the same as the current theme or the current theme is theme1, don't change theme. current theme - "
                        + currentTheme
                );
                OnInitializeThemePerms?.Invoke();
                return;
            }

            // Call the event - theme needs to be changed
            AppCentralGameAnalyticsInternal.setCustomDimention01(themeToChangeTo);
            // Set Current Theme
            PlayerPrefs.SetString("current_theme", themeToChangeTo);

            OnInitializeThemePerms?.Invoke();
        }

        public static string getCurrentThemeName()
        {
            return PlayerPrefs.GetString("current_theme", "theme2");
        }
    }
}
