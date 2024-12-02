using UnityEngine;

public class NPC : MonoBehaviour, IInteractionable
{
    public Dialogue initialDialogue;   // The starting dialogue for this NPC
    public Dialogue afterTaskDialogue; // Dialogue after the task is complete
    private Dialogue savedDialogue;    // Tracks progress for this NPC

    private void Start()
    {
        savedDialogue = initialDialogue; // Start with the initial dialogue
    }

    public void Interact()
    {
        DialogueManager dialogueManager = FindObjectOfType<DialogueManager>();
        if (dialogueManager != null && !dialogueManager.IsDialogueActive())
        {
            // Check task completion status
            if (TaskManager.IsTaskCompleted("SomeTaskID"))
            {
                savedDialogue = afterTaskDialogue; // Switch to after-task dialogue
            }
            dialogueManager.StartDialogue(savedDialogue, this);
        }
    }

    public void SaveProgress(Dialogue newDialogue)
    {
        savedDialogue = newDialogue; // Update saved dialogue to the new one
    }

    // Call this method when the task is completed
    public void CompleteTask()
    {
        TaskManager.CompleteTask("SomeTaskID"); // Mark task as completed
        savedDialogue = afterTaskDialogue; // Update dialogue after task completion
    }
}

