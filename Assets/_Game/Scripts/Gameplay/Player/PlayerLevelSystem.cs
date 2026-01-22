using UnityEngine;

public class PlayerLevelSystem : MonoBehaviour
{
    public static PlayerLevelSystem Instance;

    [Header("Level Settings")]
    public int currentLevel = 1;
    public int currentExp = 0;

    public int baseExpToLevel = 100;   // Level 1 → 2 = 100 exp
    public float expMultiplier = 1.5f; // Level 2 → 3 = 150 (100 * 1.5)

    private int expToNextLevel;

    public delegate void LevelUpEvent(int newLevel);
    public event LevelUpEvent OnLevelUp;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        expToNextLevel = baseExpToLevel;
    }

    public void AddExp(int amount)
    {
        currentExp += amount;

        while (currentExp >= expToNextLevel)
        {
            currentExp -= expToNextLevel;
            LevelUp();
        }
    }

    private void LevelUp()
    {
        currentLevel++;

        // Perbarui exp requirement
        expToNextLevel = Mathf.RoundToInt(expToNextLevel * expMultiplier);

        Debug.Log($"LEVEL UP! Now level: {currentLevel}");

        OnLevelUp?.Invoke(currentLevel);
    }

    public float GetLevelProgress()
    {
        return (float)currentExp / expToNextLevel;
    }
}
