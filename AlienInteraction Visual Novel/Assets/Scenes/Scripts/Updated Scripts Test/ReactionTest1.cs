using UnityEngine;

public class ReactionTest1 : MonoBehaviour, IInteractionable
{
    public GameStateData gameStateData;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {

    }

    public void TurnItGreen()
    {
        int layerIndex = LayerMask.NameToLayer("TapInteractable");
        Debug.Log($"Layer index for 'TapInteractable': {layerIndex}");

        if (layerIndex != -1)
        {
            gameObject.layer = layerIndex;
            Debug.Log($"Set {gameObject.name} to layer {layerIndex}");
        }
        else
        {
            Debug.LogWarning("Layer 'TapInteractable' not found! Did you create it?");
        }
    }

    public void Interact()
    {
        if (gameStateData.doneClickingBlock1)
        {
            Debug.Log("Interacted to complete this");
            gameObject.layer = LayerMask.NameToLayer("NonInteractable");
        }
    }
}