using UnityEngine;

public class ForwardMovement : MonoBehaviour, IMovement
{
    private EnemyController controller;
    public void Init(EnemyController controller)
    {
        this.controller = controller;
    }
    private void Update()
    {
        if (controller == null && !GameManager.Instance.isPlaying) return;

        transform.Translate(
            Vector3.back * controller.moveSpeed * Time.deltaTime,
            Space.World
        );
    }
}
