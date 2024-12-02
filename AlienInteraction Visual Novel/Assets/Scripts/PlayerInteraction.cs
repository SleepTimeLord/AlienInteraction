using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public Camera playerCamera;        // Assign the player camera
    public float interactionRange = 5f; // Max distance for interaction
    public LayerMask interactableLayer; // Layer for interactable objects

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CastInteractionRay();
        }
    }

    void CastInteractionRay()
    {
        if (playerCamera == null)
        {
            Debug.LogError("Player camera is not assigned!");
            return;
        }

        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, interactionRange, interactableLayer))
        {
            // Check if the object implements IInteractable
            IInteractionable interactable = hit.collider.GetComponent<IInteractionable>();
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
            Debug.Log("Raycast didn't hit anything interactable.");
        }
    }
}
