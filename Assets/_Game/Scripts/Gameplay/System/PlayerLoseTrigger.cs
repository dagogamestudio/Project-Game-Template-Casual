using UnityEngine;

public class PlayerLoseTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyController enemy = other.GetComponentInParent<EnemyController>();
            if (enemy != null)
            {
                PlayerStatus.Instance.GetDamage(enemy.damage * 2);
                enemy.Die(false);
            }
        }
    }
}
