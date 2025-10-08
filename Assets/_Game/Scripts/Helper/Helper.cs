using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;
using Random = UnityEngine.Random;

public static class Helper
{
    #region Raycast
    public static Ray RayFromCamera(Camera cam) => cam.ViewportPointToRay(new(0.5f, 0.5f, 0));
    public static bool PerformRaycast(Camera cam, out RaycastHit hit, float distance) => Physics.Raycast(RayFromCamera(cam), out hit, distance);
    public static bool PerformRaycast(Camera cam, out RaycastHit hit, float distance, LayerMask layerMask) => Physics.Raycast(RayFromCamera(cam), out hit, distance, layerMask);
    #endregion

    #region Animate Alpha
    public static void AnimateAlpha(Image image, float from, float to, float animDuration = 0.25f)
    {
        image.raycastTarget = to != 0;
        LeanTween.value(from, to, animDuration).setEaseInOutCubic().setOnUpdate(alpha =>
        {
            Color color = image.color;
            color.a = alpha;
            image.color = color;
        }).setIgnoreTimeScale(true);
    }
    public static void AnimateAlpha(TextMeshProUGUI text, float from, float to, float animDuration = 0.25f)
    {
        text.raycastTarget = to != 0;
        LeanTween.value(from, to, animDuration).setEaseInOutCubic().setOnUpdate(alpha =>
        {
            Color color = text.color;
            color.a = alpha;
            text.color = color;
        }).setIgnoreTimeScale(true);
    }
    public static void AnimateAlpha(TextMeshProUGUI text, float from, float to, System.Action onComplete, float animDuration = 0.25f)
    {
        text.raycastTarget = to != 0;
        LeanTween.value(from, to, animDuration).setEaseInOutCubic().setOnUpdate(alpha =>
        {
            Color color = text.color;
            color.a = alpha;
            text.color = color;
        }).setIgnoreTimeScale(true).setOnComplete(onComplete);
    }
    public static void AnimateAlphaAndScale(Image image, float from, float to, GameObject target, Vector3 scaleTo, System.Action onComplete, float animDuration = 0.25f)
    {
        AnimateAlpha(image, from, to, animDuration);
        LeanTween.scale(target, scaleTo, animDuration).setEaseOutCubic().setIgnoreTimeScale(true).setOnComplete(() => onComplete?.Invoke());
    }
    public static void AnimateAlphaAndScale(Image image, float from, float to, GameObject target, Vector3 scaleTo, float animDuration = 0.25f)
    {
        AnimateAlpha(image, from, to);
        LeanTween.scale(target, scaleTo, animDuration).setEaseInOutCubic().setIgnoreTimeScale(true);
    }
    public static void AnimateAlphaAndScale(TextMeshProUGUI text, float from, float to, GameObject target, Vector3 scale, float animDuration = 0.25f)
    {
        AnimateAlpha(text, from, to);
        LeanTween.scale(target, scale, animDuration).setEaseInOutCubic().setIgnoreTimeScale(true);
    }
    public static void AnimateCanvasGroup(CanvasGroup canvas, float from, float to, float animDuration = 0.25f)
    {
        canvas.alpha = from;
        LeanTween.value(from, to, animDuration).setEaseInOutCubic().setOnUpdate(alpha =>
        {
            canvas.alpha = alpha;
        }).setIgnoreTimeScale(true);
    }
    public static void AnimateCanvasGroup(CanvasGroup canvas, float from, float to, System.Action onComplete, float animDuration = 0.25f)
    {
        canvas.alpha = from;
        LeanTween.value(from, to, animDuration).setEaseInOutCubic().setOnUpdate(alpha =>
        {
            canvas.alpha = alpha;
        }).setIgnoreTimeScale(true).setOnComplete(onComplete);
    }
    public static void AnimateScale(GameObject target, Vector3 scaleTo, Action onComplete, float animDuration = 0.25f)
    {
        LeanTween.scale(target, scaleTo, animDuration).setEaseOutCubic().setIgnoreTimeScale(true).setOnComplete(() => onComplete?.Invoke());
    }

    #endregion

    #region Turn IDR Value
    public static string TurnToIDRValue(decimal value) => value.ToString("N0", new CultureInfo("id-ID"));
    public static string TurnToIDRValue(float value) => TurnToIDRValue((decimal)value);
    public static string TurnToIDRValue(int value) => TurnToIDRValue((decimal)value);
    #endregion

    public static void EnableAllColliders(GameObject gObj, bool enable)
    {
        Collider[] colliders = gObj.GetComponentsInChildren<Collider>();
        if (colliders.Length < 1)
        {
            colliders = gObj.GetComponents<Collider>();
        }
        foreach (Collider col in colliders)
        {
            col.enabled = enable;
        }
    }

    public static void ChangeLayer(GameObject obj, int layerMask)
    {
        obj.layer = layerMask;

        Renderer[] meshRend = obj.GetComponentsInChildren<Renderer>(true);
        foreach (Renderer mr in meshRend)
        {
            if (mr.gameObject.layer != LayerMask.NameToLayer("ScannerCam") && mr is not ParticleSystemRenderer) mr.gameObject.layer = layerMask;
        }
    }

    #region Set Button
    public static void SetButton(Button button, bool enable)
    {
        TMP_Text textnya = button.GetComponentInChildren<TMP_Text>();
        Image imgnya = button.GetComponent<Image>();

        if (textnya.enabled != enable) textnya.enabled = enable;
        if (imgnya.enabled != enable) imgnya.enabled = enable;
    }
    public static void SetButton(Button button, bool enable, string nama)
    {
        TMP_Text textnya = button.GetComponentInChildren<TMP_Text>();

        textnya.text = nama;
        if (textnya.enabled != enable) textnya.enabled = enable;
        if (button.enabled != enable) button.GetComponent<Image>().enabled = enable;
    }
    #endregion

    public static int RandomValue(int min, int max) => Random.Range(min, max);
    public static float RandomValue(float min, float max) => Random.Range(min, max);
    public static bool IsInternetActive() => Application.internetReachability != NetworkReachability.NotReachable;
}


[Serializable]
public class SettingsSaveData
{
    public float BGMVolume;
    public float SFXVolume;
    public float sensitivity;
    public float resolution;
    public float drawDistance;
    public bool shadow;
    public bool showFps;
    public bool showPost;
    public int indexGrapich;
    public int indexLocalized;


    public override string ToString()
    {
        return $"SettingsSaveData:\n" +
               $"- BGMVolume: {BGMVolume}\n" +
               $"- SFXVolume: {SFXVolume}\n" +
               $"- Sensitivity: {sensitivity}\n" +
               $"- Resolution: {resolution}\n" +
               $"- DrawDistance: {drawDistance}\n" +
               $"- Shadow: {shadow}\n" +
               $"- ShowFPS: {showFps}\n" +
               $"- ShowPost: {showPost}";
    }
}

public enum AxisVector3
{
    x,
    y,
    z
}

public enum PlayerState
{
    HoldItemState,
    NotHoldItemState,
    FacingNPCState
}