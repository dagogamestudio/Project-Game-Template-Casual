using System.Collections;
using UnityEngine;

public class PocongMovement : MonoBehaviour, IMovement
{
    private float delayMove = 1f;
    private float delayIdle = .5f;
    private float counterMovement;
    private bool isIdle;

    private EnemyController controller;
    public void Init(EnemyController controller)
    {
        this.controller = controller;
    }
    private void Update()
    {
        if (controller == null || !GameManager.Instance.isPlaying) return;

        counterMovement -= Time.deltaTime;

        if(counterMovement > 0 )
        {
            if (!isIdle)
            {
                transform.Translate(
                   Vector3.back * controller.moveSpeed * Time.deltaTime,
                   Space.World
               );
            }
        }
        else
        {
            if (isIdle)
            {
                counterMovement = delayMove;
            }
            else
            {
                controller.Animator.SetTrigger("Jump");
                counterMovement = Random.Range(delayIdle, delayIdle * 2);
            }

            isIdle = !isIdle;
        }

       
    }
}
