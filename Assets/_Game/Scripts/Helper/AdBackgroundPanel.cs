using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AdBackgroundPanel : MonoBehaviour
{
    public static AdBackgroundPanel instance;

    public Transform spinnerLoadingScreen;
    public Image adBackgroundPanel;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    public void OpenPanel()
    {
        adBackgroundPanel.gameObject.SetActive(true);
        LeanTween.rotateAroundLocal(spinnerLoadingScreen.gameObject, Vector3.forward, -360, 1f).setRepeat(-1).setIgnoreTimeScale(true);

        Helper.AnimateAlpha(adBackgroundPanel, 0, 0.95f);
    }

    public void ClosePanel()
    {
        LeanTween.cancel(spinnerLoadingScreen.gameObject);
        adBackgroundPanel.gameObject.SetActive(false);

        Helper.AnimateAlpha(adBackgroundPanel, 0.95f, 0);
    }
}