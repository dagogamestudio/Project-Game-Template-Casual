using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIItemShop : MonoBehaviour
{
    public GameObject panelLocked;
    public GameObject panelUnlocked;

    public GameObject panelUnselected;
    public GameObject panelSelected;

    public TextMeshProUGUI textName;
    public Image imageIcon;
    public TextMeshProUGUI textPrice;

    [HideInInspector]
    public Button button;
    private void Awake()
    {
        button = GetComponent<Button>();
    }

    public void SetUI(string name, Sprite icon, int price)
    {
        textName.text = name + Random.Range(1,100);
        imageIcon.sprite = icon;
        textPrice.text = price.ToString();
    }

    public void SetUnlocked(bool isUnlocked)
    {
        panelUnlocked.SetActive(isUnlocked);
        panelLocked.SetActive(!isUnlocked);
    }

    public void SetSelected(bool isSelected)
    {
        panelSelected.SetActive(isSelected);
        panelUnselected.SetActive(!isSelected);
    }
}
