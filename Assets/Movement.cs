using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;
    private bool isGrounded;

    private Rigidbody rb;

    void Start()
    {
        // Get the Rigidbody component on the player GameObject
        rb = GetComponent<Rigidbody>();
        // Freeze rotation to prevent the player from tipping over
        rb.freezeRotation = true;
    }

    void Update()
    {
        // Check if the player is grounded (you might want to implement a more robust solution)
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.1f);

        // Get input from the player
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate movement direction
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * speed * Time.deltaTime;

        // Move the player using Rigidbody
        rb.MovePosition(transform.position + movement);

        // Jumping
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            // Apply an upward force to simulate jumping
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
