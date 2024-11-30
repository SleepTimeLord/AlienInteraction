using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public Camera playerCamera; 
    public float interactionRange = 5f; 
    public LayerMask interactableLayer; 

    void Update()
    {
        // Check for interaction input
        if (Input.GetKeyDown(KeyCode.E))
        {
            CastInteractionRay();
        }
    }

    void CastInteractionRay()
    {
        // Raycast from the camera through the mouse position
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);

        // Perform the raycast
        if (Physics.Raycast(ray, out RaycastHit hit, interactionRange, interactableLayer))
        {
            // Check if the object has the IInteractable interface
            IInteractionable interactable = hit.collider.GetComponent<IInteractionable>();
            if (interactable != null)
            {
                interactable.Interact();
            }
            else
            {
                Debug.LogWarning("did not interact with anything");
            }
        }
    }
}
