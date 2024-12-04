using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [Header("UI Components")]
    public TextMeshProUGUI dialogueText;  // Main dialogue text
    public TextMeshProUGUI choice1Text;  // Text for the first choice
    public TextMeshProUGUI choice2Text;  // Text for the second choice
    public GameObject dialogueUI;        // UI panel to show/hide dialogue

    private Dialogue currentDialogue;    // The current dialogue being displayed
    private bool isDialogueActive = false; // Tracks if dialogue is ongoing
    private NPC currentNPC;              // Reference to the NPC that initiated the dialogue

    private void Update()
    {
        if (isDialogueActive)
        {
            // Advance dialogue with Spacebar if no choices exist
            if (Input.GetKeyDown(KeyCode.Space) && currentDialogue.choices.Length == 0)
            {
                AdvanceDialogue();
            }

            // Handle choice selection with number keys
            if (currentDialogue.choices.Length > 0)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    ChooseDialogue(currentDialogue.choices[0]);
                }
                else if (currentDialogue.choices.Length > 1 && Input.GetKeyDown(KeyCode.Alpha2))
                {
                    ChooseDialogue(currentDialogue.choices[1]);
                }
            }
        }
    }

    public void StartDialogue(Dialogue dialogue, NPC npc)
    {
        currentDialogue = dialogue;
        currentNPC = npc; // Track the NPC who started this dialogue
        isDialogueActive = true;
        dialogueUI.SetActive(true); // Show the dialogue UI
        DisplayDialogue();
    }

    private void DisplayDialogue()
    {
        if (currentDialogue == null) return;

        dialogueText.text = currentDialogue.dialogueText;

        // Update choice texts
        if (currentDialogue.choices.Length > 0)
        {
            choice1Text.text = $"1. {currentDialogue.choices[0].choiceText}";
            choice1Text.gameObject.SetActive(true);

            if (currentDialogue.choices.Length > 1)
            {
                choice2Text.text = $"2. {currentDialogue.choices[1].choiceText}";
                choice2Text.gameObject.SetActive(true);
            }
            else
            {
                choice2Text.gameObject.SetActive(false); // Hide if no second choice
            }
        }
        else
        {
            // Hide choices if none exist
            choice1Text.gameObject.SetActive(false);
            choice2Text.gameObject.SetActive(false);
        }
    }

    private void ChooseDialogue(DialogueChoice choice)
    {
        if (choice.nextDialogue != null)
        {
            HandleInteractionSettings(choice.nextDialogue);
            HandleTaskSettings(choice.nextDialogue);

            if (currentNPC != null && choice.nextDialogue.saveProgress)
            {
                currentNPC.SaveProgress(choice.nextDialogue); // Save progress to the NPC
            }

            StartDialogue(choice.nextDialogue, currentNPC);
        }
        else
        {
            EndDialogue();
        }
    }

    private void AdvanceDialogue()
    {
        // handles all the settings before advancing to next dialogue
        HandleInteractionSettings(currentDialogue);
        HandleTaskSettings(currentDialogue);

        if (currentNPC != null && currentDialogue.saveProgress)
        {
            currentNPC.SaveProgress(currentDialogue);
        }

        // goes to next dialogue and ends current dialogue
        if (currentDialogue.nextDialogue != null)
        {
            StartDialogue(currentDialogue.nextDialogue, currentNPC);
        }
        else
        {
            EndDialogue();
        }
    }

    private void HandleInteractionSettings(Dialogue dialogue)
    {
        if (currentNPC == null) return;

        if (dialogue.disableInteractionAfter)
        {
            currentNPC.DisableInteraction();
        }
        else if (dialogue.enableInteraction)
        {
            currentNPC.EnableInteraction();
        }
        else if (dialogue.resetInteraction)
        {
            currentNPC.ResetNPC();
        }
    }

    private void HandleTaskSettings(Dialogue dialogue)
    {
        if (!string.IsNullOrEmpty(dialogue.taskToAdd))
        {
            TaskManager.AddTask(dialogue.taskToAdd);
        }

        if (!string.IsNullOrEmpty(dialogue.taskToComplete))
        {
            TaskManager.CompleteTask(dialogue.taskToComplete);
        }

        if (dialogue.resetTaskList) // Example of triggering reset on a specific dialogue flag
        {
            TaskManager.ResetAllTasks();
        }
    }


    private void EndDialogue()
    {
        isDialogueActive = false;
        dialogueUI.SetActive(false); // Hide the dialogue UI

        // Clear NPC reference to avoid accidental continuation
        currentNPC = null;
    }

    public bool IsDialogueActive()
    {
        return isDialogueActive;
    }
}


