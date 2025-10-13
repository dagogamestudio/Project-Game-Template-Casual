using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("Shooting Settings")]
    [SerializeField] private string bulletTag;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 0.2f;
    [SerializeField] private float bulletSpeed = 20f;

    private float fireTimer;

    public void AutoShoot()
    {
        fireTimer += Time.deltaTime;
        if (fireTimer >= fireRate)
        {
            Fire();
            fireTimer = 0f;
        }
    }

    private void Fire()
    {
        if (bulletPrefab == null || firePoint == null)
            return;

        GameObject bullet = ObjectPoolManager.Instance.SpawnFromPool(bulletTag, firePoint.position, firePoint.rotation);
        if (bullet.TryGetComponent(out Rigidbody rb))
            rb.velocity = firePoint.forward * bulletSpeed;
    }
}
