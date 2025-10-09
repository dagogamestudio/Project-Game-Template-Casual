using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    [Header("Tile Settings")]
    [Tooltip("Prefab tile jalan (pastikan panjangnya sama, misal 10 unit)")]
    public GameObject[] tilePrefabs;

    [Tooltip("Panjang 1 tile (Z-axis)")]
    public float tileLength = 10f;

    [Tooltip("Jumlah tile aktif di scene")]
    public int numberOfActiveTiles = 5;

    [Tooltip("Transform player (biasanya karakter utama)")]
    public Transform player;

    private List<GameObject> activeTiles = new List<GameObject>();
    private float spawnZ = 0f;
    private int lastPrefabIndex = 0;

    void Start()
    {
        for (int i = 0; i < numberOfActiveTiles; i++)
        {
            if (i < 2)
                SpawnTile(0); // spawn tile awal
            else
                SpawnTile();
        }
    }

    void Update()
    {
        if (player == null) return;

        // Jika player sudah melewati tile tertentu, spawn tile baru
        if (player.position.z - 20f > (spawnZ - numberOfActiveTiles * tileLength))
        {
            SpawnTile();
            DeleteOldTile();
        }
    }

    private void SpawnTile(int prefabIndex = -1)
    {
        GameObject tilePrefab = GetRandomTile(prefabIndex);
        GameObject go = Instantiate(tilePrefab, transform.forward * spawnZ, transform.rotation);
        activeTiles.Add(go);
        spawnZ += tileLength;
    }

    private void DeleteOldTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }

    private GameObject GetRandomTile(int index = -1)
    {
        if (index == -1)
            index = Random.Range(0, tilePrefabs.Length);

        // hindari tile sama berulang
        if (index == lastPrefabIndex)
            index = (index + 1) % tilePrefabs.Length;

        lastPrefabIndex = index;
        return tilePrefabs[index];
    }
}
