using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class EatFood : MonoBehaviour
{
    ItemManager itemManager;
    Day1Script day1Script;
    // Start is called before the first frame update
    void Start()
    {
        itemManager = FindObjectOfType<ItemManager>();
        day1Script = FindObjectOfType<Day1Script>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E) && itemManager.itemCurrentlyHeld == day1Script.foodDay1Cooked)
        {
            StoryManager.Instance.MarkProgressCompleted("Eat_Food1");
            itemManager.UnequipItem();
        }

        if (Input.GetKeyDown(KeyCode.E) && itemManager.itemCurrentlyHeld == day1Script.cookedSwedishMeatballs)
        {
            StoryManager.Instance.MarkProgressCompleted("Eat_Swedish_Meatball");
            itemManager.UnequipItem();
        }
    }
}
