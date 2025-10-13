using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private Vector2 lastPointerPosition;
    private bool isDragging;

    public float DragDeltaX { get; private set; }

    [SerializeField] private float smoothTime = 0.1f; // smoothing input
    private float targetDeltaX;
    private float deltaVelocity;

    void Update()
    {
#if UNITY_EDITOR
        HandleMouseInput();
#else
        HandleTouchInput();
#endif

        // Smooth agar gerakan tidak patah antar frame
        DragDeltaX = Mathf.SmoothDamp(DragDeltaX, targetDeltaX, ref deltaVelocity, smoothTime);
    }

    void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            lastPointerPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            targetDeltaX = 0f;
        }

        if (isDragging)
        {
            Vector2 current = Input.mousePosition;
            targetDeltaX = (current.x - lastPointerPosition.x) / Screen.width;
            lastPointerPosition = current;
        }
        else
        {
            // Lepas jari → decay natural ke 0
            targetDeltaX = Mathf.Lerp(targetDeltaX, 0f, Time.deltaTime * 10f);
        }
    }

    void HandleTouchInput()
    {
        if (Input.touchCount == 0)
        {
            isDragging = false;
            targetDeltaX = 0f;
            return;
        }

        Touch touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Began)
        {
            isDragging = true;
            lastPointerPosition = touch.position;
        }
        else if (touch.phase == TouchPhase.Moved)
        {
            Vector2 current = touch.position;
            targetDeltaX = (current.x - lastPointerPosition.x) / Screen.width;
            lastPointerPosition = current;
        }
        else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
        {
            isDragging = false;
            targetDeltaX = 0f;
        }
    }
}
