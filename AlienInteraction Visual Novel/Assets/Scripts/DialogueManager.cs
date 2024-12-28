using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    [Header("UI Components")]
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI dialogueName;
    public TextMeshProUGUI choice1Text;
    public TextMeshProUGUI choice2Text;
    public GameObject dialogueUI;

    [Header("Typewriter Effect")]
    public float typewriterSpeed = 0.05f; // Delay between each character

    private Dialogue currentDialogue;
    private bool isDialogueActive = false;
    private NPC currentNPC; // Optional reference to the NPC that initiated the dialogue
    private Coroutine typewriterCoroutine;
    private bool isSkippingTypewriter = false;

    private void Update()
    {
        if (isDialogueActive)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (typewriterCoroutine != null)
                {
                    isSkippingTypewriter = true;
                }
                else if (currentDialogue.choices.Length == 0)
                {
                    AdvanceDialogue();
                }
            }

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

    public void StartDialogue(Dialogue dialogue)
    {
        currentDialogue = dialogue;
        currentNPC = null; // No NPC associated
        isDialogueActive = true;
        dialogueUI.SetActive(true);
        DisplayDialogue();
    }

    public void StartDialogue(Dialogue dialogue, NPC npc)
    {
        currentDialogue = dialogue;
        currentNPC = npc; // Associate NPC
        isDialogueActive = true;
        dialogueUI.SetActive(true);
        DisplayDialogue();
    }

    private void DisplayDialogue()
    {
        if (currentDialogue == null) return;

        if (typewriterCoroutine != null)
        {
            StopCoroutine(typewriterCoroutine);
        }

        typewriterCoroutine = StartCoroutine(TypeText(currentDialogue.dialogueText));

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
                choice2Text.gameObject.SetActive(false);
            }
        }
        else
        {
            choice1Text.gameObject.SetActive(false);
            choice2Text.gameObject.SetActive(false);
        }

        if (!string.IsNullOrEmpty(currentDialogue.dialogueName))
        {
            dialogueName.text = currentDialogue.dialogueName;
            dialogueName.gameObject.SetActive(true);
        }
        else
        {
            dialogueName.gameObject.SetActive(false);
        }
    }

    private IEnumerator TypeText(string text)
    {
        dialogueText.text = "";
        isSkippingTypewriter = false;

        foreach (char c in text)
        {
            if (isSkippingTypewriter)
            {
                dialogueText.text = text;
                break;
            }

            dialogueText.text += c;
            yield return new WaitForSeconds(typewriterSpeed);
        }

        typewriterCoroutine = null;
    }

    private void ChooseDialogue(DialogueChoice choice)
    {
        if (choice.nextDialogue != null)
        {
            HandleInteractionSettings(choice.nextDialogue);
            HandleTaskSettings(choice.nextDialogue);

            if (choice.nextDialogue.saveProgress && !string.IsNullOrEmpty(choice.nextDialogue.progressID))
            {
                StoryManager.Instance.MarkProgressCompleted(choice.nextDialogue.progressID);
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
        HandleInteractionSettings(currentDialogue);
        HandleTaskSettings(currentDialogue);

        if (!string.IsNullOrEmpty(currentDialogue.progressID))
        {
            StoryManager.Instance.MarkProgressCompleted(currentDialogue.progressID);
        }
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

        if (dialogue.resetTaskList)
        {
            TaskManager.ResetAllTasks();
        }
    }

    private void EndDialogue()
    {
        isDialogueActive = false;
        dialogueUI.SetActive(false);
        currentNPC = null;
    }

    public bool IsDialogueActive()
    {
        return isDialogueActive;
    }
}