using Dan.Main;
using Dan.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelLeaderboard : MonoBehaviour
{
    public static PanelLeaderboard Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(gameObject);
    }

    [Header("Gameplay")]
    public int countDataShowed;

    [Header("Leaderboard Essentials:")]
    public Transform leaderboardParent;
    public ItemLeaderboard itemLeaderboardPrefab;
    public GameObject leaderboardLoadingPanel;
    public GameObject panelNoData;

    private void OnEnable()
    {
        OpenLeaderboard();
    }

    public void OpenLeaderboard()
    {
        panelNoData.SetActive(false);
        leaderboardLoadingPanel.SetActive(true);

        foreach (Transform t in leaderboardParent)
            Destroy(t.gameObject);

        Load();
    }
    private void OnLeaderboardLoaded(Entry[] entries)
    {
        if (entries.Length == 0) panelNoData.SetActive(true);
        else foreach (var t in entries)
                CreateEntryDisplay(t);

        ToggleLoadingPanel(false);
    }
    private void ToggleLoadingPanel(bool isOn)
    {
        leaderboardLoadingPanel.SetActive(isOn);
    }
    private void CreateEntryDisplay(Entry entry)
    {
        var entryDisplay = Instantiate(itemLeaderboardPrefab.gameObject, leaderboardParent);

        entryDisplay.GetComponent<ItemLeaderboard>().Set(entry.RankSuffix(), entry.Username, entry.Score, entry.IsMine());
    }
    public void Load()
    {
        var searchQuery = new LeaderboardSearchQuery
        {
            Take = countDataShowed, // Atur jumlah data di show
        };

        Leaderboards.DemoSceneLeaderboard.GetEntries(searchQuery, OnLeaderboardLoaded, ErrorCallback);
        ToggleLoadingPanel(true);
    }
    private void ErrorCallback(string error)
    {
        Debug.LogError(error);
    }
}
