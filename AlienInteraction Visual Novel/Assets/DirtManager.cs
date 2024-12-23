using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtManager : MonoBehaviour
{
    // Start is called before the first frame update
    Day1Script day1Script;
    void Start()
    {
        day1Script = FindObjectOfType<Day1Script>();
    }

    // Update is called once per frame
    void Update()
    {
        if (day1Script.dirtCleaned == 6)
        {
            StoryManager.Instance.MarkProgressCompleted("Done_Cleaning1");
        }
    }
}
