using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance;

    public int currentWave = 1;

    [Header("Enemy Scaling")]
    public float hpIncreasePerWave = 0.15f;
    public float damageIncreasePerWave = 0.10f;
    public float speedIncreasePerWave = 0.03f;
    public float maxSpeedMultiplier = 2.2f;

    [Header("Enemy Count")]
    public int baseEnemyCount = 5;
    public int enemyIncreasePerWave = 2;
    public int targetTotalEnemy;
    public int totalEnemyDie;

    [Header("Spawn Interval")]
    public float baseSpawnInterval = 2f;
    public float spawnIntervalDecrease = 0.05f;
    public float minSpawnInterval = 0.5f;
    public float delayNextWave;

    [Header("References")]
    public Spawner spawner;

    private void Awake()
    {
        Instance = this;
    }

    public void StartWave()
    {
        targetTotalEnemy = GetEnemyCount();
        spawner.StartSpawning(
            targetTotalEnemy,
            GetSpawnInterval()
        );
    }

    public void EnemyDie()
    {
        totalEnemyDie++;
        if(totalEnemyDie >= targetTotalEnemy)
        {
            //All enemy die
            NextWave();
        }
    }


    public void NextWave()
    {
        currentWave++;
        totalEnemyDie = 0;
        GameManager.Instance.canvasManager.panelWaveCompleted.SetText(currentWave);
        Invoke(nameof(StartWave), delayNextWave);
    }

    public int GetEnemyCount()
    {
        return baseEnemyCount + currentWave * enemyIncreasePerWave;
    }

    public float GetSpawnInterval()
    {
        return Mathf.Max(
            minSpawnInterval,
            baseSpawnInterval - currentWave * spawnIntervalDecrease
        );
    }

    // ===== STAT MULTIPLIER =====

    public float GetHpMultiplier()
    {
        return 1f + (currentWave - 1) * hpIncreasePerWave;
    }

    public float GetDamageMultiplier()
    {
        return 1f + (currentWave - 1) * damageIncreasePerWave;
    }

    public float GetSpeedMultiplier()
    {
        return Mathf.Min(
            1f + (currentWave - 1) * speedIncreasePerWave,
            maxSpeedMultiplier
        );
    }
}
