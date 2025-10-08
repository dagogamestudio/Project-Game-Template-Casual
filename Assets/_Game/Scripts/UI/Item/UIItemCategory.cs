using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIItemCategory : MonoBehaviour
{
    [HideInInspector] public Button button;
    public TextMeshProUGUI textCategory;

    public Color colorSelected;
    public Color colorUnselected;
    private void Awake()
    {
        button = GetComponent<Button>();
    }

    public void SetUI(string categoryName)
    {
        textCategory.text = categoryName;
    }
    public void SetButton(bool isSelected)
    {
        button.image.color = isSelected ? colorSelected : colorUnselected;
    }
}
