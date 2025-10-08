using UnityEngine;

public class FacingPlayer : MonoBehaviour
{
    private Vector3 playerDirection;

    void LateUpdate()
    {
        //playerDirection = player.mainCam.transform.position - transform.position;
        playerDirection.y = 0;
        transform.rotation = Quaternion.LookRotation(-playerDirection);
    }
}