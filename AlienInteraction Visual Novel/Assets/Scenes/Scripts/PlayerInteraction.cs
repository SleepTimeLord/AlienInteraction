using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using UnityEditor.Experimental.GraphView;
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
                if (hitObject.layer == 7)
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
        }
    }

    public GameObject ObjectChecker()
    {
        if (playerCamera == null)
        {
            Debug.LogError("Player camera is not assigned!");
        }

        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, interactionRange))
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
            if (hitObject.layer == 3) 
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
    void ShowTextForInteract()
    {
        // this is a template to indicate if i should show text on the screen to do something
        return;
    }


/*    public void CastInteractionRay()
    {
        if (playerCamera == null)
        {
            Debug.LogError("Player camera is not assigned!");
            return;
        }

        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, interactionRange, tapInteractableLayer))
        {
            // Check if the object implements IInteractable
            IInteractionable interactable = hit.collider.GetComponent<IInteractionable>();
            if (interactable != null)
            {
                //isHolding = true;
                interactable.Interact();
            }
            else
            {
                Debug.Log("Hit an object, but it doesn't implement IInteractable.");
            }
        }
        else
        {
            Debug.Log("Raycast didn't hit anything interactable.");
        }
    }*/
}
