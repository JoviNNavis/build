using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.Events;
using TMPro;

public class TestingModeSelection : MonoBehaviour
{
    public static UnityAction<AppCentralSDKFeatureType> UIStateChangeEvent;

    public AppCentralSDKFeatureType appCentralSDKFeatureType;
    private TMP_Dropdown dropdown;

    private void Start()
    {
        dropdown = GetComponent<TMP_Dropdown>();
        initializeDropDown();
    }

    private void initializeDropDown()
    {
        dropdown.ClearOptions();

        List<string> AllTypes = new List<string>();

        for (int i = 0; i < AppCentralSDKFeatureType.GetNames(typeof(AppCentralSDKFeatureType)).Length +2; i++)
        {
            AppCentralSDKFeatureType fType = (AppCentralSDKFeatureType)i;
            AllTypes.Add(fType.ToString());
        }

        dropdown.AddOptions(AllTypes);

    }

    public void OnStateChange(int index)
    {
        appCentralSDKFeatureType = (AppCentralSDKFeatureType)index;
        UIStateChangeEvent?.Invoke(appCentralSDKFeatureType);

        ACLogger.UserDebug(":  Debug State Change to " + appCentralSDKFeatureType.ToString());
    }
}

public enum AppCentralSDKFeatureType
{
    GamePlay,
    GameAnalytics,
    AppsLovin,
    RateUsDialog,
    FlightModePrompt,
    InApps,
    URLScheme,
}
