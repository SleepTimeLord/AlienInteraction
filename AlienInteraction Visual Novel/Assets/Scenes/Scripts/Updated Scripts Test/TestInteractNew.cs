using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteractNew : MonoBehaviour, IInteractionable
{
    public GameStateData gameStateData;

    public GameEvent testEvent1;

    private void Start()
    {
        if (!gameStateData.doneClickingBlock1)
        {
            gameObject.layer = LayerMask.NameToLayer("TapInteractable");
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer("NonInteractable");
        }
    }
    public void Interact()
    {
        Debug.Log("Player action activated.");
        gameObject.layer = LayerMask.NameToLayer("NonInteractable");
        testEvent1.Raise();
    }
}
