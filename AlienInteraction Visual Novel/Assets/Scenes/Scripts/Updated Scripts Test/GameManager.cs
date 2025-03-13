using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameStateData gameStateData;

    public GameEvent testEvent1;
    public GameEvent testEvent2;

    private void OnEnable()
    {
        if (testEvent1 != null && !gameStateData.doneClickingBlock1)
        {
            Debug.Log("this is activated");
            testEvent1.OnEventRaised.AddListener(TestComplete);
        }
    }

    private void OnDisable()
    {
        if(testEvent1 != null && !gameStateData.doneClickingBlock1)
        {
            testEvent1.OnEventRaised.RemoveListener(TestComplete);
        }
    }
    
    void TestComplete()
    {
        gameStateData.doneClickingBlock1 = true;
        Debug.Log("test completed from here and gameStateData.doneClickingBlock1 is true");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
