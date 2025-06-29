using UnityEngine;

public class OpenPowerDoor : MonoBehaviour, IInteractionable
{
    private bool isOpen;
    public Transform rotationPoint;
    private Vector3 openDoor = new Vector3(0,180,0);

    private void Start()
    {

    }

    public void Interact()
    {
        if (isOpen)
        {
            rotationPoint.eulerAngles = openDoor; 
            isOpen = false;
        }
        else
        {
            rotationPoint.eulerAngles = Vector3.zero;
            isOpen = true;
        }
    }

}
