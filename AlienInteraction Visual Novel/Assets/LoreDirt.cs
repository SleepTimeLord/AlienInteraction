using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoreDirt : MonoBehaviour, IInteractionable
{
    public bool isPart1;
    public bool isPart2;
    public bool isPart3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Interact()
    {
        gameObject.SetActive(false);
        if (isPart1)
        {
            StoryManager.Instance.MarkProgressCompleted("Cleaning_Lore1");
        }
        if (isPart2)
        {
            StoryManager.Instance.MarkProgressCompleted("Cleaning_Lore2");
        }
        if (isPart3)
        {
            StoryManager.Instance.MarkProgressCompleted("Get_Last_Key");
        }
    }
}
