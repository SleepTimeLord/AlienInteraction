using UnityEngine;

public class DialogueStarter : MonoBehaviour
{
    public Dialogue dialogueStartTrigger;
    public bool triggerOnStart = true;
    private bool completedPrologue = false;
    private DialogueManager dialogueManager;
    private ChangeScene changeScene;

    private void Start()
    {
        changeScene = FindObjectOfType<ChangeScene>();
        dialogueManager = FindObjectOfType<DialogueManager>();
        if (triggerOnStart && dialogueStartTrigger != null)
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
            Debug.Log("successfully Destroyed");
        }
    }

    public void StartDialogue()
    {
        if (StoryManager.Instance == null)
        {
            Debug.LogError("StoryManager instance is not initialized.");
            return;
        }

        if (!StoryManager.Instance.IsProgressCompleted("Completed_Prologue"))
        {
            dialogueManager?.StartDialogue(dialogueStartTrigger);
        }
    }

    private void OnProgressUpdated(string progressKey)
    {
        if (progressKey == "Completed_Prologue" && !completedPrologue)
        {
            FinishDialogueScene();
        }
    }

    public void FinishDialogueScene()
    {
        Debug.Log($"Finished prologue scene, now moving to part 1");
        completedPrologue = true;
        changeScene?.ChangeToNextScene();
    }
}


