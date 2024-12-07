using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveItem : MonoBehaviour, IInteractionable
{
    public ItemManager itemManager;
    // Start is called before the first frame update
    public void Interact()
    {
        if (itemManager != null)
        {
            itemManager.itemCurrentlyHeld.SetActive(false);
            itemManager.itemCurrentlyHeld = null;
            Debug.Log("removed" +  itemManager.itemCurrentlyHeld);
        }
        else
        {
            Debug.LogWarning("Reference the ItemManager Script");
        }
    }
}
