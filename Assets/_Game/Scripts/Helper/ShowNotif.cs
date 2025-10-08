using TMPro;
using UnityEngine;

public class ShowNotif : MonoBehaviour
{
    public static ShowNotif instance;

    [SerializeField] private float animDuration = 0.25f;
    [SerializeField] private float widthOffset = 50;
    
    private RectTransform rectTransform;
    private TMP_Text complainText;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        rectTransform = GetComponent<RectTransform>();
        complainText = GetComponentInChildren<TMP_Text>();
    }

    public void ShowNotikasi(string complain, float showDuration = 1)
    {
        LeanTween.cancel(gameObject);

        complainText.text = complain;
        rectTransform.sizeDelta = new Vector2(complainText.preferredWidth + widthOffset, rectTransform.rect.height);

        LeanTween.scale(gameObject, Vector3.one, animDuration).setEaseOutCubic();

        HideNotifikasi(showDuration);
    }

    private void HideNotifikasi(float delay)
    {
        LeanTween.scale(gameObject, Vector3.zero, animDuration).setEaseInCubic().delay = delay;
    }
}