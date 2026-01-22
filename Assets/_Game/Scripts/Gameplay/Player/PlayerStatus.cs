using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public static PlayerStatus Instance;

    [Header("Health")]
    public float currentHealth = 100;
    public float maxHealth = 100;

    [Header("Shooting")]
    public float fireSpeed = 0.5f;
    public float bulletSpeed = 10;
    public float bulletDamage = 5;

    [Header("Level")]
    public int Level = 1;
    public int exp = 0;
    public int expTarget;

    private void Awake()
    {
        Instance = this;
    }

    public void ResetData()
    {

        Level = 1;
        exp = 0;
        currentHealth = maxHealth;


        SetPlayerProfile();
    }
    public void AddExp(int amount)
    {
        exp += amount;

        while (exp >= expTarget)
        {
            exp -= expTarget;
            Level++;
            GameManager.Instance.LevelUp();
            SetPlayerProfile();
        }
        SetPlayerUI();
    }
    public void AddHealth(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        SetPlayerUI();
    }

    public void SetPlayerProfile()
    {
        // EXP target scaling
        expTarget = Mathf.RoundToInt(
            50 * Level * (1 + Level * 0.2f)
        );

        SetPlayerUI();
    }

    public void GetDamage(int damage)
    {
        currentHealth -= damage;

        SetPlayerUI();

        if (currentHealth <= 0)
            GameManager.Instance.GameFinish();
    }

    private void SetPlayerUI()
    {
        CanvasManager canvas = GameManager.Instance.canvasManager;
        canvas.SetTextHealth((int)currentHealth, (int)maxHealth);
        canvas.SetTextExp(Level, exp, expTarget);
    }

    public void ApplyUpgrade(UpgradeType type, float value)
    {
        switch (type)
        {
            case UpgradeType.Health:
                AddHealth((int)maxHealth); // Darah Full
                break;

            case UpgradeType.MaxHealth:
                maxHealth += value;
                break;

            case UpgradeType.FireRate:
                fireSpeed *= value; // -8%
                fireSpeed = Mathf.Max(fireSpeed, 0.01f);
                break;

            case UpgradeType.BulletSpeed:
                bulletSpeed += value;
                bulletSpeed = Mathf.Min(bulletSpeed, 50f);
                break;

            case UpgradeType.BulletDamage:
                //bulletDamage += Mathf.Max(1f, bulletDamage * 0.08f);
                bulletDamage += 3;
                break;
        }

        //Update UI
        SetPlayerUI();
    }
}
public enum UpgradeType
{
    Health,
    MaxHealth,
    FireRate,
    BulletSpeed,
    BulletDamage,
}
