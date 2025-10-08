using TMPro;
using UnityEngine.Events;

public class PanelConfirmation : PanelAnimation
{
    public UnityAction onConfirmation;

    public TextMeshProUGUI textTitle;
    public TextMeshProUGUI textDescription;

    public void SetInformation(string title, string desc)
    {
        OpenPanel();

        textTitle.text = title;
        textDescription.text = desc;
    }

    public void SetAction(UnityAction action) => onConfirmation = action;

    public void ButtonConfirm()
    {
        onConfirmation?.Invoke();
        onConfirmation = null;

        ClosePanel();
    }
}
