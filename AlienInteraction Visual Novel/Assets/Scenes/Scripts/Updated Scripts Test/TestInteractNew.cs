using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteractNew : MonoBehaviour, IInteractionable
{
    public GameEvent testEvent1;
    public void Interact()
    {
        Debug.Log("Player action completed.");
        testEvent1.Raise();
    }
}
