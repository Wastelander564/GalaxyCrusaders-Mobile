using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private float Xmin;
    private float Xmax;

    public float padding = 0.7f;
    public float speed = 15.0f;
    public InputActionReference moveActionToUse; // Assigned in Inspector

    private Vector2 moveDirection; // Stores joystick movement

    private void Start()
    {
        SetScreenBorders();
    }

    private void SetScreenBorders()
    {
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        Xmin = leftmost.x + padding;
        Xmax = rightmost.x - padding;
    }

    private void Update()
    {
        moveDirection = moveActionToUse.action.ReadValue<Vector2>(); // Read gamepad movement
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(moveDirection.x, 0, 0) * speed * Time.fixedDeltaTime;
        transform.position = new Vector2(Mathf.Clamp(transform.position.x + movement.x, Xmin, Xmax), transform.position.y);
    }
}
