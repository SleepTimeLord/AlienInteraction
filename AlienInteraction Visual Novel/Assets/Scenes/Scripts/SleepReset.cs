using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepReset : MonoBehaviour, IInteractionable
{
    public bool sleeping = false;
    public GameObject blackScreen;
    PlayerMovement playerMovement;
    PlayerCam playerCam;
    Day1Script day1Script;
    // Start is called before the first frame update

    public void Interact()
    {
        sleeping = true;
        blackScreen.SetActive(true);
        gameObject.layer = LayerMask.NameToLayer("NonInteractable");

        if (day1Script.doneDay1)
        {
            StoryManager.Instance.MarkProgressCompleted("Sleep_Talk1");
            day1Script.doneDay1 = false;
        }

        if (day1Script.doneDay2)
        {
            TaskManager.CompleteTask("Go To Bed");
            StartCoroutine(Sleeping());
            day1Script.doneDay2 = false;
        }
    }

    void Start()
    {
        sleeping = false;
        day1Script = FindAnyObjectByType<Day1Script>();
        playerMovement = FindAnyObjectByType<PlayerMovement>();
        playerCam = FindAnyObjectByType<PlayerCam>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.P))
        {
            WakeUp();
        }
    }

    public void WakeUp()
    {
        blackScreen.SetActive(false);
        playerMovement.ResetPlayerPos();
        playerCam.ResetCamPosition();
    }


    IEnumerator Sleeping()
    {
        yield return new WaitForSeconds(2f);

        StoryManager.Instance.MarkProgressCompleted("Start_Day_3");
    }
}
