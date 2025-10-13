using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public float spawnInterval = 2f;
    public string enemyPoolTag = "Enemy";

    [Header("Spawn Points")]
    public int levelSpawnPoint = 3; //Upgrade tambah ketika semakin lama bermain
    public float offsetX;
    public List<Transform> spawnPoints;

    private float timer;

    private void Update()
    {
        if (!GameManager.Instance.isPlaying) return;

        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    private void SpawnEnemy()
    {
        int getRandomPoint = Random.Range(0, levelSpawnPoint);

        getRandomPoint++;
        if (getRandomPoint >= spawnPoints.Count) getRandomPoint = 0;

        Transform randomPoint = spawnPoints[getRandomPoint];
        float randomOffsetX = Random.Range(-offsetX, offsetX);
        GameObject enemy = ObjectPoolManager.Instance.SpawnFromPool(enemyPoolTag, randomPoint.position + new Vector3(randomOffsetX,0,0), Quaternion.identity);

        enemy.transform.forward = Vector3.back;

        enemy.GetComponent<EnemyController>().SetUI();
    }
}
