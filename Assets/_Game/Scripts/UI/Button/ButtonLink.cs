using UnityEngine;

public class ButtonLink : ButtonConfirmationTemplate
{
    [Header("Link")]
    public string url;
    public override void OnButtonClick()
    {
        base.OnButtonClick();

        CoreManager.Instance.panelConfirmation.SetAction(() =>
        {
            Application.OpenURL(url);
        });
    }
}
