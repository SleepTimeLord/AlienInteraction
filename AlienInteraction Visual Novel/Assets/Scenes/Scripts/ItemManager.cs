using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] public GameObject itemCurrentlyHeld;
    [SerializeField] public GameObject itemCurrentlyInspected;
    public InspectableObject[] items;
    public GameObject[] itemsHoldable;
    public PickUpItem[] itemsInspectable;
    public GameObject[] itemsInspect;
    private PickUpItem pickUpItem;

    private void Start()
    {
        //itemsInspectable = FindObjectsByType<PickUpItem>(FindObjectsSortMode.None);

        // use this script for next time
        itemsInspect = GameObject.FindGameObjectsWithTag("Inspectable");
        print(itemsInspect.Length);
    }

    public void EquipItem(GameObject equipItem)
    {
        bool itemFound = false;

/*        foreach (InspectableObject item in items)
        {
            // Deactivate all items in holdable slot and activate all items 
            item.holdable.SetActive(false);
            item.inspectable.SetActive(true);

            if (item.holdable == equipItem)
            {
                //Debug.Log("detected objects as " + itemCurrentlyHeld);
                itemCurrentlyHeld = item.holdable;
                item.holdable.SetActive(true);
                item.inspectable.SetActive(false);
                itemFound = true;
            }
            if (item.holdable.activeSelf)
            {
                Debug.Log("this holdable object was activated" + itemCurrentlyHeld);
            }
        }*/
/*
        foreach (GameObject holdable in itemsHoldable)
        {
            holdable.SetActive(false);

            if (holdable == equipItem)
            {
                itemCurrentlyInspected = itemsInspectable[System.Array.IndexOf(itemsHoldable, holdable)];
            }
        }*/

        if (!itemFound)
        {
            Debug.LogWarning("Item is not in the Obj Holder");
        }
    }

    public void UnequipItem()
    {
        itemCurrentlyHeld.SetActive(false);
        itemCurrentlyHeld=null;
    }
}