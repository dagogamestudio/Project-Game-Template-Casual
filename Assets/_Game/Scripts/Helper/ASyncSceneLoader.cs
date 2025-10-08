using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class ASyncSceneLoader : MonoBehaviour
{
    public static ASyncSceneLoader Instance;
    public static event Action OnSceneFinishLoaded;

    [Header("Menu Screens")]
    [SerializeField] private PanelAnimation panelLoading;
    [SerializeField] private Image imageBackground;
    [SerializeField] private Sprite[] loadingSprite;

    [Header("Slider")]
    [SerializeField] private Image imageSliderLoading;

    [Header("Spinner Loading")]
    [SerializeField] private float delayLoading;
    [SerializeField] private Transform spinnerLoadingScreen;
    private bool isOnLoading = false;
    private const float _rotateSpeed = 360;
    

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }
    
    public void RestartScene()
    {
        ChangeScene(SceneManager.GetActiveScene().name);
    }

    public void ChangeScene(string sceneName)
    {
        Time.timeScale = 1;

        SaveManager.Instance?.SaveData();

        if (loadingSprite.Length > 1)
        {
            imageBackground.sprite = loadingSprite[Random.Range(0, loadingSprite.Length)];
        }
        else imageBackground.sprite = loadingSprite[0];

        panelLoading.OpenPanel();

        StartCoroutine(LoadSceneAsync(sceneName));
    }

    private void Update()
    {
        if (isOnLoading)
        {
            spinnerLoadingScreen.Rotate(0f, 0f, _rotateSpeed * Time.unscaledDeltaTime);
        }

    }
    IEnumerator LoadSceneAsync(string sceneName)
    {
        float elapsed = 0f;
        isOnLoading = true;

        while (elapsed < delayLoading)
        {
            elapsed += Time.unscaledDeltaTime;
            float percent = Mathf.Clamp01(elapsed / delayLoading);
            imageSliderLoading.fillAmount = percent * 0.8f;

            //spinnerLoadingScreen.Rotate(0f, 0f, _rotateSpeed * Time.unscaledDeltaTime);
            yield return null;
        }

        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneName);
        loadOperation.allowSceneActivation = false;

        while (loadOperation.progress < 0.9f)
        {
            float progressValue = Mathf.Clamp01(loadOperation.progress / 0.9f);
            imageSliderLoading.fillAmount = 0.8f + (progressValue * 0.2f);

            //spinnerLoadingScreen.Rotate(0f, 0f, _rotateSpeed * Time.unscaledDeltaTime);
            yield return null;
        }
        loadOperation.allowSceneActivation = true;

        yield return new WaitForSeconds(1f);

        isOnLoading = false;
        panelLoading.ClosePanel();
        OnSceneFinishLoaded?.Invoke();
    }
}