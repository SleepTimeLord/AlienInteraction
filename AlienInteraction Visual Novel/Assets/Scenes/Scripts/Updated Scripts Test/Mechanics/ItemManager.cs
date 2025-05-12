using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemManager : MonoBehaviour
{
    public GameObject player;
    public Transform holdPos;

    public Camera mainCam;
    private GameObject clonedHeldObj;
    public float pickUpSmooth = 5f;
    public bool isInPickUpMode = false;
    public float rotationSens = 1f;
    public GameObject heldObj;
    public GameObject savedObject;
    private Rigidbody heldObjRb;
    private bool canDrop = true;
    private int layerNumber;

    public GameObject pickedUpObject;

    PlayerController playerController;

    private void Awake()
    {
        playerController = new PlayerController();
        layerNumber = LayerMask.NameToLayer("Inspectable");

        if (holdPos.localScale != Vector3.one)
        {
            Debug.LogError("HoldPos must have scale (1,1,1) for proper object handling!");
            holdPos.localScale = Vector3.one;
        }
    }

    private void OnEnable()
    {
        if (canDrop)
        {
            playerController.Enable();
        }
        playerController.PlayerControls.ExitInspect.started += DropObj;
    }

    private void OnDisable()
    {
        playerController.Disable();
        playerController.PlayerControls.ExitInspect.started -= DropObj;
    }

    public void EquipItem(GameObject equipItem)
    {
        bool itemFound = false;


        if (!itemFound)
        {
            Debug.LogWarning("Item is not in the Obj Holder");
        }
    }

    private void FixedUpdate()
    {
        if (pickedUpObject != null)
        {
            PickUpObject(pickedUpObject);
        }

        if (isInPickUpMode)
        {
            RotateObject();
        }
    }

    public void PickUpObject(GameObject pickUpObj)
    {
        if (pickedUpObject != null)
        {
            if (!isInPickUpMode)
            {
                clonedHeldObj = Instantiate(pickUpObj);
                savedObject = pickUpObj;
                savedObject.SetActive(false);
            }

            if (clonedHeldObj.GetComponent<Rigidbody>()) //make sure the object has a RigidBody
            {
                heldObj = clonedHeldObj; //assign heldObj to the object that was hit by the raycast (no longer == null)
                heldObjRb = clonedHeldObj.GetComponent<Rigidbody>(); //assign Rigidbody
                heldObjRb.isKinematic = true;
                //heldObjRb.transform.parent = holdPos.transform; //parent object to holdposition
                heldObjRb.transform.position = Vector3.Slerp(heldObjRb.transform.position, holdPos.position, Time.deltaTime * pickUpSmooth);
                heldObj.layer = layerNumber; //change the object layer to the holdLayer
                                             //make sure object doesnt collide with player, it can cause weird bugs
                Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), true);
                isInPickUpMode = true;
            }
        }
    }

    void RotateObject()
    {
        if (Input.GetKey(KeyCode.R))//hold R key to rotate, change this to whatever key you want
        {
            canDrop = false;

            float mouseX = Input.GetAxis("Mouse X") * rotationSens;
            float mouseY = Input.GetAxis("Mouse Y") * rotationSens;

            // Rotate around camera's horizontal plane
            heldObj.transform.Rotate(mainCam.transform.up, mouseX, Space.World);
            heldObj.transform.Rotate(mainCam.transform.right, -mouseY, Space.World);
        }
        else
        {
            canDrop = true;
        }
    }

    void DropObj(InputAction.CallbackContext context)
    {
        Destroy(heldObj);

        pickedUpObject = null;
        heldObj.transform.parent = null; //unparent object
        heldObj = null; //undefine game object
        isInPickUpMode = false;
        savedObject.SetActive(true);
        savedObject = null;
    }
}