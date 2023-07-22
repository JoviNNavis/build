using System;
using System.Collections.Generic;
using UnityEngine;
using AppCentralCore;

using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Security;

public class Purchase_GUI : MonoBehaviour
{
    private GUIStyle currentStyle = null;
    private int windowWidth;
    private int windowHeight;
    private Rect windowRect;
    public Boolean windowOpen = false;
    private String localizedPriceString = "Price";
    private Boolean priceReceived = false;
    private String localizedTitle = "Full Game";
    private String localizedDescription = "Access all levels";

    string inAppId;
    Action<bool, string> subscriptionRsponce;

    public void showEditorIAP_Window(string _inAppId, Action<bool, string> _subscriptionRsponce)
    {
        inAppId = _inAppId;
        subscriptionRsponce = _subscriptionRsponce;
        windowOpen = true;
        priceReceived = true;
    }

    void OnGUI()
    {
        if (currentStyle == null || currentStyle.normal.background == null)
        {
            currentStyle = new GUIStyle(GUI.skin.box);
            currentStyle.normal.background = MakeTex(2, 2, new Color(0.13f, 0.41f, 0.52f, 0.9f));
        }
        if (windowOpen)
        {
            GUI.BeginGroup(new Rect(Screen.width / 2 - 400, Screen.height / 2 - 300, 800, 600));

            windowWidth = 600;
            windowHeight = 600;
            windowRect = new Rect(
                (Screen.width - windowWidth) / 2,
                (Screen.height - windowHeight) / 2,
                windowWidth,
                windowHeight
            );

            GUI.ModalWindow(0, windowRect, PopWindow, "", currentStyle);

            GUI.EndGroup();
        }
    }

    void PopWindow(int windowID)
    {
        if (priceReceived)
        {
            PaywallWindow(windowID);
        }
        else
        {
            ErrorWindow(windowID);
        }
    }

    void PaywallWindow(int windowID)
    {
        GUIStyle myButtonStyle = new GUIStyle(GUI.skin.button);
        myButtonStyle.fontSize = 35;

        Color color = Color.black;
        color.a = 1f;
        GUI.backgroundColor = color;

        if (GUI.Button(new Rect(10, 10, 40, 40), "X", myButtonStyle))
        {
            windowOpen = false;
        }

        GUIStyle myLabelStyle = new GUIStyle(GUI.skin.label);
        myLabelStyle.fontSize = 35;
        myLabelStyle.alignment = TextAnchor.UpperCenter;
        GUI.Label(
            new Rect(0, 100, windowWidth, 300),
            localizedTitle
                + "\n"
                + localizedDescription
                + "\n\nJust "
                + localizedPriceString
                + "/month",
            myLabelStyle
        );

        int extraButtonsWidth = 200;
        if (
            GUI.Button(
                new Rect(50, windowHeight - 100, extraButtonsWidth, 50),
                "Terms",
                myButtonStyle
            )
        )
        {
            print("Terms");
            Application.OpenURL("https://www.app-central.com/terms");
        }

        if (
            GUI.Button(
                new Rect(
                    windowWidth - extraButtonsWidth - 50,
                    windowHeight - 100,
                    extraButtonsWidth,
                    50
                ),
                "Restore",
                myButtonStyle
            )
        )
        {
            print("Restore");
            AppCentralInAppPurchaser.Instance.RestorePurchases();
            windowOpen = false;
        }

        color.a = 1.0f;
        GUI.backgroundColor = color;
        GUIStyle purchaseButtonStyle = new GUIStyle(GUI.skin.button);
        purchaseButtonStyle.fontSize = 50;

        int buttonWidth = 400;
        if (
            GUI.Button(
                new Rect(windowWidth / 2 - buttonWidth / 2, 300, buttonWidth, 100),
                "Subscribe",
                purchaseButtonStyle
            )
        )
        {
            print("Purchasing!");
            AppCentralInAppPurchaser.Instance.BuySubscription_Editor(inAppId, subscriptionRsponce);
            windowOpen = false;
        }
    }

    void ErrorWindow(int windowID)
    {
        GUIStyle myButtonStyle = new GUIStyle(GUI.skin.button);
        myButtonStyle.fontSize = 35;

        Color color = Color.white;
        color.a = 0.0f;
        GUI.backgroundColor = color;
        if (GUI.Button(new Rect(10, 10, 40, 40), "X", myButtonStyle))
        {
            windowOpen = false;
        }

        GUIStyle myLabelStyle = new GUIStyle(GUI.skin.label);
        myLabelStyle.fontSize = 35;
        myLabelStyle.alignment = TextAnchor.UpperCenter;
        GUI.Label(
            new Rect(0, 100, windowWidth, 300),
            "Something went wrong :(\nMake sure your device can make purchases and try again.",
            myLabelStyle
        );

        color.a = 1.0f;
        GUI.backgroundColor = color;
        GUIStyle purchaseButtonStyle = new GUIStyle(GUI.skin.button);
        purchaseButtonStyle.fontSize = 50;

        int buttonWidth = 400;
        if (
            GUI.Button(
                new Rect(windowWidth / 2 - buttonWidth / 2, 300, buttonWidth, 100),
                "OK",
                purchaseButtonStyle
            )
        )
        {
            windowOpen = false;
        }
    }

    private Texture2D MakeTex(int width, int height, Color col)
    {
        Color[] pix = new Color[width * height];
        for (int i = 0; i < pix.Length; ++i)
        {
            pix[i] = col;
        }
        Texture2D result = new Texture2D(width, height);
        result.SetPixels(pix);
        result.Apply();
        return result;
    }
}
