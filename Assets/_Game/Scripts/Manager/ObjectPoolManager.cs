using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string poolTag;
        public GameObject prefab;
        public int size = 10;
        public Transform parent; // ✅ Parent baru
    }

    public static ObjectPoolManager Instance;

    [Header("Pool List")]
    public List<Pool> pools;

    private Dictionary<string, Queue<GameObject>> poolDictionary;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        InitializePools();
    }

    private void InitializePools()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectQueue = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab, pool.parent);
                obj.SetActive(false);
                objectQueue.Enqueue(obj);
            }

            poolDictionary.Add(pool.poolTag, objectQueue);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning($"Pool with tag {tag} doesn't exist!");
            return null;
        }

        GameObject objToSpawn = poolDictionary[tag].Dequeue();
        objToSpawn.SetActive(true);
        objToSpawn.transform.SetPositionAndRotation(position, rotation);

        poolDictionary[tag].Enqueue(objToSpawn);

        return objToSpawn;
    }
}
