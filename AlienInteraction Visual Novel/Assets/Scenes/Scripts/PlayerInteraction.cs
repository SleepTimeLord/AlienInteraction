using System.Runtime.CompilerServices;
using System.Xml.Serialization;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    public Camera playerCamera;        // Assign the player camera
    public float interactionRange = 5f; // Max distance for interaction
    public LayerMask tapInteractableLayer; // Layer for tap interactable objects
    public LayerMask holdInteractableLayer; // Layer for hold interactable objects
    public bool isHolding;
    public GameObject hitObject;
    public float holdingTime = 0f;
    public float maxHold = 5f;
    public LayerMask ignoredLayers;

    PlayerController playerController;

    private void OnEnable()
    {
        playerController.PlayerControls.Interact.started += InteractTap;
        playerController.Enable();
    }
    private void OnDisable()
    {
        playerController.PlayerControls.Interact.started -= InteractTap;
        playerController.Disable();
    }
    private void Awake()
    {
        playerController = new PlayerController();
        playerController.PlayerControls.Interact.performed += PlayerHolding;
        playerController.PlayerControls.Interact.canceled += PlayerHoldCancelled;
    }

    void PlayerHolding(InputAction.CallbackContext context)
    {
        isHolding = true;
    }

    void PlayerHoldCancelled(InputAction.CallbackContext context)
    {
        holdingTime = 0;
        isHolding = false;
    }

    void Update()
    {
        hitObject = ObjectChecker();
        ObjectChecker();

        if (isHolding)
        {
            holdingTime += Time.deltaTime;

            if (hitObject != null)
            {
                if (((1 << hitObject.layer) & holdInteractableLayer.value) != 0)
                {
                    IInteractionable interactable = hitObject.GetComponent<IInteractionable>();
                    if (interactable != null)
                    {
                        if (holdingTime >= maxHold)
                        {
                            Debug.Log("hi");
                            holdingTime = 0;
                            interactable.Interact();
                        }
                    }
                    else
                    {
                        Debug.Log("Hit an object, but it doesn't have HoldInteract.");
                    }
                }
            }
            else
            {
                holdingTime = 0;
            }
        }
    }

    public GameObject ObjectChecker()
    {
        if (playerCamera == null)
        {
            Debug.LogError("Player camera is not assigned!");
        }

        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, interactionRange, ~ignoredLayers))
        {
            return hit.collider.gameObject;
        }
        
        return null;
    }

    void InteractTap(InputAction.CallbackContext context)
    {
        isHolding = true;

        if (hitObject != null)
        {
            if (((1 << hitObject.layer) & tapInteractableLayer.value) != 0)
            {
                IInteractionable interactable = hitObject.GetComponent<IInteractionable>();
                if (interactable != null)
                {
                    interactable.Interact();
                }
                else
                {
                    Debug.Log("Hit an object, but it doesn't implement IInteractable.");
                }
            }
            else
            {
                Debug.Log("Hit an object, but it doesn't have Tap Interact.");
            }
        }
    } 
}
