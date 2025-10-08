using TMPro;
using UnityEngine;

public class AutoSizePrefferedText : MonoBehaviour
{
    public TMP_Text PrefferedText;
    [SerializeField] private RectTransform autoSizedRectTf;

    [Space(10)]
    [SerializeField] private AutoSizedRect chooseAutoSized;
    [SerializeField] private float widthOffset;
    [SerializeField] private float heightOffset;

    public enum AutoSizedRect
    {
        width,
        height,
        both,
    }

    public void ChangeText(string newText)
    {
        Vector2 newSize = autoSizedRectTf.sizeDelta;
        PrefferedText.text = newText;

        switch (chooseAutoSized)
        {
            case AutoSizedRect.width:
                newSize.x = PrefferedText.preferredWidth + widthOffset;
                break;
            case AutoSizedRect.height:
                newSize.y = PrefferedText.preferredHeight + heightOffset;
                break;
            case AutoSizedRect.both:
                newSize.x = PrefferedText.preferredWidth + widthOffset;
                newSize.y = PrefferedText.preferredHeight + heightOffset;
                break;
        }

        autoSizedRectTf.sizeDelta = newSize;
    }
}