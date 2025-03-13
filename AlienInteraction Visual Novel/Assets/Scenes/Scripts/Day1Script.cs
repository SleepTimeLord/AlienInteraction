using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Day1Script : MonoBehaviour
{
    public Dialogue dialogueStartTrigger;
    public bool triggerOnStart = true;
    private DialogueManager dialogueManager;
    private ChangeScene changeScene;

    [Header("General")]
    //public bool isConsoleLightOn;
    public int dirtCleaned = 0;
    public int dirtCleaned2 = 0;
    public GameObject consoleIndicateOn;
    public GameObject consoleMic;
    public GameObject fridge;
    public GameObject bed;
    public GameObject microwave;
    public GameObject journeyScreen;
    private SpriteRenderer journeyScreenSR;
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
    public Sprite day1Screen;
    public bool doneDay1;

    [Header("Day 2")]
    public Dialogue dialoguePart1Day2StartTrigger;
    public Dialogue dialoguePart2Day2StartTrigger;
    public GameObject foodDay2Uncooked;
    public GameObject foodDay2Cooked;
    public GameObject dirtSpawn2;
    public GameObject posterDirt;
    public Sprite day2Screen;
    public bool doneDay2;

    [Header("Day 3")]
    public Dialogue dialoguePart1Day3StartTrigger;
    public Dialogue dialoguePart2Day3StartTrigger;
    public GameObject lastKey;
    public GameObject commPanel;
    public GameObject lever;
    public Sprite day3Screen;
    public bool yesRoute;
    //public bool noRoute;

    // Start is called before the first frame update
    void Start()
    {
        changeScene = FindAnyObjectByType<ChangeScene>();
        consoleLight = consoleMic.GetComponent<Renderer>();
        journeyScreenSR = journeyScreen.GetComponent<SpriteRenderer>();

        journeyScreenSR.sprite = day1Screen;

        dialogueManager = FindAnyObjectByType<DialogueManager>();
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

    private void OnDestroy()
    {
        if (StoryManager.Instance != null)
        {
            // Unsubscribes from the OnProgressUpdated event to prevent it from running when the object is destroyed or the scene changes.
            StoryManager.Instance.OnProgressUpdated -= OnProgressUpdated;
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
        switch (progressKey)
        {
            case "Make_Breakfast1":
                Debug.Log(foodDay1Uncooked);
                EnableFridgeAndMicrowave(foodDay1Uncooked);
                break;

            case "Cooking1_Complete":
                DisableMicrowave();
                TaskManager.CompleteTask("Make Breakfast");
                TaskManager.AddTask("Eat Breakfast");
                break;

            case "Eat_Food1":
                EnableNPCInteraction(consoleIndicateOn);
                dialogueManager?.StartDialogue(dialoguePart2StartTrigger);
                TaskManager.CompleteTask("Eat Breakfast");
                break;

            case "Investigate_Console":
                TaskManager.CompleteTask("Investigate Noise");
                break;

            case "Light_Turn_On":
                SetConsoleLightColor(Color.yellow);
                break;

            case "Done_Conversating1":
                SetConsoleLightColor(Color.gray);
                dirtSpawn1.SetActive(true);
                break;

            case "Done_Cleaning1":
                print("hello");
                dirtSpawn1.SetActive(false);
                pictureDirt.SetActive(true);
                break;

            case "Cleaning_Lore1":
                dialogueManager?.StartDialogue(dialoguePart3StartTrigger);
                TaskManager.CompleteTask("Clean");
                break;

            case "Make_Meatballs":
                EnableFridgeAndMicrowave(uncookedSwedishMeatballs);
                break;

            case "Done_Cooking_Swedish_Meatball":
                DisableMicrowave();
                TaskManager.CompleteTask("Make Swedish Meatballs");
                TaskManager.AddTask("Eat Swedish Meatballs");
                break;

            case "Eat_Swedish_Meatball":
                TaskManager.CompleteTask("Eat Swedish Meatballs");
                dialogueManager?.StartDialogue(dialoguePart4StartTrigger);
                SetConsoleLightColor(Color.yellow);
                break;

            case "End_Of_Com":
                SetConsoleLightColor(Color.gray);
                break;

            case "Go_To_Bed":
                bed.layer = LayerMask.NameToLayer("Interactable");
                doneDay1 = true;
                break;

            case "Sleep_Talk1":
                TaskManager.CompleteTask("Go To Bed");
                dialogueManager?.StartDialogue(dialoguePart5StartTrigger);
                break;

            case "Done_Sleeping":
                journeyScreenSR.sprite = day2Screen;
                ResetSleep(bed, foodDay2Uncooked);
                TaskManager.AddTask("Make Breakfast");
                EnableFridgeAndMicrowave(foodDay2Uncooked);
                break;

            case "Cooking2_Complete":
                DisableMicrowave();
                TaskManager.CompleteTask("Make Breakfast");
                TaskManager.AddTask("Eat Breakfast");
                break;

            case "Eat_Food2":
                TaskManager.CompleteTask("Eat Breakfast");
                SetConsoleLightColor(Color.yellow);
                dialogueManager?.StartDialogue(dialoguePart1Day2StartTrigger);
                break;

            case "Done_Convo2":
                SetConsoleLightColor(Color.gray);
                break;

            case "Start_Cleaning2":
                dirtSpawn2.SetActive(true);
                break;

            case "Done_Cleaning2":
                dirtSpawn2.SetActive(false);
                posterDirt.SetActive(true);
                break;

            case "Cleaning_Lore2":
                TaskManager.CompleteTask("Clean Ship");
                dialogueManager?.StartDialogue(dialoguePart2Day2StartTrigger);
                break;

            case "Comms_Alien_pt2":
                SetConsoleLightColor(Color.yellow);
                break;

            case "Comm_day2_pt2_done":
                SetConsoleLightColor(Color.gray);
                break;

            case "end_day_2":
                bed.layer = LayerMask.NameToLayer("Interactable");
                doneDay2 = true;
                break;

            case "Start_Day_3":
                journeyScreenSR.sprite = day3Screen;
                print("start of day 3");
                lastKey.SetActive(true);
                ResetSleep(bed, null);
                break;

            case "Get_Last_Key":
                dialogueManager?.StartDialogue(dialoguePart1Day3StartTrigger);
                break;

            case "Key_Pickup_Done":
                commPanel.layer = LayerMask.NameToLayer("Interactable");
                break;

            case "Done_Checking_Comms":
                TaskManager.CompleteTask("Open Comms Panel");
                dialogueManager?.StartDialogue(dialoguePart2Day3StartTrigger);
                break;

            case "Yes_Choice":
                TaskManager.AddTask("Pull the lever to Elysia");
                changeScene.sceneName = "YesChoice";
                lever.layer = LayerMask.NameToLayer("Interactable");
                yesRoute = true;
                break;

            case "No_Choice":
                TaskManager.AddTask("Pull the lever to the Space Station");
                lever.layer = LayerMask.NameToLayer("Interactable");
                changeScene.sceneName = "NoChoice";
                break;

            default:
                Debug.LogWarning($"Unhandled progressKey: {progressKey}");
                break;
        }
    }

    private void EnableFridgeAndMicrowave(GameObject food)
    {
        fridge.layer = LayerMask.NameToLayer("Interactable");
        microwave.layer = LayerMask.NameToLayer("Interactable");
        Fridge fridgeScript = fridge.GetComponent<Fridge>();
        fridgeScript.currentFood = food;
    }

    private void DisableMicrowave()
    {
        microwave.layer = LayerMask.NameToLayer("NonInteractable");
    }

    private void EnableNPCInteraction(GameObject npcObject)
    {
        NPC npcScript = npcObject.GetComponent<NPC>();
        npcScript.EnableInteraction();
    }

    private void SetConsoleLightColor(Color color)
    {
        consoleLight.material.color = color;
    }

    private void ResetSleep(GameObject bedObject, GameObject food)
    {
        SleepReset sleepScript = bedObject.GetComponent<SleepReset>();
        sleepScript.sleeping = false;
        sleepScript.WakeUp();
        if (food != null)
        {
            Fridge fridgeScript = fridge.GetComponent<Fridge>();
            fridgeScript.currentFood = food;
        }
    }
}
