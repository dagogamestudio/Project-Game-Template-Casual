using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [Header("Start")] //Pake SO
    public float currentHealth;
    public float maxHealth = 50;
    public int scoreEnemy = 10;

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 2f;

    private EnemyHealthBar healthBar;
    private bool isActive;

    private void OnEnable()
    {
        isActive = true;
    }

    private void Update()
    {
        if (!isActive) return;
        transform.Translate(Vector3.back * moveSpeed * Time.deltaTime, Space.World);
    }

    public void SetUI()
    {
        currentHealth = maxHealth;

        // Spawn healthbar dari ObjectPool atau prefab di canvas
        GameObject barObj = ObjectPoolManager.Instance.SpawnFromPool("EnemyHealthBar", Vector3.zero, Quaternion.identity);
        healthBar = barObj.GetComponent<EnemyHealthBar>();

        // Inisialisasi agar healthbar tahu enemy & camera
        healthBar.Initialize(transform, Camera.main);

        // Pastikan health tampil penuh di awal
        healthBar.SetHealth(currentHealth, maxHealth);
        barObj.SetActive(false);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0;

        healthBar.gameObject.SetActive(true);
        healthBar.SetHealth(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            GameManager.Instance.AddScore(scoreEnemy);
            Die();
        }
    }
    public void Die()
    {
        // Matikan enemy
        gameObject.SetActive(false);
        // Matikan healthbar juga
        healthBar.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        isActive = false;
    }
}
