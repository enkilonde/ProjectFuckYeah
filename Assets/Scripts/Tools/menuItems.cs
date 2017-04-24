using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class menuItems
{
    public static int playerNumber = 1;

    [MenuItem("Tools/SetNumberOfPlayers/1")]    
    public static void SetPLayer_1()
    {
        SetPlayerCount(1);
    }

    [MenuItem("Tools/SetNumberOfPlayers/2")]
    public static void SetPLayer_2()
    {
        SetPlayerCount(2);
    }

    [MenuItem("Tools/SetNumberOfPlayers/3")]
    public static void SetPLayer_3()
    {
        SetPlayerCount(3);
    }

    [MenuItem("Tools/SetNumberOfPlayers/4")]
    public static void SetPLayer_4()
    {
        SetPlayerCount(4);
    }

    private static void SetPlayerCount(int value)
    {
        PlayerPrefs.SetInt("NumberOfPlayers", value);
        Debug.Log(PlayerPrefs.GetInt("NumberOfPlayers"));

    }

}
