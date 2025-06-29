using Unity.Cinemachine;
using UnityEngine;

public class StopMovement : MonoBehaviour
{
    private ItemManager itemManager;

    private PlayerMovement playerMovement;
    private AntiGravity antiGravity;
    public CinemachineInputAxisController playerFPHeadMovement;
    public CinemachineBasicMultiChannelPerlin playerHeadBob;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        itemManager = FindAnyObjectByType<ItemManager>();
        playerMovement = FindAnyObjectByType<PlayerMovement>();
        antiGravity = FindAnyObjectByType<AntiGravity>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (itemManager != null)
        {

            if (!itemManager.isInPickUpMode)
            {
                playerMovement.StartPlayerMovement();
                antiGravity.GravitySwitch();
                playerFPHeadMovement.enabled = true;
                playerHeadBob.enabled = true;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                playerFPHeadMovement.enabled = false;
                playerHeadBob.enabled = false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }
}
