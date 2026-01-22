using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    public PlayerStatus playerStat;

    [Header("Shooting Settings")]
    [SerializeField] private string bulletTag;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private ParticleSystem effectShoot;
    [SerializeField] private ParticleSystem effectShoot2;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform firePoint2;

    private float fireTimer;

    public void AutoShoot()
    {
        fireTimer += Time.deltaTime;
        if (fireTimer >= playerStat.fireSpeed)
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
        SetBullet(bullet);
        effectShoot.Play();


        if (PlayerStatus.Instance.Level >= 10)
        {
            GameObject bullet2 = ObjectPoolManager.Instance.SpawnFromPool(bulletTag, firePoint2.position, firePoint2.rotation);
            SetBullet(bullet2);
            effectShoot2.Play();
        }
        SoundManager.Instance.PlaySFX("Shoot");
    }

    private void SetBullet(GameObject bullet)
    {

        BulletController bulletController = bullet.GetComponent<BulletController>();

        bulletController.SetDamage(playerStat.bulletDamage);
        bulletController.rb.velocity = firePoint.forward * playerStat.bulletSpeed;
    }
}
