using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day1Script : MonoBehaviour
{
    public Dialogue dialogueStartTrigger;
    public bool triggerOnStart = true;
    private DialogueManager dialogueManager;

    [Header("General")]
    //public bool isConsoleLightOn;
    public int dirtCleaned = 0;
    public GameObject consoleIndicateOn;
    public GameObject fridge;
    public GameObject bed;
    public GameObject microwave;
    private Renderer consoleLight;

    [Header("Day 1")]
    public Dialogue dialoguePart2StartTrigger;
    public Dialogue dialoguePart3StartTrigger;
    public Dialogue dialoguePart4StartTrigger;
    public Dialogue dialoguePart5StartTrigger;
    public GameObject foodDay1Uncooked;
    public GameObject foodDay1Cooked;
    public GameObject dirtSpawn1;
    public GameObject pictureDirt;
    public GameObject uncookedSwedishMeatballs;
    public GameObject cookedSwedishMeatballs;
    public bool doneDay1;

    // Start is called before the first frame update
    void Start()
    {
        consoleLight = consoleIndicateOn.GetComponent<Renderer>();

        dialogueManager = FindObjectOfType<DialogueManager>();
        if (dialogueStartTrigger != null )
        {
            StartDialogue();
        }

        if (StoryManager.Instance != null)
        {
            // Subscribes to the OnProgressUpdated event to respond to progress updates.
            StoryManager.Instance.OnProgressUpdated += OnProgressUpdated;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartDialogue()
    {
        if (StoryManager.Instance == null)
        {
            Debug.LogError("StoryManager instance is not initialized.");
            return;
        }

        if (StoryManager.Instance.IsProgressCompleted("Completed_Prologue"))
        {
            dialogueManager?.StartDialogue(dialogueStartTrigger);
        }
        else
        {
            Debug.LogWarning("Player hasn't completed action");
        }
    }

    private void OnProgressUpdated(string progressKey)
    {
        if (progressKey == "Make_Breakfast1")
        {
            microwave.layer = LayerMask.NameToLayer("Interactable");
            fridge.layer = LayerMask.NameToLayer("Interactable");
            Fridge fridgeScript = fridge.GetComponent<Fridge>();
            fridgeScript.currentFood = foodDay1Uncooked;
        }

        if (progressKey == "Cooking1_Complete")
        {
            microwave.layer = LayerMask.NameToLayer("NonInteractable");
            TaskManager.CompleteTask("Make Breakfast");
            TaskManager.AddTask("Eat Breakfast");
        }

        if (progressKey == "Eat_Food1")
        {
            NPC npcScript = consoleIndicateOn.GetComponent<NPC>();
            //Renderer consoleLight = consoleIndicateOn.GetComponent<Renderer>();

            npcScript.EnableInteraction();
            dialogueManager?.StartDialogue(dialoguePart2StartTrigger);
            //consoleLight.material.color = Color.yellow;
            TaskManager.CompleteTask("Eat Breakfast");
        }

        if (progressKey == "Investigate_Console")
        {
            TaskManager.CompleteTask("Investigate Noise");
        }

        if (progressKey == "Light_Turn_On")
        {
            consoleLight.material.color = Color.yellow;
        }

        if (progressKey == "Done_Conversating1")
        {
            consoleLight.material.color = Color.grey;

            dirtSpawn1.SetActive(true);

        }

        if (progressKey == "Done_Cleaning1")
        {
            print("hello");
            dirtSpawn1.SetActive(false);
            pictureDirt.SetActive(true);
        }
        
        if (progressKey == "Cleaning_Lore1")
        {
            dialogueManager?.StartDialogue(dialoguePart3StartTrigger);
            TaskManager.CompleteTask("Clean");
        }

        if (progressKey == "Make_Meatballs")
        {
            fridge.layer = LayerMask.NameToLayer("Interactable");
            microwave.layer = LayerMask.NameToLayer("Interactable");
            Fridge fridgeScript = fridge.GetComponent<Fridge>();
            fridgeScript.currentFood = uncookedSwedishMeatballs;
        }

        if (progressKey == "Done_Cooking_Swedish_Meatball")
        {
            microwave.layer = LayerMask.NameToLayer("NonInteractable");
            TaskManager.CompleteTask("Make Swedish Meatballs");
            TaskManager.AddTask("Eat Swedish Meatballs");
        }

        if (progressKey == "Eat_Swedish_Meatball")
        {
            TaskManager.CompleteTask("Eat Swedish Meatballs");
            dialogueManager?.StartDialogue(dialoguePart4StartTrigger);
            consoleLight.material.color = Color.yellow;
        }

        if (progressKey == "End_Of_Com")
        {
            consoleLight.material.color = Color.grey;
        }

        if (progressKey == "Go_To_Bed")
        {
            bed.layer = LayerMask.NameToLayer("Interactable");
            doneDay1 = true;
        }

        if (progressKey == "Sleep_Talk1")
        {
            TaskManager.CompleteTask("Go To Bed");
            dialogueManager?.StartDialogue(dialoguePart5StartTrigger);
        }

        if (progressKey == "Done_Sleeping")
        {
            SleepReset sleepScript = bed.GetComponent<SleepReset>();

            sleepScript.sleeping = false;
            sleepScript.WakeUp();
            Debug.Log("Im Awake");
        }
    }
}
