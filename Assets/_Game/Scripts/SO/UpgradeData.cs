using UnityEngine;

[CreateAssetMenu(menuName = "Upgrade/Upgrade Data")]
public class UpgradeData : ScriptableObject
{
    public UpgradeType type;
    public float value;
    public Sprite icon;
    public string title;
    public string description;
}
