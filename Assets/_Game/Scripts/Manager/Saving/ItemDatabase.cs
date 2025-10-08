using UnityEngine;
using System.Collections.Generic;

public class ItemDatabase : MonoBehaviour
{
    //Khusus game simulator 
    public static ItemDatabase Instance;

/*    public List<ItemBaseSO> allItems;

    private Dictionary<string, ItemBaseSO> lookup;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            lookup = new Dictionary<string, ItemBaseSO>();
            foreach (var item in allItems)
            {
                lookup[item.itemId] = item;
            }
        }
        else Destroy(gameObject);
    }

    public ItemBaseSO GetItemByName(string id)
    {
        lookup.TryGetValue(id, out var item);
        return item;
    }*/
}
