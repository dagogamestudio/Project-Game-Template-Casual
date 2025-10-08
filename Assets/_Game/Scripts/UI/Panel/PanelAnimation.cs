using UnityEngine;
using UnityEngine.UI;

public class PanelAnimation : MonoBehaviour
{
    public string panelName;
    [SerializeField] private Image parentPanel;
    [SerializeField] private GameObject contentPanel;
    [SerializeField] private bool freezeTime;

    [SerializeField] private float animDuration = 0.25f;
    [SerializeField] private float fadeValue = 0.95f;

    public void OpenPanel()
    {
        gameObject.SetActive(true);
        contentPanel.transform.localScale = Vector3.zero;

        Helper.AnimateAlpha(parentPanel, 0, fadeValue, animDuration);
        Helper.AnimateScale(contentPanel, Vector3.one, () =>
        {
            if (freezeTime) Time.timeScale = 0;
        }, animDuration);
    }

    public void ClosePanel()
    {
        Helper.AnimateAlpha(parentPanel, fadeValue, 0, animDuration);
        Helper.AnimateScale(contentPanel, Vector3.zero, () =>
        {
            if (freezeTime) Time.timeScale = 1;
            gameObject.SetActive(false);
        }, animDuration);
    }

    public void CloseCanvasPanel()
    {
        GameManager.Instance?.canvasManager?.HidePanel(panelName);
    }
}