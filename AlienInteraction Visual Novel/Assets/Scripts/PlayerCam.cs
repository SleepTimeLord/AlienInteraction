using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform orientation;

    private float xRotation;
    private float yRotation;
    public Quaternion originalRotation;
    public Quaternion originalOrientationRotation;
    private SleepReset isSleeping;

    void Start()
    {
        isSleeping = FindObjectOfType<SleepReset>();
        originalRotation = transform.rotation;
        originalOrientationRotation = orientation.rotation;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    void Update()
    {
        if (!isSleeping.sleeping)
        {
            // Mouse input for camera rotation
            float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
            float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

            yRotation += mouseX;

            xRotation += mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        }
    }

    public void ResetCamPosition()
    {
        xRotation = originalRotation.x;
        yRotation = originalRotation.y;
        transform.rotation = originalRotation;
        orientation.rotation = originalOrientationRotation;
    }
}

