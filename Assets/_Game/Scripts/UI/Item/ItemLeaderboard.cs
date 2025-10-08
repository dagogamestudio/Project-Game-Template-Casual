using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemLeaderboard : MonoBehaviour
{
    public TextMeshProUGUI textRank;
    public TextMeshProUGUI textName;
    public TextMeshProUGUI textScore;

    public Color colorPlayer;

    public void Set(string rank, string name, int score, bool isPlayer)
    {
        GetComponent<Image>().color = isPlayer ? colorPlayer : Color.white;
        textName.text = isPlayer ? PlayerPrefs.GetString("PlayerName") : name.ToString();

        int rankInt = int.Parse(rank.Substring(0, rank.Length - 2));
        string suffix;

        switch (rankInt)
        {
            case 1:
                suffix = "st";
                break;
            case 2:
                suffix = "nd";
                break;
            case 3:
                suffix = "rd";
                break;
            default:
                suffix = "th";
                break;
        }

        textRank.text = $"{rankInt}{suffix}";
        textScore.text = score.ToString();
    }
}