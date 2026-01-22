using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCrate : MonoBehaviour
{
    public int rewardShooter = 5;

    private void OnTriggerEnter(Collider other)
    {
/*        if (other.CompareTag("Player"))
        {
            PlayerShooterManager.Instance.AddShooter(rewardShooter);
            gameObject.SetActive(false);
        }*/
    }
}
