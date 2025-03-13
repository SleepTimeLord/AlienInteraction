using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dirt : MonoBehaviour, IInteractionable
{
    Day1Script day1Script;
    public bool isPart1;
    public bool isPart2;
    // Start is called before the first frame update
    private void Start()
    {
        day1Script = FindAnyObjectByType<Day1Script>();
    }
    public void Interact()
    {
        if (isPart1)
        {
            day1Script.dirtCleaned += 1;
            
        }
        if (isPart2)
        {
            day1Script.dirtCleaned2 += 1;
        }

        gameObject.SetActive(false);
    }
}