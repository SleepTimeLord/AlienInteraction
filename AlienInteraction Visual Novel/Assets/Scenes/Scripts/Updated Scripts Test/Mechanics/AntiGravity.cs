using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class AntiGravity : MonoBehaviour
{
    PlayerController playerController;

    public bool gravitySwitch = false;
    public Transform turnCamUpsidedown;
    public GameObject player;
    private PlayerMovement playerMovement;

    private float smooth = 5f;

    private void OnEnable()
    {
        playerMovement = FindAnyObjectByType<PlayerMovement>();
        playerController = new PlayerController();
        playerController.PlayerControls.ActivateAntiGravity.started += ToggleAntiGravity;
        playerController.Enable();
    }

    private void OnDisable()
    {
        playerController.PlayerControls.ActivateAntiGravity.started -= ToggleAntiGravity;
        playerController.Disable();
    }

    public void GravitySwitch()
    {
        if (gravitySwitch)
        {
            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, Quaternion.Euler(180f, 0f, 0f), Time.deltaTime * smooth);
            Physics.gravity = new Vector3(0f, 9.8f, 0f);
            turnCamUpsidedown.rotation = Quaternion.Slerp(turnCamUpsidedown.rotation, Quaternion.Euler(180f, 0f, 0f), Time.deltaTime * smooth);
        }
        else
        {
            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, Quaternion.Euler(0f, 0f, 0f), Time.deltaTime * smooth);
            Physics.gravity = new Vector3(0f, -9.8f, 0f);
            turnCamUpsidedown.rotation = Quaternion.Slerp(turnCamUpsidedown.rotation, Quaternion.Euler(0f, 0f, 0f), Time.deltaTime * smooth);
        }
    }

    void ToggleAntiGravity(InputAction.CallbackContext context)
    {
        if (playerMovement.isOnGround)
        {
            gravitySwitch = !gravitySwitch;
        }
    }
}