using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtManager : MonoBehaviour
{
    // Start is called before the first frame update
    Day1Script day1Script;
    void Start()
    {
        day1Script = FindAnyObjectByType<Day1Script>();
    }

    // Update is called once per frame
    void Update()
    {
        if (day1Script.dirtCleaned == 6)
        {
            StoryManager.Instance.MarkProgressCompleted("Done_Cleaning1");
            day1Script.dirtCleaned = 0;
        }

        if (day1Script.dirtCleaned2 == 6)
        {
            StoryManager.Instance.MarkProgressCompleted("Done_Cleaning2");
            day1Script.dirtCleaned2 = 0;
        }
    }
}
