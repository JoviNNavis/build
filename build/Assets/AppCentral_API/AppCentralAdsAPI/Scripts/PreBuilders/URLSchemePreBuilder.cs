using UnityEngine;
using UnityEditor;

public class URLSchemePreBuilder : MonoBehaviour
{

#if UNITY_EDITOR

    public static void BuildAppCentralURLScheme()
    {
        string[] URLSchemes = new string[1];

        string bundleID = PlayerSettings.applicationIdentifier;

        ACLogger.UserDebug("Current bundle ID=" + bundleID);

        string[] allChurn = bundleID.Split('.');

        for (int i = 1; i < allChurn.Length; i++)
        {
            URLSchemes[0] += allChurn[i];
        }

         ACLogger.UserDebug("URL SCHEME is=" + URLSchemes[0]);

        PlayerSettings.iOS.iOSUrlSchemes = URLSchemes;
        
    }

    public static void BuildAppCentralURLScheme(string bundleID)
    {
        string[] URLSchemes = new string[1];

         ACLogger.UserDebug("Current bundle ID=" + bundleID);

        string[] allChurn = bundleID.Split('.');

        for (int i = 1; i < allChurn.Length; i++)
        {
            URLSchemes[0] += allChurn[i];
        }

         ACLogger.UserDebug("URL SCHEME is=" + URLSchemes[0]);

        PlayerSettings.iOS.iOSUrlSchemes = URLSchemes;

    }

    public static void ClearAllurlSchemes()
    {
        PlayerSettings.iOS.iOSUrlSchemes = new string[0];
    }


    public static string GetAppCentralURLScheme()
    {
        if (PlayerSettings.iOS.iOSUrlSchemes.Length > 0)
        {
            return PlayerSettings.iOS.iOSUrlSchemes[0];

        }
        else
        {
            return "";
        }

    }

#endif
}
