using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float jumpHeight = 2f;
    public float gravity = -10f;
    public float moveSpeed = 10f;
    public float mouseSensitivity = 100f;

    private Vector3 velocity;
    private bool isGrounded;
    private float xRotation = 0f;
    private CharacterController controller;
    private Camera playerCamera;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerCamera = GetComponentInChildren<Camera>();
        // Lock and hide the cursor for FPS controls
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        isGrounded = controller.isGrounded;
        // Prevent gravity from accumulating while grounded
        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        // Use physics formula to calculate correct launch velocity for desired jump height
        if (Input.GetButtonDown("Jump") && isGrounded)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        // Move relative to the direction the player is facing
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 move = transform.right * horizontal + transform.forward * vertical;
        controller.Move(move * moveSpeed * Time.deltaTime);

        // Pull the player downward each frame to simulate gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Horizontal mouse movement rotates the player body left and right
        // Vertical mouse movement rotates only the camera up and down, clamped to prevent flipping
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation - mouseY, -90f, 90f);
        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
}