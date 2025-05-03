using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour, IInteractionable
{
    public GameObject correspondingHoldable;
    private ItemManager itemManager;

    private void Start()
    {
        itemManager = FindAnyObjectByType<ItemManager>();
    }
    public void Interact()
    {
        if (itemManager != null)
        {
            if (itemManager.heldObj == null)
            {
                itemManager.pickedUpObject = transform.gameObject;
            }
        }
        else
        {
            Debug.LogWarning("Reference the ItemManager Script");
        }
    }
}