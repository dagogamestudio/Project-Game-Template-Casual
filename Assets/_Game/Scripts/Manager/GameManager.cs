using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    [Header("Data")]
    private int playerMoney;
    public int PlayerMoney
    {
        get { return playerMoney; }
        set 
        { 
            playerMoney = value;
            canvasManager.SetTextMoney(playerMoney);
        }
    }

    public int playerScore;
    public int playerBestScore;

    [Header("Reference")]
    public CanvasManager canvasManager;
    public PlayerController playerController;
    public EnemySpawner enemySpawner;

    [Header("Gameplay")]
    public bool isPlaying;

    public void Initialize()
    {
        canvasManager.SetTextScore(0,playerBestScore);
    }

    public void StartGame()
    {
        //Mulai game
        isPlaying = true;
        playerScore = 0;

        canvasManager.panelMenu.SetActive(false);
        canvasManager.panelGameplay.SetActive(true);
    }

    public void AddScore(int score)
    {
        playerScore += score;
        if (playerScore > playerBestScore)
            playerBestScore = playerScore;

        canvasManager.SetTextScore(playerScore, playerBestScore);
    }
    public void GameFinish()
    {
        isPlaying = false;

        SaveManager.Instance.SaveData();

        //Tampilin panel Finish
        canvasManager.panelGameplay.SetActive(false);
        canvasManager.panelFinish.SetActive(true);
    }
}


public static class DataString
{
    public static string PlayerName = "PlayerName";
}