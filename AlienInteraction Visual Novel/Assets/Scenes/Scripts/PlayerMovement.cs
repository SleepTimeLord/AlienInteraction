using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Movement speed
    public Transform orientation; // Reference to the camera's orientation
    public Transform playerPosition;

    private Rigidbody rb;
    private Vector3 movementInput;
    //private SleepReset isSleeping;

    private Vector3 startingPosition;
    private void Start()
    {
        //isSleeping = FindObjectOfType<SleepReset>();
        rb = GetComponent<Rigidbody>();

        startingPosition = playerPosition.position;
        if (rb == null)
        {
            Debug.LogError("Rigidbody component not found on player object!");
        }

        // Freeze rotation so physics doesn't interfere with player control
        rb.freezeRotation = true;
    }

    private void Update()
    {

        // Get input for movement
        float horizontal = Input.GetAxisRaw("Horizontal"); // A/D or Left/Right
        float vertical = Input.GetAxisRaw("Vertical");     // W/S or Up/Down

        // Calculate movement direction relative to the orientation
        Vector3 forward = orientation.forward;
        Vector3 right = orientation.right;

        // Flatten the vectors to keep movement on the XZ plane
        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        // Combine input with orientation vectors
        movementInput = (forward * vertical + right * horizontal).normalized;
        
    }

    private void FixedUpdate()
    {
        // Calculate velocity
        Vector3 moveVelocity = movementInput * moveSpeed;

        // Apply movement via Rigidbody
        rb.linearVelocity = new Vector3(moveVelocity.x, rb.linearVelocity.y, moveVelocity.z);
        
    }

    public void ResetPlayerPos()
    {
        playerPosition.position = startingPosition;
    }
}

