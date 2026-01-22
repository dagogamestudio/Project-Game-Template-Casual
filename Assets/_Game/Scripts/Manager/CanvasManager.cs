using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    [Header("Panel")]
    public GameObject panelMenu;
    public GameObject panelGameplay;
    public GameObject panelFinish;
    public PanelWaveCompleted panelWaveCompleted;
    public GameObject panelUpgradeOption;

    [Header("Main UI")]
    public TextMeshProUGUI textCoin;
    public TextMeshProUGUI textScore;
    public TextMeshProUGUI textBestScore;


    [Header("Gameplay UI")]
    public TextMeshProUGUI textHealth;
    public TextMeshProUGUI textExp;
    public TextMeshProUGUI textWave;
    public TextMeshProUGUI textWavePercentage;

    public Slider sliderHealth;
    public Slider sliderExp;
    public Slider sliderWave;

    public void SetWave(int currentWave, int enemySpawned, int targetEnemySpawn)
    {
        textWave.text = $"WAVE {currentWave}";

        sliderWave.maxValue = targetEnemySpawn;
        sliderWave.value = enemySpawned;

        float percent = (float)enemySpawned / targetEnemySpawn * 100f;
        textWavePercentage.text = $"{Mathf.RoundToInt(percent)}%";

        //Jika sudah 100%, panggil fungsi next wave dan pengecekan untuk boss wave
    }

    public void SetTextCoin(int price)
    {
        textCoin.text = Helper.TurnToIDRValue(price);
    }

    public void SetTextScore(int playerScore, int playerBestScore)
    {
        textScore.text = $"SCORE: {playerScore}";
        textBestScore.text = $"BEST SCORE: {playerBestScore}";
    }

    public void SetTextHealth(int currentHealth, int maxHealth)
    {
        sliderHealth.maxValue = maxHealth;
        sliderHealth.value = currentHealth;
        textHealth.text = $"Health : {currentHealth}/{maxHealth} HP";
    }

    public void SetTextExp(int currentLevel, int currentExp, int targetExp)
    {
        sliderExp.maxValue = targetExp;
        sliderExp.value = currentExp;
        textExp.text = $"Level {currentLevel}: {currentExp}/{targetExp} Exp";
    }
}

[Serializable]
public class PanelGame
{
    public string panelName;
    public PanelAnimation panelObject;
}