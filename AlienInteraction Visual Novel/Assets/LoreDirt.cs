using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoreDirt : MonoBehaviour, IInteractionable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Interact()
    {
        gameObject.SetActive(false);
        StoryManager.Instance.MarkProgressCompleted("Cleaning_Lore1");
    }
}
