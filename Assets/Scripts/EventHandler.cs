using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
    public static event Action OnChestOpen;
    public static event Action OnDoorOpen;
    public static event Action<float> OnGameOver;
    public static event Action OnDecline;

    public void Decline()
    {
        OnDecline?.Invoke();
    }

    public void ChestPopUp()
    {
        OnChestOpen?.Invoke();
    }

    public void OpenChest()
    {
        if (!PlayerData.ChestOpened)
        {
            PlayerData.ChestOpened = true;
        }
        else if (!PlayerData.HasKey)
        {
            PlayerData.HasKey = true;
        }
        ChestPopUp();
    }

    public void DoorOpen()
    {
        if (PlayerData.HasKey)
        {
            float score = PlayerData.StopTimer();
            OnGameOver?.Invoke(score);
        }
        else
        {
            OnDoorOpen?.Invoke();
        }
    }

}
