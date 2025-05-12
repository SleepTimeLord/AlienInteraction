using JetBrains.Annotations;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class RefuelOxygen : MonoBehaviour, IInteractionable
{
    Oxygen oxygenManager;

    private void Start()
    {
        oxygenManager = FindAnyObjectByType<Oxygen>();
    }
    public void Interact()
    {
        StartCoroutine(oxygenManager.IncreaseOxygenOvertime(100f));
        Debug.Log("interacted HAHAHAHAHHHHHHHHHHHHH");
    }

}
