using TMPro;
using UnityEngine;

public class SetTitlePanel : MonoBehaviour
{
    public TextMeshProUGUI textTitle;
    public AutoSizePrefferedText autoSize;
    private void Start()=> autoSize.ChangeText(textTitle.text);
}
