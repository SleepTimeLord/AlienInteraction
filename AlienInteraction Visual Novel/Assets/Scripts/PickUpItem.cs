using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour, IInteractionable
{
    public GameObject enablePlayerObject;
    public ItemManager itemManager;
    public void Interact()
    {
        if (itemManager != null)
        {
            Destroy(gameObject);
            enablePlayerObject.SetActive(true);

            itemManager.itemCurrentlyHeld = enablePlayerObject;
        }
        else
        {
            Debug.LogWarning("Reference the ItemManager Script");
        }
    }
}