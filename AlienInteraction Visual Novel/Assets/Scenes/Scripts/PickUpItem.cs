using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour, IInteractionable
{
    public GameObject correspondingHoldable;
    public ItemManager itemManager;
    public void Interact()
    {
        if (itemManager != null)
        {
            /*            gameObject.SetActive(false);
                        enablePlayerObject.SetActive(true);*/

            itemManager.EquipItem(correspondingHoldable);
            //itemManager.itemCurrentlyHeld = enablePlayerObject;
        }
        else
        {
            Debug.LogWarning("Reference the ItemManager Script");
        }
    }
}