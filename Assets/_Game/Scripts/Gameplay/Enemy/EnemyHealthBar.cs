using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Image fillImage;
    private Transform target;
    private Camera cam;

    public void Initialize(Transform enemy, Camera mainCamera)
    {
        target = enemy;
        cam = mainCamera;
    }

    void Update()
    {
        if (target == null) return;

        Vector3 screenPos = cam.WorldToScreenPoint(target.position + Vector3.up * 1f);
        transform.position = screenPos;
    }

    public void SetHealth(float current, float max)
    {
        fillImage.fillAmount = current / max;
    }
}