using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoreManager : MonoBehaviour
{
    public static CoreManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    [Header("Reference")]
    public PanelConfirmation panelConfirmation;
    public DisplayFPS panelFps;

    [Header("Panel Setting")]
    [SerializeField] private float animDuration = 0.5f;
    [SerializeField] private float widthOffset = 50f;
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private TMP_Text complainText;

    [Header("Notify")]
    public bool isNotifActive;
    public List<NotifyData> notifyQueue = new List<NotifyData>();


    public void ShowNotif(string message, float showDuration = 1)
    {
        if (isNotifActive)
        {
            SaveNextNotif(message, showDuration);
        }
        else
        {
            isNotifActive = true;
            ShowPopUp(message, showDuration);
        }
    }

    private void SaveNextNotif(string msg, float duration)
    {
        notifyQueue.Add(new NotifyData(msg, duration));
    }
    private void ShowPopUp(string text, float showDuration = 1)
    {
        LeanTween.cancel(rectTransform.gameObject);

        complainText.text = text;
        rectTransform.sizeDelta = new Vector2(complainText.preferredWidth + widthOffset, rectTransform.rect.height);

        rectTransform.LeanScale(Vector3.one, animDuration).setEaseOutBack().setIgnoreTimeScale(true);

        HideNotif(showDuration);
    }

    private void HideNotif(float delay)
    {
        rectTransform.LeanScale(Vector3.zero, animDuration).setEaseInBack().setOnComplete(() =>
        {
            CheckNextNotif();
        }).setIgnoreTimeScale(true).delay = delay;
    }

    private void CheckNextNotif()
    {
        isNotifActive = false;
        if (notifyQueue.Count > 0)
        {
            NotifyData newNotif = notifyQueue[0];
            notifyQueue.RemoveAt(0);
            ShowNotif(newNotif.message, newNotif.duration);
        }
    }
}

[Serializable]
public class NotifyData
{
    public string message;
    public float duration;
    public NotifyData(string message, float duration)
    {
        this.message = message;
        this.duration = duration;
    }
}
