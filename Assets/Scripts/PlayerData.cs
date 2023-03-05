using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    private static bool hasKey = false;
    private static bool chestOpened = false;
    private static float highscore;
    private static int minutes = 0;
    private static int seconds = 0;
    private static float currentTime = 0f;
    private static bool running = false;

    public static bool HasKey { get => hasKey; set => hasKey = value; }
    public static bool ChestOpened { get => chestOpened; set => chestOpened = value; }
    public static int Minutes { get => minutes; set => minutes = value; }
    public static int Seconds { get => seconds; set => seconds = value; }

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("highscore"))
        {
            highscore = float.MaxValue;
        }
        else
        {
            highscore = PlayerPrefs.GetFloat("highscore");
        }
    }

    private static void Reset()
    {
        running = false;
        hasKey = false;
        chestOpened = false;
        currentTime = 0f;
        minutes = 0;
        seconds = 0;
    }

    public static void StartTimer()
    {
        Reset();
        running = true;
    }

    public static float StopTimer()
    {
        running = false;
        float tmpSeconds = seconds * 0.01f;
        float tmpTime = minutes * 1f + tmpSeconds;
        if (tmpTime < highscore)
        {
            highscore = tmpTime;
            PlayerPrefs.SetFloat("highscore", highscore);
            PlayerPrefs.Save();
        }
        return tmpTime;
    }

    private void Update()
    {
        if (running)
        {
            currentTime += Time.deltaTime;
            minutes = Mathf.FloorToInt(currentTime / 60);
            seconds = Mathf.FloorToInt(currentTime % 60);
        }
    }
}
