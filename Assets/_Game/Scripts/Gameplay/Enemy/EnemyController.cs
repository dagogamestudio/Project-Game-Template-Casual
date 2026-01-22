using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EnemyType enemyType;

    [Header("BASE STATS (JANGAN DIUBAH RUNTIME)")]
    [SerializeField] private float baseMaxHealth = 10f;
    [SerializeField] private int baseDamage = 1;
    [SerializeField] private float baseMoveSpeed = 0.75f;

    [Header("Health Settings")]
    public bool isAlive;
    public float currentHealth;
    public float maxHealth;
    public int damage;
    public float moveSpeed;

    public int scoreEnemy = 10;
    public int expAmount;

    [Header("Reference")]
    public Animator Animator;
    public int minGetCoin, maxGetCoin;


    private EnemyHealthBar healthBar;
    private void Awake()
    {
        EnemyMovementFactory.AttachMovement(gameObject, enemyType);
    }
    private void OnEnable()
    {
        ResetStats();
    }
    private void ResetStats()
    {
        isAlive = true;
        maxHealth = baseMaxHealth;
        damage = baseDamage;
        moveSpeed = baseMoveSpeed;
        currentHealth = maxHealth;
    }
    public void ApplyWaveScaling(int wave)
    {
        maxHealth = baseMaxHealth * (1f + (wave - 1) * 0.15f);
        damage = Mathf.RoundToInt(baseDamage * (1f + (wave - 1) * 0.10f));
        moveSpeed = baseMoveSpeed * (1f + GetMoveSpeedBonus(wave));

        currentHealth = maxHealth;
    }
    float GetMoveSpeedBonus(int wave)
    {
        // aman: naik pelan, ada cap
        return Mathf.Min(0.25f, (wave - 1) * 0.02f); // max +25%
    }
    public void SetUI()
    {
        GameObject barObj = ObjectPoolManager.Instance.SpawnFromPool("EnemyHealthBar", Vector3.zero, Quaternion.identity);

        healthBar = barObj.GetComponent<EnemyHealthBar>();
        healthBar.Initialize(transform, Camera.main);
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
            PlayerStatus.Instance.AddExp(expAmount);
            GameManager.Instance.AddScore(scoreEnemy);
            Die(true);
        }
    }

    public void Die(bool isDropCoin)
    {
        if (isDropCoin)
        {
            int amountCoin = Random.Range(minGetCoin, maxGetCoin);
            GameManager.Instance.PlayerCoin += amountCoin;

            SoundManager.Instance.PlaySFX("EnemyDie");
        }
        else
        {
            //Effect layar merah, sound sakit
        }

        WaveManager.Instance.EnemyDie();
        isAlive = false;
        healthBar.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}

public enum EnemyType
{
    Pocong,
    Babi,
    Tuyul,
    Boss
}