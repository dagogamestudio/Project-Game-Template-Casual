using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeOptionButton : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI textTitle;
    public TextMeshProUGUI textDesc;
    public Image iconUpgrade;

    public UpgradeData upgradeData;

    public void SetUpgradeOption(UpgradeData upgradeData)
    {
        this.upgradeData = upgradeData;
        textTitle.text = upgradeData.title;
        textDesc.text = upgradeData.description;
        iconUpgrade.sprite = upgradeData.icon;
    }

    public void OnClick()
    {
        PlayerStatus.Instance.ApplyUpgrade(upgradeData.type, upgradeData.value);
        GameManager.Instance.UpgradeDone();
    }
}