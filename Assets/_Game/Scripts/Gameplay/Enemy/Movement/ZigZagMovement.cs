using UnityEngine;

public class ZigZagMovement : MonoBehaviour, IMovement
{
    private EnemyController controller;
    public float zigzagAmount = 1f;
    public float zigzagSpeed = 1.5f;
    private float time;

    public void Init(EnemyController controller)
    {
        this.controller = controller;
    }

    private void Update()
    {
        if (controller == null && !GameManager.Instance.isPlaying) return;

        time += Time.deltaTime;
        float x = Mathf.Sin(time * zigzagSpeed) * zigzagAmount;

        Vector3 move = new Vector3(x, 0, -controller.moveSpeed);
        transform.Translate(move * Time.deltaTime, Space.World);
    }
}
