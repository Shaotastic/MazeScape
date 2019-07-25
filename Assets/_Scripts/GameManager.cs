using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{

    public static int keys = 1;

    public static bool exitEnable = false;

    public static int enemiesKilled;

    public static float time;

    public static int playerDeaths = 0;
    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (keys == 0)
            exitEnable = true;

        time += Time.time;
    }

    public static string GetTime()
    {
        int minutes = Mathf.FloorToInt(Time.time / 60);
        int seconds = Mathf.FloorToInt(Time.time % 60);
        return minutes + ":" + seconds;
    }
}
