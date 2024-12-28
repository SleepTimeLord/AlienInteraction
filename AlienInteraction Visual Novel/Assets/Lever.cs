using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour, IInteractionable
{
    Day1Script day1Script;
    ChangeScene changeScene;

    void Start()
    {
        changeScene = FindObjectOfType<ChangeScene>();
        day1Script = FindObjectOfType<Day1Script>();
    }
    // Start is called before the first frame update
    public void Interact()
    {
        if (day1Script.yesRoute)
        {
            changeScene.sceneName = "YesChoice";
        }
        else
        {
            changeScene.sceneName = "NoChoice";
        }
        changeScene.ChangeToNextScene();
    }
}
