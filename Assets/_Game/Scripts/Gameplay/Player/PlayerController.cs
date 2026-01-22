using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    [Header("Player")]
    public PlayerShooter playerShooting;

    [Header("Movement Settings")]
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private float horizontalLimit;
    [SerializeField] private float smoothMove = 10f;

    private PlayerInput playerInput;
    private Vector3 targetPosition;
    private Rigidbody rb;


    private float targetX; // posisi X yang diinginkan

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();
        targetPosition = transform.position;
    }

    private void Update()
    {
        if (GameManager.Instance.isPlaying)
        {
            // Baca input di Update (lebih akurat)
            float delta = playerInput.DragDeltaX * horizontalSpeed;
            targetX = Mathf.Clamp(rb.position.x + delta, -horizontalLimit, horizontalLimit);


            playerShooting.AutoShoot();
        }
    }

    private void FixedUpdate()
    {
        if (!GameManager.Instance.isPlaying) return;

        // Smooth movement di FixedUpdate
        Vector3 targetPos = new Vector3(targetX, rb.position.y, rb.position.z);
        rb.MovePosition(Vector3.Lerp(rb.position, targetPos, smoothMove * Time.fixedDeltaTime));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyController enemy = collision.gameObject.GetComponentInParent<EnemyController>();
            if (enemy != null)
            {
                PlayerStatus.Instance.GetDamage(enemy.damage);
                enemy.Die(false);
            }

            //GameManager.Instance.GameFinish();
        }
    }
}
