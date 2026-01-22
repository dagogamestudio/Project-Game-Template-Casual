using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    [Header("Pool Tags")]
    public string enemyTagPrefix = "Enemy";
    ///public string crateTag = "Crate";

    [Header("Spawn Points")]
    public Transform[] spawnPoints;

    [Header("Crate Chance")]
    public float baseCrateChance = 5f;
    public float crateChancePerWave = 0.5f;
    public float maxCrateChance = 20f;

    private float spawnInterval;
    private int totalEnemyToSpawn;
    private int enemySpawned;

    public void StartSpawning(int enemyCount, float interval)
    {
        spawnInterval = interval;
        totalEnemyToSpawn = enemyCount;
        enemySpawned = 0;
        
        GameManager.Instance.canvasManager.SetWave(
            WaveManager.Instance.currentWave,
            enemySpawned,
            totalEnemyToSpawn
        );

        StartCoroutine(SpawnEnemies(enemyCount));
    }

    IEnumerator SpawnEnemies(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Transform point = GetSpawnPosition();

            string enemyTag = GetEnemyTagByWave();
            GameObject enemy = ObjectPoolManager.Instance.SpawnFromPool(
                enemyTag,
                point.position,
                point.rotation
            );

            SetupEnemyStats(enemy);
            //TrySpawnCrate();

            enemySpawned++;

            GameManager.Instance.canvasManager.SetWave(
                WaveManager.Instance.currentWave,
                enemySpawned,
                totalEnemyToSpawn
            );

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    // ================= UTIL =================

    private void SetupEnemyStats(GameObject enemy)
    {
        EnemyController controller = enemy.GetComponent<EnemyController>();

        controller.ApplyWaveScaling(WaveManager.Instance.currentWave);
        controller.SetUI();
    }


    private void TrySpawnCrate()
    {
/*        float chance = Mathf.Min(
            baseCrateChance + WaveManager.Instance.currentWave * crateChancePerWave,
            maxCrateChance
        );

        if (Random.Range(0f, 100f) <= chance)
        {
            Transform point = GetSpawnPosition();
            ObjectPoolManager.Instance.SpawnFromPool(
                crateTag,
                point.position,
                point.rotation
            );
        }*/
    }

    private string GetEnemyTagByWave()
    {
        int wave = WaveManager.Instance.currentWave;

        if (wave <= 10)
            return enemyTagPrefix + "1"; // Pocong
        else if (wave <= 25)
            return enemyTagPrefix + Random.Range(1, 3); // Pocong + Tuyul
        else
            return enemyTagPrefix + Random.Range(1, 3); // Tuyul + Boss
    }

    private Transform GetSpawnPosition()
    {
        int wave = WaveManager.Instance.currentWave;

        int availablePoints =
            wave <= 10 ? 3 :
            wave <= 25 ? 4 :
            spawnPoints.Length;

        return spawnPoints[Random.Range(0, availablePoints)];
    }
}
