using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AppCentralAPI
{
    public class AppCentral_ENUMS { }

    public enum PickUp
    {
        Box,
        Money
    }

    public enum Gender
    {
        Male,
        Female,
    }

    public enum GenderPopUpShowLocation
    {
        onStart,
        inGame
    }

    public enum PixelBonusActionType
    {
        @default,
        start_rewarded,
        start_play,
        skipped
    }

    public enum PixelBuildActionType
    {
        @default,
        start,
        end
    }

    public enum PixelBuildEndType
    {
        none,
        exit_build,
        minimize_app
    }

    public enum PAYWALL_TYPE
    {
        // these names will be use a pixel names for paywall

        @default,
        dynamic,
        midGame,
        popup
    }

    public enum MidGamePaywallAdInteraction
    {
        after,
        instead
    }

    public enum SHOW_LOCATION
    {
        onLoad,
        startPlay,
        deepInGame,
        adsUnavailable
    }

    public enum API_URL
    {
        Appcentral_WhenLive,
        LocalServer_DontUse,
        GitPersonal_DontUse,
        TestingIdfa_EditorTesting
    }

    public enum Progresion01
    {
        level,
        bonus
    }

    public enum Progresion02
    {
        @default,
        mode
    }

    public enum CustomDimension01
    {
        theme1,
        theme2,
    }

    public enum CustomDimension02
    {
        Restricted,
        Authorized,
        Denied,
        NotDetermined
    }

    public enum BannerPosition
    {
        TopLeft,
        TopCenter,
        TopRight,
        Centered,
        CenterLeft,
        CenterRight,
        BottomLeft,
        BottomCenter,
        BottomRight
    }

    public enum RateUsThemeMode
    {
        DarkMode,
        LightMode,
    }

    public enum AppCentralPlugins
    {
        AC_MEDIA_SDK,
        AC_ADS_SDK,

        AC_APPSFLYER,
        AC_GAMEANALYTICS,
        AC_SMARTLOOK,
        AC_ONESIGNAL,
        AC_APPLOVIN,
        AC_IAPS
    }

    public enum BrightDataUserConsentChoice
    {
        accepted,
        declined
    }
}
