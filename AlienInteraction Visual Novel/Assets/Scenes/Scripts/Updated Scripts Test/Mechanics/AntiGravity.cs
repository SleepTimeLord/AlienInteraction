using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class AntiGravity : MonoBehaviour
{
    public Transform turnCamUpsidedown;
    public GameObject player;
    public float smooth = 5f;

    private PlayerController playerController;
    private PlayerMovement playerMovement;
    private bool gravitySwitch = false;
    private CinemachineBrain cineBrain;

    void Awake()
    {
        cineBrain = Camera.main.GetComponent<CinemachineBrain>();
    }

    void OnEnable()
    {
        playerMovement = FindAnyObjectByType<PlayerMovement>();
        playerController = new PlayerController();
        playerController.PlayerControls.ActivateAntiGravity.started += ToggleAntiGravity;
        playerController.Enable();
    }

    void OnDisable()
    {
        playerController.PlayerControls.ActivateAntiGravity.started -= ToggleAntiGravity;
        playerController.Disable();
    }

    void LateUpdate()
    {
        // Drive the flip logic every frame
        ApplyAntiGravity();
    }

    public void ApplyAntiGravity()
    {
        // Determine target rotation (roll 180° around Z when flipped)
        Quaternion target = gravitySwitch
            ? Quaternion.Euler(0f, 0f, 180f)
            : Quaternion.Euler(0f, 0f, 0f);

        // Smoothly rotate player and helper
        player.transform.rotation = Quaternion.Slerp(player.transform.rotation, target, Time.deltaTime * smooth);
        turnCamUpsidedown.rotation = Quaternion.Slerp(turnCamUpsidedown.rotation, target, Time.deltaTime * smooth);

        // Flip gravity
        Physics.gravity = gravitySwitch
            ? new Vector3(0f, +9.8f, 0f)
            : new Vector3(0f, -9.8f, 0f);

        // Make sure Cinemachine reads the new up
        cineBrain.WorldUpOverride = turnCamUpsidedown;
    }

    void ToggleAntiGravity(InputAction.CallbackContext ctx)
    {
        if (playerMovement.isOnGround)
            gravitySwitch = !gravitySwitch;
    }
}
