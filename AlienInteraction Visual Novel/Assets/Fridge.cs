using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fridge : MonoBehaviour, IInteractionable
{
    ItemManager itemManager;
    public GameObject currentFood;

    public void Interact()
    {
        if (currentFood == null)
        {
            Debug.Log("no food referenced");
        }
        else
        {
            GetFoodFromFridge(currentFood);
            gameObject.layer = LayerMask.NameToLayer("NonInteractable");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        itemManager = FindObjectOfType<ItemManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetFoodFromFridge(GameObject food)
    {
        if (itemManager == null)
        {
            Debug.Log("Cannot fin the script ItemManager");
        }
        else
        {
            itemManager.EquipItem(food);
        }

    }
}
