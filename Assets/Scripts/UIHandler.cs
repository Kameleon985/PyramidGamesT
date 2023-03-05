using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Zenject;

public class UIHandler : MonoBehaviour
{
    private TextMeshProUGUI title;
    private GameObject message;
    private Button yesButton;
    private Button noButton;
    private TextMeshProUGUI timer;
    private TextMeshProUGUI highScore;
    private GameObject menu;

    [Inject]
    public void Construct(TextMeshProUGUI title, GameObject message, Button yesButton, Button noButton, TextMeshProUGUI timer, TextMeshProUGUI highscore, GameObject menu)
    {
        this.title = title;
        this.message = message;
        this.yesButton = yesButton;
        this.noButton = noButton;
        this.timer = timer;
        this.highScore = highscore;
        this.menu = menu;
    }

    private void Awake()
    {
        EventHandler.OnChestOpen += ChestOpened;
        EventHandler.OnDoorOpen += DoorOpened;
        EventHandler.OnGameOver += ShowGameOver;
        ShowHighScore();
    }

    private void ShowHighScore()
    {
        string score = "00:00";
        if (PlayerPrefs.HasKey("highscore"))
        {
            score = GetScoreString(PlayerPrefs.GetFloat("highscore"));
        }

        highScore.text = string.Format("High Score: {0}", score);
    }

    private string GetScoreString(float score)
    {
        int minutes = Mathf.FloorToInt(score);
        float seconds = score - minutes;
        return string.Format("{0}:{1}", minutes, System.Math.Round(seconds, 2) * 100);
    }

    private void Update()
    {
        timer.text = string.Format("Time: {0}:{1}", PlayerData.Minutes, PlayerData.Seconds);
    }

    private void ShowMessage()
    {
        message.SetActive(true);
        yesButton.gameObject.SetActive(true);
    }

    private void HideMessage()
    {
        message.SetActive(false);
    }

    private void ChestOpened()
    {
        yesButton.GetComponentInChildren<TextMeshProUGUI>().text = "Yes";
        noButton.GetComponentInChildren<TextMeshProUGUI>().text = "No";
        if (!PlayerData.ChestOpened)
        {
            title.text = "Open?";
            ShowMessage();
        }
        else if (!PlayerData.HasKey)
        {
            title.text = "Take?";
            ShowMessage();
        }
        else
        {
            HideMessage();
        }


    }

    private void DoorOpened()
    {
        title.text = "You need a key!";
        noButton.GetComponentInChildren<TextMeshProUGUI>().text = "Ok";
        ShowMessage();
        yesButton.gameObject.SetActive(false);
    }

    private void ShowGameOver(float score)
    {
        menu.GetComponentInChildren<Button>().GetComponentInChildren<TextMeshProUGUI>().text = "Try Again!";
        ShowHighScore();
        string currentScore = GetScoreString(score);
        highScore.text = string.Format("Current Score: {0} \n" +
            "{1}", currentScore, highScore.text);

        menu.SetActive(true);
    }

}
