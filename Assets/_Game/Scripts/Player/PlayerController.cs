using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Simple Forward Movement")]
    [Tooltip("Kecepatan maju player (unit per detik)")]
    public float forwardSpeed = 8f;

    // Opsional: kalau ada CharacterController, gunakan untuk gerakan yang lebih "physics-friendly"
    private CharacterController controller;
    private Animator animator;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();

        if (controller == null)
            Debug.LogWarning("[PlayerController_Simple] CharacterController tidak ditemukan — memakai transform.Translate sebagai fallback.");

        if (animator == null)
            Debug.Log("[PlayerController_Simple] Animator tidak ditemukan (opsional).");

        Debug.Log("[PlayerController_Simple] Simple forward movement aktif. forwardSpeed = " + forwardSpeed);
    }

    void Update()
    {
        // Buat vektor perpindahan untuk frame ini (per detik -> dikali Time.deltaTime)
        Vector3 displacement = transform.forward * forwardSpeed * Time.deltaTime;

        // Jika ada CharacterController, gunakan Move (lebih aman untuk collision)
        if (controller != null)
        {
            controller.Move(displacement);
        }
        else
        {
            // Fallback: pindahkan transform secara langsung (simple)
            transform.Translate(displacement, Space.World);
        }

        // Update animator (jika ada) supaya animasi lari bisa disinkronkan
        if (animator != null)
            animator.SetFloat("Speed", forwardSpeed);
    }
}
