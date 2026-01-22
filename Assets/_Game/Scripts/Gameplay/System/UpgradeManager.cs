using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public List<UpgradeOptionButton> upgradeOptionButtons;
    public List<UpgradeData> upgradeDatas;

    void Start()
    {
        SetUpgradeOption();
    }

    public void SetUpgradeOption()
    {
        if (upgradeDatas.Count < upgradeOptionButtons.Count)
        {
            Debug.LogError("UpgradeData kurang dari jumlah tombol!");
            return;
        }

        // Copy list agar data asli tidak berubah
        List<UpgradeData> shuffledUpgrades = new List<UpgradeData>(upgradeDatas);

        // Shuffle (Fisher–Yates)
        for (int i = shuffledUpgrades.Count - 1; i > 0; i--)
        {
            int rand = Random.Range(0, i + 1);
            (shuffledUpgrades[i], shuffledUpgrades[rand]) =
                (shuffledUpgrades[rand], shuffledUpgrades[i]);
        }

        // Assign ke button
        for (int i = 0; i < upgradeOptionButtons.Count; i++)
        {
            upgradeOptionButtons[i].SetUpgradeOption(shuffledUpgrades[i]);
        }
    }
}
