using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.InputSystem;

//this class control player trigger event
public class PlayerEvent : MonoBehaviour
{
    // for display only
    private GameObject pointer;
    
    private GameObject previousHoverTarget;
    private bool isTargetContainsChild;
    private Camera cameraPlayer;

    private RaycastHit hit;
    private float distance = 2f;

    private int interactableLayer;
    private int highlightedLayer;

    private GameObject holdingMenuParent;

    public PlayerInputActions PlayerInputActions;
     
    private void Awake()
    {
        pointer = GameObject.Find("Pointer_Display");
        pointer.SetActive(false);
        interactableLayer = LayerMask.NameToLayer("Interactable");
        highlightedLayer = LayerMask.NameToLayer("Highlighted");

        cameraPlayer = GameObject.Find("Player").transform.Find("Main Camera").GetComponent<Camera>();

        holdingMenuParent = cameraPlayer.transform.Find("HoldingMenu").gameObject;
    }

    private void OnEnable()
    {
        PlayerInputActions = new PlayerInputActions();
        PlayerInputActions.Player.Enable();
        EnableDefaultLeftClick(true);
    }

    private void Update()
    {

    }

    //event handle
    public void EnableDefaultLeftClick(bool isEnable)
    {
        if (isEnable)
        {
            PlayerInputActions.Player.Click.performed += CastRayOnClick;
        }
        else
        {
            PlayerInputActions.Player.Click.performed -= CastRayOnClick;
        }
    }

    //highlight object
    private void RayCastOnInteracbleObject()
    {
        //Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        Ray ray = cameraPlayer.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if (Physics.Raycast(ray, out hit, distance, LayerMask.GetMask("Interactable", "Highlighted")))
        {
            //hit interactable or highlighted layer object
            InteractableObject targetStatus;
            GameObject hitGameObject;
            //get target status
            if (hit.collider.GetComponent<InteractableObject>() == null)
            {
                //get parent since current target does not contain InteractableObject
                targetStatus = hit.collider.transform.parent.GetComponent<InteractableObject>();
                hitGameObject = hit.collider.transform.parent.gameObject;
            }
            else
            {
                //get target if it contains InteractableObject
                targetStatus = hit.collider.GetComponent<InteractableObject>();
                hitGameObject = hit.collider.gameObject;
            }

            if (previousHoverTarget != hitGameObject)
            {
                //new object is hit

                if (previousHoverTarget != null)
                {
                    //ensure the previous target is not null and unhighlight it if avaliable
                    if (previousHoverTarget.layer == highlightedLayer)
                    {
                        //dishighlight old object, this action behaves the same as raycasted general object,
                        //if the new object is not interactable neither
                        UnHighlightObject();
                    }
                }

                if (targetStatus.interactableStatus.isInteractable)
                {
                    if (targetStatus.interactableStatus.isHighlightable)
                    {
                        //highlight object set this as old object
                        HighlightObject(hitGameObject, targetStatus);
                    }
                    else
                    {
                        //only interactable object, no highlight occurs
                        previousHoverTarget = hitGameObject;
                    }
                }
            }
            else
            {
                //same object is hit, simply check the interactable status
                if (targetStatus.interactableStatus.isHighlightable && targetStatus.interactableStatus.isInteractable)
                {
                    //current object is interactable 
                    if (previousHoverTarget.layer != highlightedLayer)
                    {
                        //highlight it if it is not highlighted 
                        HighlightObject(hitGameObject, targetStatus);
                    }
                }
                else
                {
                    //object is not interactable and highlightable anymore, unhighlight it
                    if (previousHoverTarget.layer == highlightedLayer)
                    {
                        UnHighlightObject();
                    }
                }

            }
        }
        else if (previousHoverTarget != null)
        {
            //raycasted object that is not interactable
            //dishighlight old object if any and set it as null
            UnHighlightObject();
        }
    }

    private void HighlightObject(GameObject hitGameObject, InteractableObject targetStatus)
    {
        //Debug.Log("highlight enable");

        hitGameObject.layer = highlightedLayer;
        isTargetContainsChild = false;
        if (targetStatus.interactableStatus.isChildHighlight)
        {
            isTargetContainsChild = true;
            //get a list of excludedChild if any
            List<string> excludedChild = new List<string>();
            if (!targetStatus.interactableStatus.isAllChild)
            {
                foreach (string child in targetStatus.interactableStatus.excludedChild)
                {
                    excludedChild.Add(child);
                }          
            }

            //loop through the child object and compare with exclude list if any
            if (excludedChild.Count > 0)
            {
                foreach (Transform child in hitGameObject.transform)
                {
                    if (!excludedChild.Exists(x => x.Contains(child.name)))
                    {
                        child.gameObject.layer = highlightedLayer;
                    }
                }
            }
            else
            {
                foreach (Transform child in hitGameObject.transform)
                {
                    child.gameObject.layer = highlightedLayer;
                }
            }
        }
        previousHoverTarget = hitGameObject;
    }

    private void UnHighlightObject()
    {
        //Debug.Log("highlight disable");
        previousHoverTarget.layer = interactableLayer;
        if (isTargetContainsChild)
        {
            foreach (Transform child in previousHoverTarget.transform)
            {
                child.gameObject.layer = interactableLayer;
            }
        }
        previousHoverTarget = null;
    }

    private void CastRayOnClick(InputAction.CallbackContext context)
    {
        //Debug.Log("try to cast");
        //objects must be interactable so that previousHoverTarget is not null
        if (previousHoverTarget != null)
        {
            //pointer.SetActive(true);
            //pointer.transform.position = hit.point;
            DetermineClickEvent(previousHoverTarget.tag);
        }
        else
        {
            //pointer.SetActive(false);
        }
    }

    private void DetermineClickEvent(string tagName)
    {
        switch (tagName)
        {
            case "Door":
                // previousHoverTarget.GetComponent<DoorEvent>().TriggerDoorEvent();
                break;
            case "HingedDoor":
                previousHoverTarget.GetComponent<HingedDoor>().TriggerHingedDoorEvent(this.gameObject.transform.localPosition.x);
                break;
            default:
                Debug.LogWarning("no tag name found");
                break;
        }
    }
}
