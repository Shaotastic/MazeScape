﻿using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{

    public static int keys = 2;

    public static bool exitEnable = false;

    public static int enemiesKilled;

    public static float time;

    public static int playerDeaths = 0;

    static int currentKeys = 0;

    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentKeys == keys)
            exitEnable = true;

        time += Time.time;
    }

    public static string GetTime()
    {
        int minutes = Mathf.FloorToInt(Time.time / 60);
        int seconds = Mathf.FloorToInt(Time.time % 60);
        return minutes + ":" + seconds;
    }   
    
    public static void AddKey()
    {
        if (currentKeys != keys)
            currentKeys++;
    }

    public static int GetKeyAmount()
    {
        return currentKeys;
    }
}
