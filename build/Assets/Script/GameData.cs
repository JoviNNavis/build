using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static int GetCurrentScene()
    {
        return PlayerPrefs.GetInt("CurrentScene",1);
    }
    public static void SetCurrentScene(int val)
    {
        PlayerPrefs.SetInt("CurrentScene", val);
    }
    public static int GetCoins()
    {
        return PlayerPrefs.GetInt("Coins");
    }
    public static void SetCoins(int val)
    {
        PlayerPrefs.SetInt("Coins", val);
    }
}
