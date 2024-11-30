using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform cameraPosition;

    [Header("Camera Bobbing")]
    public float bobFrequency = 2f;       // Frequency of the bobbing
    public float bobAmplitude = 0.05f;   // Amplitude of the bobbing (vertical offset)
    public float bobSmoothing = 0.1f;    // Smooth transition when stopping

    private float bobTimer;

    void Update()
    {
        // Sync the camera with the player's position
        transform.position = cameraPosition.position;

        // Apply bobbing effect
        HandleBobbing();
    }

    void HandleBobbing()
    {
        // Check if the player is moving
        bool isMoving = Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0;

        if (isMoving)
        { 
            // Increment bob timer based on movement
            bobTimer += Time.deltaTime * bobFrequency;

            // Calculate vertical bob offset using sine wave
            float bobOffset = Mathf.Sin(bobTimer) * bobAmplitude;

            // Apply the bob offset to the camera's position
            transform.position += new Vector3(0, bobOffset, 0);
        }
        else
        {
            // Reset the bob timer when the player stops
            bobTimer = 0;
        }
    }
}

