using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelSetting : MonoBehaviour
{
    [Header("Category")]
    public List<Button> listButtonCategory;
    public List<GameObject> listContentCategory;

    [Header("UI Elements")]
    public Slider musicSlider;
    public Slider sfxSlider;
    public List<Button> graphicsButtons;
    public List<Button> frameRateButtons;

    public Toggle postProcessingToggle;
    public Toggle showFpsToggle;

    private int[] fpsOptions = new int[] { 30, 60, 90, 120 };

    private void Start()
    {
        AssignUI();
    }

    public void SetCategory(int index)
    {
        for (int i = 0;i < listButtonCategory.Count; i++)
        {
            listButtonCategory[i].interactable = (i != index);
            listContentCategory[i].SetActive(i == index);
        }
    }
    private void AssignUI()
    {
        // === Category Buttons ===
        for (int i = 0; i < listButtonCategory.Count; i++)
        {
            int temp = i;
            listButtonCategory[i].onClick.AddListener(() => SetCategory(temp));
        }
        SetCategory(0);

        // === Init value dari SettingManager ===
        var s = SettingManager.Instance.currentSettings;

        musicSlider.value = s.musicVolume;
        sfxSlider.value = s.sfxVolume;
        postProcessingToggle.isOn = s.postProcessing;
        showFpsToggle.isOn = s.showFps;

        // === Binding sliders & toggles ===
        musicSlider.onValueChanged.AddListener(SettingManager.Instance.SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SettingManager.Instance.SetSfxVolume);
        postProcessingToggle.onValueChanged.AddListener(SettingManager.Instance.SetPostProcessing);
        showFpsToggle.onValueChanged.AddListener(SettingManager.Instance.SetShowFPS);

        // === Graphics buttons ===
        for (int i = 0; i < graphicsButtons.Count; i++)
        {
            int index = i; // perlu local var biar delegate benar
            graphicsButtons[i].onClick.AddListener(() => {
                SettingManager.Instance.SetGraphics(index);
                RefreshUI();
            });
        }

        // === Framerate buttons ===
        for (int i = 0; i < frameRateButtons.Count; i++)
        {
            int fps = fpsOptions[i];
            frameRateButtons[i].onClick.AddListener(() => {
                SettingManager.Instance.SetFrameRate(fps);
                RefreshUI();
            });
        }

        RefreshUI();
    }

    private void RefreshUI()
    {
        var s = SettingManager.Instance.currentSettings;

        // === Graphics Buttons State ===
        for (int i = 0; i < graphicsButtons.Count; i++)
        {
            graphicsButtons[i].interactable = (s.graphicsQuality != i);
        }

        // === Framerate Buttons State ===
        for (int i = 0; i < frameRateButtons.Count; i++)
        {
            frameRateButtons[i].interactable = (s.targetFrameRate != fpsOptions[i]);
        }
    }
}
