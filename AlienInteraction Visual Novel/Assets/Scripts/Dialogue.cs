using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogue", menuName = "Dialogue System/Dialogue")]
public class Dialogue : ScriptableObject
{
    [TextArea(3, 10)] public string dialogueText;   // The text to display in the dialogue
    public DialogueChoice[] choices;               // Possible choices after this dialogue
    public Dialogue nextDialogue;                  // Dialogue after this one
}

[System.Serializable]
public class DialogueChoice
{
    public string choiceText;  // The text displayed for the choice
    public Dialogue nextDialogue;  // The next dialogue after this choice is made
}

