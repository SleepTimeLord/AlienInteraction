using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dirt : MonoBehaviour, IInteractionable
{
    Day1Script day1Script;
    // Start is called before the first frame update
    private void Start()
    {
        day1Script = FindObjectOfType<Day1Script>();
    }
    public void Interact()
    {
        day1Script.dirtCleaned += 1;
        gameObject.SetActive(false);
    }
}