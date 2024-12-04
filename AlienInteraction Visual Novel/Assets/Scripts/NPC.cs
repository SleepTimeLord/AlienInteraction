using UnityEngine;

public class NPC : MonoBehaviour, IInteractionable
{
    [Header("Dialogue Settings")]
    public Dialogue initialDialogue;       // The starting dialogue for this NPC
    public Dialogue afterTaskDialogue;     // Dialogue after the task is complete
    public Dialogue defaultDialogue;       // Default dialogue after the NPC has been fully interacted with

    [Header("Interaction Settings")]
    public bool disableInteractionAfterTalk = false; // Option to lose interaction after talking
    public string interactionDisabledLayer = "NonInteractable"; // Layer to switch to if interaction is disabled
    public string interactionEnabledLayer = "Interactable";     // Layer to switch to if interaction is re-enabled

    [Header("Save Settings")]
    public bool saveProgress = true;       // Determines if progress is saved for this NPC

    private Dialogue savedDialogue;        // Tracks progress for this NPC
    private bool hasBeenInteractedWith = false; // Tracks if the NPC has been interacted with

    private void Start()
    {
        savedDialogue = initialDialogue; // Start with the initial dialogue
    }

    public void Interact()
    {
        DialogueManager dialogueManager = FindObjectOfType<DialogueManager>();
        if (dialogueManager != null && !dialogueManager.IsDialogueActive())
        {
            // Check if task is completed and update dialogue accordingly
            if (TaskManager.IsTaskCompleted("SomeTaskID"))
            {
                savedDialogue = afterTaskDialogue; // Switch to after-task dialogue
            }
            else if (hasBeenInteractedWith)
            {
                savedDialogue = defaultDialogue; // Use default dialogue if already interacted with
            }

            dialogueManager.StartDialogue(savedDialogue, this);

            // Mark as interacted with
            hasBeenInteractedWith = true;

            // Handle disabling interaction after talking
            if (disableInteractionAfterTalk)
            {
                DisableInteraction();
            }
        }
    }

    public void SaveProgress(Dialogue newDialogue)
    {
        if (saveProgress)
        {
            savedDialogue = newDialogue; // Update saved dialogue only if saving is enabled
        }
    }

    // Call this method when the task is completed
    public void CompleteTask()
    {
        TaskManager.CompleteTask("SomeTaskID"); // Mark task as completed
        savedDialogue = afterTaskDialogue; // Update dialogue after task completion
    }

    // Disable further interaction by changing the layer
    public void DisableInteraction()
    {
        print("applied");
        gameObject.layer = LayerMask.NameToLayer(interactionDisabledLayer);
    }

    // Enable interaction by resetting the layer
    public void EnableInteraction()
    {
        gameObject.layer = LayerMask.NameToLayer(interactionEnabledLayer);
    }

    // Reset the NPC to its initial state
    public void ResetNPC()
    {
        savedDialogue = initialDialogue;
        hasBeenInteractedWith = false;

        // Restore interaction by resetting the layer
        EnableInteraction();
    }
}


