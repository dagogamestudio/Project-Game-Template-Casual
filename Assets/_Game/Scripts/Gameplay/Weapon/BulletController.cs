using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float lifeTime = 3f;
    [SerializeField] private int damage = 10;

    private float timer;
    private Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

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
        // Jika nanti ada enemy:
        if (other.CompareTag("Enemy"))
        {
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
}
