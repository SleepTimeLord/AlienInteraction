using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [Header("UI Components")]
    public TextMeshProUGUI dialogueText;   // The text field for dialogue
    public GameObject choicesContainer;    // Container for choice buttons
    public Button choiceButtonPrefab;      // Button prefab for dialogue choices
    public GameObject dialogueUI;          // UI panel to show/hide dialogue

    private Dialogue currentDialogue;      // The current dialogue being displayed
    private bool isDialogueActive = false; // Tracks if dialogue is ongoing
    private NPC currentNPC;                // Reference to the NPC that initiated the dialogue

    private void Update()
    {
        // Advance dialogue with Spacebar if no choices exist
        if (isDialogueActive && Input.GetKeyDown(KeyCode.Space) && currentDialogue.choices.Length == 0)
        {
            if (currentDialogue.nextDialogue != null)
            {
                StartDialogue(currentDialogue.nextDialogue, currentNPC); // Pass NPC context
            }
            else
            {
                EndDialogue();
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

        // Clear previous choices
        foreach (Transform child in choicesContainer.transform)
        {
            Destroy(child.gameObject);
        }

        // Create buttons for choices
        if (currentDialogue.choices.Length > 0)
        {
            foreach (var choice in currentDialogue.choices)
            {
                Button choiceButton = Instantiate(choiceButtonPrefab, choicesContainer.transform);
                choiceButton.GetComponentInChildren<TextMeshProUGUI>().text = choice.choiceText;
                choiceButton.onClick.AddListener(() => ChooseDialogue(choice));
            }
        }
    }

    private void ChooseDialogue(DialogueChoice choice)
    {
        if (choice.nextDialogue != null)
        {
            // Save progress to the NPC
            if (currentNPC != null)
            {
                currentNPC.SaveProgress(choice.nextDialogue);
            }

            StartDialogue(choice.nextDialogue, currentNPC);  // Pass both next dialogue and NPC
        }
        else
        {
            EndDialogue();
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


