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
    public int playerLevel;

    [Header("Reference")]
    public CanvasManager canvasManager;

    [Header("Gameplay")]
    public bool isPlaying;

    public void Initialize()
    {
        //Do something
    }

    public void StartGame()
    {
        //Mulai game
        isPlaying = true;

        canvasManager.panelMenu.SetActive(false);
        canvasManager.panelGameplay.SetActive(true);
    }

    public void GameFinish()
    {
        isPlaying = false;

        //Tampilin panel Finish
        canvasManager.panelGameplay.SetActive(false);
        canvasManager.panelFinish.SetActive(true);
    }
}


public static class DataString
{
    public static string PlayerName = "PlayerName";
}