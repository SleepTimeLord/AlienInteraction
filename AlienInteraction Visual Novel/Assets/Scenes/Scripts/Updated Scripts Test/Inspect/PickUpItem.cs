using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class PickUpItem : MonoBehaviour, IInteractionable
{

    private ItemManager itemManager;
    public bool canRotate;
    public bool faceForward;
    public GameObject frontOfObject;
    private CinemachineBrain brain;

    private void Start()
    {
        itemManager = FindAnyObjectByType<ItemManager>();
        brain = FindAnyObjectByType<CinemachineBrain>();
    }
    public void Interact()
    {
        if (itemManager != null && !brain.IsBlending)
        {
            if (itemManager.pickedUpObject == null)
            {
                itemManager.pickedUpObject = transform.gameObject;
                gameObject.layer = 3;
            }
            else
            {
                gameObject.layer = 6;
            }
        }
        else
        {
            Debug.LogWarning("Reference the ItemManager Script");
        }
    }
}