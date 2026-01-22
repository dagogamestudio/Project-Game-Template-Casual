using UnityEngine;

public static class EnemyMovementFactory
{
    public static void AttachMovement(GameObject enemy, EnemyType type)
    {
        foreach (var m in enemy.GetComponents<MonoBehaviour>())
        {
            if (m is IMovement) Object.Destroy(m);
        }

        EnemyController controller = enemy.GetComponent<EnemyController>();
        IMovement movement = null;

        switch (type)
        {
            case EnemyType.Pocong:
                movement = enemy.AddComponent<PocongMovement>();
                break;
            case EnemyType.Babi:
                movement = enemy.AddComponent<ForwardMovement>();
                break;
            case EnemyType.Tuyul:
                movement = enemy.AddComponent<ZigZagMovement>();
                break;

            case EnemyType.Boss:
                movement = enemy.AddComponent<BossMovement>();
                break;
        }

        movement?.Init(controller);
    }
}
public interface IMovement
{
    void Init(EnemyController controller);
}