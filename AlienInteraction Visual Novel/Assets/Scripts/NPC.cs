using UnityEngine;

public class NPC : MonoBehaviour, IInteractionable
{
    [Header("Dialogue Settings")]
    public Dialogue initialDialogue;
    public Dialogue afterTaskDialogue;
    public Dialogue defaultDialogue;

    [Header("Interaction Settings")]
    public bool disableInteractionAfterTalk = false;
    public string interactionDisabledLayer = "NonInteractable";
    public string interactionEnabledLayer = "Interactable";
    public string completeProgress;

    private Dialogue savedDialogue;
    private bool hasBeenInteractedWith = false;

    private void Start()
    {
        savedDialogue = initialDialogue;
    }

    public void Interact()
    {
        DialogueManager dialogueManager = FindObjectOfType<DialogueManager>();
        if (dialogueManager != null && !dialogueManager.IsDialogueActive())
        {
            Dialogue dialogueToStart = savedDialogue;

            if (TaskManager.IsTaskCompleted("SomeTaskID"))
            {
                dialogueToStart = afterTaskDialogue;
            }
            else if (hasBeenInteractedWith)
            {
                dialogueToStart = defaultDialogue;
            }

            dialogueManager.StartDialogue(dialogueToStart, this);
            hasBeenInteractedWith = true;

            if (disableInteractionAfterTalk)
            {
                DisableInteraction();
            }

            if (completeProgress != null)
            {
                CompleteProgress();
            }
        }
    }

    public void DisableInteraction()
    {
        gameObject.layer = LayerMask.NameToLayer(interactionDisabledLayer);
    }

    public void EnableInteraction()
    {
        gameObject.layer = LayerMask.NameToLayer(interactionEnabledLayer);
    }

    public void ResetNPC()
    {
        savedDialogue = initialDialogue;
        hasBeenInteractedWith = false;
        EnableInteraction();
    }

    public void CompleteProgress()
    {
        StoryManager.Instance.MarkProgressCompleted(completeProgress);
    }
}



