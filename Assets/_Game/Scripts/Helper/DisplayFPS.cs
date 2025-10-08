using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayFPS : MonoBehaviour
{
    [SerializeField] private Image displayFpsBg;
    [SerializeField] private TMP_Text fpsText;

    private float _deltaTime;

    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 240;
    }

    private void Update()
    {
        _deltaTime += (Time.unscaledDeltaTime - _deltaTime) * 0.1f;
        float fps = 1 / _deltaTime;

        fpsText.text = Mathf.Ceil(fps).ToString() + " FPS";
    }

    public void ShowFPS(bool activate)
    {
        displayFpsBg.enabled = activate;
        fpsText.enabled = activate;
    }
}