using Dan.Main;
using Dan.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardManager : MonoBehaviour
{
    public static LeaderboardManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(gameObject);
    }

    public void SendScore(int highScore)
    {
        string playerName = PlayerPrefs.GetString("PlayerName");
        Debug.Log("Submit Score " + highScore + " Name " + playerName);
        Leaderboards.DemoSceneLeaderboard.UploadNewEntry(playerName, highScore, Callback, ErrorCallback);
    }

    public void ResetPlayer()
    {
        LeaderboardCreator.ResetPlayer();
    }

    private void OnPersonalEntryLoaded(Entry entry)
    {
        Debug.Log($"Player: {entry.Username} : {entry.Score}");
    }

    private void Callback(bool success)
    {
        if (success) Debug.Log("Success upload data");
        //Load();
    }

    private void ErrorCallback(string error)
    {
        Debug.LogError(error);
    }
}
