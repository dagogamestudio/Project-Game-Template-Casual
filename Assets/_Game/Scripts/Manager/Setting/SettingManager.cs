using System;
using UnityEngine;

public class SettingManager : MonoBehaviour
{
    public static SettingManager Instance;

    public GameSettings currentSettings = new GameSettings();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
            LoadSettings();
        }
        else
            Destroy(gameObject);

    }
    private void Start()
    {
        ApplySettings();
    }


    public Action OnSettingChange;


    // === Save & Load ===
    public void SaveSettings()
    {
        ApplySettings();

        string json = JsonUtility.ToJson(currentSettings);
        PlayerPrefs.SetString("GameSettings", json);
        PlayerPrefs.Save();
    }

    public void LoadSettings()
    {
        if (PlayerPrefs.HasKey("GameSettings"))
        {
            string json = PlayerPrefs.GetString("GameSettings");
            currentSettings = JsonUtility.FromJson<GameSettings>(json);
        }
        else
        {
            GameSettings newSetting = new GameSettings();
            currentSettings = newSetting;
            SaveSettings();
        }
    }

    // === Apply ke Unity ===
    public void ApplySettings()
    {
        QualitySettings.SetQualityLevel(currentSettings.graphicsQuality);
        Application.targetFrameRate = currentSettings.targetFrameRate;

        SoundManager.Instance?.SetBGMVolume(currentSettings.musicVolume);
        SoundManager.Instance?.SetSFXVolume(currentSettings.sfxVolume);

        CoreManager.Instance?.panelFps?.ShowFPS(currentSettings.showFps);

        OnSettingChange?.Invoke();
    }

    // === Getter & Setter ===
    public void SetMusicVolume(float value)
    {
        currentSettings.musicVolume = value;
        SaveSettings();
    }

    public void SetSfxVolume(float value)
    {
        Debug.Log("Change volume sfx " + value);
        currentSettings.sfxVolume = value;
        SaveSettings();
    }

    public void SetGraphics(int index)
    {
        currentSettings.graphicsQuality = index;
        SaveSettings();
    }

    public void SetFrameRate(int fps)
    {
        currentSettings.targetFrameRate = fps;
        SaveSettings();
    }

    public void SetPostProcessing(bool enabled)
    {
        currentSettings.postProcessing = enabled;
        SaveSettings();
    }
    public void SetShowFPS(bool enabled)
    {
        currentSettings.showFps = enabled;
        SaveSettings();
    }
}


[System.Serializable]
public class GameSettings
{
    [Range(0f, 1f)] public float musicVolume = 1f;
    [Range(0f, 1f)] public float sfxVolume = 1f;

    public int graphicsQuality = 1;   // 0 = Low, 1 = Medium, 2 = High
    public int targetFrameRate = 60;  // bisa 30, 60, 90, 120

    public bool postProcessing = true;
    public bool showFps = true;
}
