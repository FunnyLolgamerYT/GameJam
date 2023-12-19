using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;
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
        int layerMask = 1 << 8;

        layerMask = ~layerMask;
        // Check if the player is grounded (you might want to implement a more robust solution)
        RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.down, out hit, 1f, layerMask)) {

            Debug.DrawRay(transform.position, Vector3.down * hit.distance, Color.yellow);
            Debug.Log("Did hit");
            isGrounded = true;

        } else {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            Debug.Log("Did not Hit");
            isGrounded = false;
        }

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
