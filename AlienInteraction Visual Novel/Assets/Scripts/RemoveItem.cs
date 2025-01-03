using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class RemoveItem : MonoBehaviour, IInteractionable
{
    public ItemManager itemManager;
    private Day1Script day1Script;
    public GameObject microwaveScreen;
    private Renderer rd;
    public bool microwaving = false;
    private bool doneMicrowaving = false;
    private GameObject itemCurrentlyGettingMicrowaved;

    public string interactionDisabledLayer = "NonInteractable";
    public string interactionEnabledLayer = "Interactable";
    // Start is called before the first frame update
    private void Start()
    {
        rd = microwaveScreen.GetComponent<Renderer>();
        rd.material.color = Color.green;
        day1Script = FindObjectOfType<Day1Script>();
    }

    private void Update()
    {
 
    }
    public void Interact()
    {
        if (itemManager.itemCurrentlyHeld != null)
        {
            if (StoryManager.Instance == null)
            {
                Debug.LogWarning("StoryManager not Intialized");
            }
            else
            {
                Debug.Log("activated");
                itemCurrentlyGettingMicrowaved = itemManager.itemCurrentlyHeld;
                itemManager.UnequipItem();
                StartCoroutine(MicrowavingFood(5f));
            }
        }

        if (doneMicrowaving && itemManager.itemCurrentlyHeld == null)
        {
            if (itemCurrentlyGettingMicrowaved == day1Script.foodDay1Uncooked)
            {
                itemManager.EquipItem(day1Script.foodDay1Cooked);
                doneMicrowaving = false;
                StoryManager.Instance.MarkProgressCompleted("Cooking1_Complete");
            }

            if (itemCurrentlyGettingMicrowaved == day1Script.uncookedSwedishMeatballs)
            {
                itemManager.EquipItem(day1Script.cookedSwedishMeatballs);
                doneMicrowaving = false;
                StoryManager.Instance.MarkProgressCompleted("Done_Cooking_Swedish_Meatball");
            }

            if (itemCurrentlyGettingMicrowaved == day1Script.foodDay2Uncooked)
            {
                itemManager.EquipItem(day1Script.foodDay2Cooked);
                doneMicrowaving = false;
                StoryManager.Instance.MarkProgressCompleted("Cooking2_Complete");
            }
        }
    }

    IEnumerator MicrowavingFood(float timeMicrowaved)
    {
        microwaving = true;
        rd.material.color = Color.red;
        gameObject.layer = LayerMask.NameToLayer(interactionDisabledLayer);
        yield return new WaitForSeconds(timeMicrowaved);
        
        doneMicrowaving = true;
        rd.material.color = Color.green;
        microwaving = false;
        gameObject.layer = LayerMask.NameToLayer(interactionEnabledLayer);
    }
}
