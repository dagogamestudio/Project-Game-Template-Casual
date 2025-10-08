using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    [Header("Shop")]
    public GameObject panelShop;
    public List<ShopCategory> listShopCategory;
    public UIItemShop prefabItemUI;
    public int indexShopCategory;

    [Header("Status")]
    public int indexCurrentItem; // Item yang sedang digunakan di setiap category
    public void Initialize()
    {
        panelShop.SetActive(true);
        SetUI();
        panelShop.SetActive(false);
    }

    public void SetUI()
    {
        //Set Category
        for (int i = 0; i < listShopCategory.Count; i++)
        {
            int indexCategory = i;
            ShopCategory shopCat = listShopCategory[i];

            var buttonCategory = shopCat.buttonCategory;

            buttonCategory.SetUI(shopCat.categoryName);
            buttonCategory.button.onClick.RemoveAllListeners();
            buttonCategory.button.onClick.AddListener(() => ChangeCategory(indexCategory));


            //Set Item
            for (int j = 0; j < shopCat.listItemShop.Count; j++)
            {
                int indexItemShop = j;
                ItemShop itemShop = shopCat.listItemShop[j];

                UIItemShop itemShopUI = Instantiate(prefabItemUI, shopCat.parentItem);
                itemShopUI.SetUI(itemShop.itemName, itemShop.itemIcon, itemShop.itemPrice);

                itemShopUI.button.onClick.RemoveAllListeners();
                itemShopUI.button.onClick.AddListener(() => OnButtonItemClick(indexCategory, indexItemShop));

                shopCat.listItemShop[indexItemShop].UIItemShop = itemShopUI;
            }
        }

        ChangeCategory(0);
    }

    private void RefreshUI()
    {
        for (int i = 0; i < listShopCategory.Count; i++)
        {
            int indexCategory = i;
            ShopCategory shopCat = listShopCategory[i];

            for (int j = 0; j < shopCat.listItemShop.Count; j++)
            {
                int indexItemShop = j;
                ItemShop itemShop = shopCat.listItemShop[j];

                bool isUsed = shopCat.currentSelectedId == j;

                UIItemShop itemShopUI = shopCat.listItemShop[indexItemShop].UIItemShop;
                itemShopUI.SetUnlocked(itemShop.isUnlocked);
                itemShopUI.SetSelected(isUsed);
                //itemShopUI.SetSelected(itemShop.isUsed);

                if (isUsed)
                    shopCat.OnItemSelected.Invoke(indexItemShop);
            }
        }
    }

    public void ChangeCategory(int index)
    {
        indexShopCategory = index;
        for (int i = 0;i < listShopCategory.Count; i++)
        {
            bool isSelected = i == index;
            listShopCategory[i].buttonCategory.SetButton(isSelected);
            listShopCategory[i].parentItem.gameObject.SetActive(isSelected);
        }

        RefreshUI();
    }

    public void OnButtonItemClick(int indexCategory, int indexItem)
    {
        Debug.Log($"Selected item {indexItem} in category {indexCategory}");

        ShopCategory itemShop = listShopCategory[indexCategory];

        //Jika sudah unlock coba pakai
        if (itemShop.listItemShop[indexItem].isUnlocked)
        {
            //Coba pakai, unUsed item lainnya
            TryUsedItem(indexItem);
        }
        else
        {
            //Beli itemnya

            //Check jika uang cukup
            int playerMoney = GameManager.Instance.PlayerMoney;
            int itemPrice = itemShop.listItemShop[indexItem].itemPrice;

            if (playerMoney >= itemPrice)
            {
                GameManager.Instance.PlayerMoney -= itemPrice;
                UnlockItem(indexItem);

                CoreManager.Instance.ShowNotif($"Buy Success, {itemShop.listItemShop[indexItem].itemName} Unlocked");
            }
            else
            {
                CoreManager.Instance.ShowNotif("You don't have enough money");
            }
        }


        RefreshUI();
    }

    private void TryUsedItem(int indexItem)
    {
        var category = listShopCategory[indexShopCategory];

        for (int i = 0; i < category.listItemShop.Count; i++)
        {
            if(i == indexItem)
            {
                category.currentSelectedId = indexItem;
                category.OnItemSelected?.Invoke(indexItem);
            }
        }
    }

    public void UnlockItem(int indexItem)
    {
        string keyItem = $"{listShopCategory[indexShopCategory].categoryId}_{indexItem}";
        Debug.Log("Unlock " + keyItem);

        PlayerPrefs.SetInt(keyItem, 1);

        string category = listShopCategory[indexShopCategory].categoryId;

        // Update ke SaveManager
        ItemDataSave data = new()
        {
            categoryId = category,
            indexItem = indexItem,
            isUnlocked = true,
            isUsed = false,
            
        };

        SaveManager.Instance.dataNewGame.listItemDataSave.Add(data);
        SaveManager.Instance.SaveData();

        // Update state runtime juga
        listShopCategory[indexShopCategory].listItemShop[indexItem].isUnlocked = true;

        Debug.Log($"Unlocked: {category}_{indexItem}");
    }
}

[Serializable]
public class ShopCategory
{
    public string categoryId;
    public string categoryName;

    public int currentSelectedId;

    public UIItemCategory buttonCategory;
    public Transform parentItem;
    public List<ItemShop> listItemShop;

    public UnityEvent<int> OnItemSelected;
}

[Serializable]
public class ItemShop
{
    public string itemName;
    public Sprite itemIcon;
    public int itemPrice;
    public bool isUnlocked;

    public UIItemShop UIItemShop;
}