using UnityEngine;
using UnityEngine.UI;

public class ButtonConfirmationTemplate : MonoBehaviour
{
    public string title;
    [TextArea(2,2)]
    public string desc;

    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
    }

    public virtual void OnButtonClick() => CoreManager.Instance.panelConfirmation.SetInformation(title, desc);
}
