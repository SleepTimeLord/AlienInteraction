using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] public GameObject itemCurrentlyHeld;
    public GameObject[] items;

    private void Start()
    {
        foreach (GameObject item in items)
        {
            Debug.Log("Item in list: " + item.name);
        }
    }

    public void EquipItem(GameObject equipItem)
    {
        bool itemFound = false;

        foreach (GameObject child in items)
        {
            // Deactivate all items
            child.SetActive(false);

            if (child == equipItem)
            {
                itemCurrentlyHeld = child;
                child.SetActive(true);
                itemFound = true;
            }
        }

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