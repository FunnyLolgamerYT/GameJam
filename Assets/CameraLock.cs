using UnityEngine;

public class CameraLock : MonoBehaviour
{
    public Transform playerBody;
    public float rotationSpeed = 5f;

    public Vector3 initialRotationOffset = new Vector3(0f, 90f, 0f);

    void Start() {

        playerBody.rotation *= Quaternion.Euler(initialRotationOffset);
    }

    void Update()
    {
        HandleRotation();
        HandleMovement();
    }

    void HandleRotation()
    {
        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0f;

        Quaternion targetRotation = Quaternion.LookRotation(cameraForward);
        playerBody.rotation = Quaternion.Slerp(playerBody.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    void HandleMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        Vector3 moveVector = Quaternion.Euler(0f, playerBody.eulerAngles.y, 0f) * moveDirection;

        transform.Translate(moveVector * Time.deltaTime);
    }
}
