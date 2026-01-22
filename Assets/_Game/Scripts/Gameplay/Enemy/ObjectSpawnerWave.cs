using System.Collections;
using UnityEngine;

public class ObjectSpawnerWave : MonoBehaviour
{
    /*[Header("References")]
    public Transform[] spawnPoints;

    [Header("Pool Tags")]
    public string enemyTag = "Enemy";
    public string crateTag = "Crate";
    public string modifierTag = "Modifier";

    private int currentWave = 0;
    private bool spawning = false;

    void Start()
    {
        StartCoroutine(StartWave());
    }

    IEnumerator StartWave()
    {
        spawning = true;

        int enemyLeft = wave.totalEnemy;
        int crateLeft = wave.totalCrate;
        int modifierLeft = wave.totalModifier;

        int totalLeft = enemyLeft + crateLeft + modifierLeft;

        while (totalLeft > 0)
        {
            int roll = Random.Range(0, 100);

            // Tentukan spawn berdasarkan kesempatan
            if (roll < wave.enemyChance && enemyLeft > 0)
            {
                Spawn(enemyTag);
                enemyLeft--;
            }
            else if (roll < wave.enemyChance + wave.crateChance && crateLeft > 0)
            {
                Spawn(crateTag);
                crateLeft--;
            }
            else if (modifierLeft > 0)
            {
                Spawn(modifierTag);
                modifierLeft--;
            }

            totalLeft = enemyLeft + crateLeft + modifierLeft;

            yield return new WaitForSeconds(wave.spawnInterval);
        }

        spawning = false;

        // Tunggu sampai semua musuh mati sebelum lanjut wave
        yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("Enemy").Length == 0);

        NextWave();
    }

    void NextWave()
    {
        currentWave++;
        if (currentWave >= waveDB.waves.Length)
        {
            Debug.Log("SEMUA WAVE SELESAI!");
            return;
        }

        StartCoroutine(StartWave());
    }

    void Spawn(string poolTag)
    {
        int lane = Random.Range(0, spawnPoints.Length);

        GameObject obj = ObjectPoolManager.Instance.SpawnFromPool(
            poolTag,
            spawnPoints[lane].position,
            Quaternion.identity
        );

        obj.transform.forward = Vector3.back;

        // Setup khusus enemy
        if (poolTag == enemyTag)
        {
            obj.GetComponent<EnemyController>().SetUI();
        }
    }*/
}
