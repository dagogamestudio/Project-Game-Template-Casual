using System;
using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;


    public DataGame dataNewGame;
    private string savePath;

    //C:\Users\DagoEng Game Studio\AppData\LocalLow\DefaultCompany\Project Template Casual Game
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            savePath = Path.Combine(Application.persistentDataPath, "save.json");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Initialize()
    {
        TryLoadData();
    }

    public void SaveData()
    {
        Debug.Log("Save Data");
        if (GameManager.Instance == null) return;

        Debug.Log("Save Data");
        DataGame data = new()
        {
            playerBestScore = GameManager.Instance.playerBestScore,
            playerMoney = GameManager.Instance.PlayerMoney,
            listItemDataSave = GetListItemDataShop()
        };

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);
        Debug.Log("Game saved: " + savePath);
    }

    private List<ItemDataSave> GetListItemDataShop(bool? isNewGame = null)
    {
        List<ItemDataSave> listItemDataSave = new();

        List<ShopCategory> shop = ShopManager.Instance.listShopCategory;
        for (int i = 0; i < shop.Count; i++)
        {
            int indexCategory = i;
            for (int j = 0; j < shop[i].listItemShop.Count; j++)
            {
                string category = shop[i].categoryId;
                int indexItem = j;

                // Update ke SaveManager
                ItemDataSave data = new()
                {
                    categoryId = category,
                    indexItem = indexItem,
                    isUnlocked = shop[indexCategory].listItemShop[indexItem].isUnlocked,
                    //isUsed = shop[indexCategory].listItemShop[indexItem].isUsed,
                };

                if (isNewGame.HasValue)
                {
                    data.isUnlocked = j == 0;
                    data.isUsed = j == 0;
                }

                listItemDataSave.Add(data);
            }
        }

        return listItemDataSave;
    }


    private void TryLoadData()
    {
        Debug.Log("Game Load: " + savePath);
        if (File.Exists(savePath))
        {
            LoadData();
        }
        else
        {
            Debug.LogWarning("No save file found. Starting new game.");
            SetDataNewGame();
            LoadData();
        }
    }

    private void LoadData()
    {
        string json = File.ReadAllText(savePath);
        DataGame data = JsonUtility.FromJson<DataGame>(json);

        //Apply Data ke gameplay
        GameManager.Instance.PlayerMoney = data.playerMoney;
        GameManager.Instance.playerBestScore = data.playerBestScore;

        /*        List<ShopCategory> listShopData = ShopManager.Instance.listShopCategory;
                // Apply ke shop
                foreach (var savedItem in data.listItemDataSave)
                {
                    for (int i = 0; i < listShopData.Count; i++)
                    {
                        if (listShopData[i].categoryId == savedItem.categoryId)
                        {
                            for (int j = 0; j < listShopData[i].listItemShop.Count; j++)
                            {
                                if(j == savedItem.indexItem)
                                {
                                    listShopData[i].listItemShop[j].isUnlocked = savedItem.isUnlocked;
                                    listShopData[i].listItemShop[j].isUsed = savedItem.isUsed;
                                }
                            }
                        }
                    }

                    ShopManager.Instance.listShopCategory
                        .Find(c => c.categoryId == savedItem.categoryId)
                        .listItemShop[savedItem.indexItem].isUnlocked = savedItem.isUnlocked;
                }*/

        foreach (var savedItem in data.listItemDataSave)
        {
            var category = ShopManager.Instance.listShopCategory
            .Find(c => c.categoryId == savedItem.categoryId);

            if (category != null && savedItem.indexItem >= 0 && savedItem.indexItem < category.listItemShop.Count)
            {
                var item = category.listItemShop[savedItem.indexItem];
                item.isUnlocked = savedItem.isUnlocked;
            }
        }
    }

    private void SetDataNewGame()
    {
        dataNewGame.listItemDataSave.Clear();
        dataNewGame.listItemDataSave = GetListItemDataShop(false);

        string json = JsonUtility.ToJson(dataNewGame, true);
        File.WriteAllText(savePath, json);

        LoadData();
        ASyncSceneLoader.Instance.RestartScene();
    }

    public void ResetData()
    {
        SetDataNewGame();
        //ASyncSceneLoader.Instance.RestartScene();
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause) SaveData();
    }

    private void OnApplicationQuit() => SaveData();
}


[Serializable]
public class DataGame
{
    public int playerMoney;
    public int playerBestScore;
    // Simpan status unlock item
    public List<ItemDataSave> listItemDataSave = new();
}

[Serializable]
public class ItemDataSave
{
    public string categoryId;
    public int indexItem;
    public bool isUnlocked;
    public bool isUsed;
}