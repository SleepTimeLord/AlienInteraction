using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogue", menuName = "Dialogue System/Dialogue")]
public class Dialogue : ScriptableObject
{
    [TextArea(3, 10)]
    public string dialogueText;   // The text to display in the dialogue
    public DialogueChoice[] choices; // Possible choices after this dialogue
    public Dialogue nextDialogue;    // Dialogue after this one

    [Header("Task Settings")]
    public string taskToAdd;       // ID of the task to add (leave blank if not applicable)
    public string taskToComplete;  // ID of the task to complete (leave blank if not applicable)
    public bool resetTaskList;     // reset all tasks on the list

    [Header("Interaction Settings")]
    public bool saveProgress = true;       // Whether to save progress for this dialogue
    public bool disableInteractionAfter;   // Disable interaction with NPC after this dialogue
    public bool resetInteraction;          // Reset interaction state when this dialogue is reached
    public bool enableInteraction;         // Enable interaction with the NPC
}

[System.Serializable]
public class DialogueChoice
{
    public string choiceText;      // The text displayed for the choice
    public Dialogue nextDialogue;  // The next dialogue after this choice is made
}

