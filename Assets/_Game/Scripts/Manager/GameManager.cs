using System;
using UnityEngine;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    [Header("Data")]
    private int playerCoin;
    public int PlayerCoin
    {
        get { return playerCoin; }
        set 
        { 
            playerCoin = value;
            canvasManager.SetTextCoin(playerCoin);
        }
    }

    public int playerScore;
    public int playerBestScore;

    [Header("Reference")]
    public CanvasManager canvasManager;
    public PlayerController playerController;
    public WaveManager waveManager;
    public UpgradeManager upgradeManager;

    [Header("Gameplay")]
    public bool isPlaying;

    public void Initialize()
    {
        canvasManager.SetTextScore(0,playerBestScore);
    }

    public void StartGame()
    {
        isPlaying = true;
        playerScore = 0;
        PlayerStatus.Instance.ResetData();

        waveManager.StartWave();
        canvasManager.panelMenu.SetActive(false);
        canvasManager.panelGameplay.SetActive(true);
        CameraManager.instance.ChangeCamera(true);
        SoundManager.Instance.PlayBGM("Game");
    }

    public void AddScore(int score)
    {
        playerScore += score;
        if (playerScore > playerBestScore)
            playerBestScore = playerScore;

        canvasManager.SetTextScore(playerScore, playerBestScore);
    }

    public void LevelUp()
    {
        isPlaying = false;
        canvasManager.panelUpgradeOption.SetActive(true);
        upgradeManager.SetUpgradeOption();

        SoundManager.Instance.PlaySFX("LevelUp");
    }
    public void UpgradeDone()
    {
        isPlaying = true;
        canvasManager.panelUpgradeOption.SetActive(false);


        SoundManager.Instance.PlaySFX("UpgradeOption");
    }
    public void GameFinish()
    {
        if (!isPlaying) return;

        isPlaying = false;

        SaveManager.Instance.SaveData();

        //Tampilin panel Finish
        canvasManager.panelGameplay.SetActive(false);
        canvasManager.panelFinish.SetActive(true);


        SaveManager.Instance.SaveData();
        LeaderboardManager.Instance.SendScore(playerBestScore);
    }

}


public static class DataString
{
    public static string PlayerName = "PlayerName";
}