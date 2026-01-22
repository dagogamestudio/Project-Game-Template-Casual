using System;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float lifeTime = 3f;
    private float damage;
    private float timer;

    public Rigidbody rb;

    private void OnEnable()
    {
        timer = 0f;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= lifeTime)
            HideBullet();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Hit Enemy
        if (other.CompareTag("Enemy"))
        {
            ObjectPoolManager.Instance.SpawnFromPool("EffectHit", transform.position, transform.rotation);
            other.GetComponentInParent<EnemyController>()?.TakeDamage(damage);
        }

        HideBullet();
    }


    private void HideBullet()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        gameObject.SetActive(false);
    }

    internal void SetDamage(float bulletDamage)
    {
        damage = bulletDamage;
    }
}
