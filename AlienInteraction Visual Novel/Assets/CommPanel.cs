using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommPanel : MonoBehaviour, IInteractionable
{
    // Start is called before the first frame update
    public void Interact()
    {
        gameObject.layer = LayerMask.NameToLayer("NonInteractable");
        StoryManager.Instance.MarkProgressCompleted("Done_Checking_Comms");
    }
}
