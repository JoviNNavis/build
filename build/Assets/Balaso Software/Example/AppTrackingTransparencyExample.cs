using System.Collections;
using Balaso;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

/// <summary>
/// Example MonoBehaviour class 
/// iOS Tracking Authorization
/// </summary>
public class AppTrackingTransparencyExample : MonoBehaviour
{

    public GameObject GameAnalyticsPrefeb;
    public GameObject AppFlyerPrefeb;

    private void Awake()
    {

#if UNITY_IOS
        AppTrackingTransparency.RegisterAppForAdNetworkAttribution();
        AppTrackingTransparency.UpdateConversionValue(3);
#endif
    }

    void Start()
    {

#if UNITY_EDITOR

        OnAuthorizationRequestDone(AppTrackingTransparency.AuthorizationStatus.AUTHORIZED);
        ACLogger.UserDebug("OnAuthorizationRequestDone");

#elif UNITY_IOS

        AppTrackingTransparency.OnAuthorizationRequestDone += OnAuthorizationRequestDone;

        AppTrackingTransparency.AuthorizationStatus currentStatus = AppTrackingTransparency.TrackingAuthorizationStatus;
        ACLogger.UserDebug(string.Format("Current authorization status: {0}", currentStatus.ToString()));
        if (currentStatus != AppTrackingTransparency.AuthorizationStatus.AUTHORIZED)
        {
            ACLogger.UserDebug("Requesting authorization...");
            AppTrackingTransparency.RequestTrackingAuthorization();
        }

#endif


    }

#if UNITY_IOS

    /// <summary>
    /// Callback invoked with the user's decision
    /// </summary>
    /// <param name="status"></param>
    private void OnAuthorizationRequestDone(AppTrackingTransparency.AuthorizationStatus status)
    {
        bool IsAccess = false;

        switch (status)
        {
            case AppTrackingTransparency.AuthorizationStatus.NOT_DETERMINED:
                IsAccess = false;

                ACLogger.UserDebug("AuthorizationStatus: NOT_DETERMINED");
                break;
            case AppTrackingTransparency.AuthorizationStatus.RESTRICTED:
                ACLogger.UserDebug("AuthorizationStatus: RESTRICTED");
                IsAccess = false;

                break;
            case AppTrackingTransparency.AuthorizationStatus.DENIED:
                ACLogger.UserDebug("AuthorizationStatus: DENIED");
                IsAccess = false;
                break;
            case AppTrackingTransparency.AuthorizationStatus.AUTHORIZED:
                
                IsAccess = true;
                ACLogger.UserDebug("AuthorizationStatus: AUTHORIZED");
                break;
        }


        if (IsAccess)
        {
            Instantiate(GameAnalyticsPrefeb);
            Instantiate(AppFlyerPrefeb);
        }
        else
        {
            //SceneManager.LoadScene(2);
        }

        // Obtain IDFA
        ACLogger.UserDebug(string.Format("IDFA: {0}", AppTrackingTransparency.IdentifierForAdvertising()));
    }


   
#endif
}   
