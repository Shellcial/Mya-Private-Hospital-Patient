using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//this class control the player movement and object click event

public class PlayerController : MonoBehaviour
{
    private GameObject player;
    private CharacterController controller;
    private PlayerInputActions playerInputActions;

    //control movement
    public float moveSpeed = 2f;
    public float startMoveSpeed = 2f;
    public float fastMoveSpeed = 3f;
    private Vector2 smoothedInputVector2;
    private Vector2 smoothInputVelocity; //no need to use this value in this case
    public float smoothSpeed = 0.1f;

    //control rotation
    private float mouseRotationSensitivity = 15f;
    private float horizontalRotation = 0f;
    private float verticalRotation = 0f;
    private Transform cameraPlayer;

    //gravity
    private float gravity = -9.81f;
    private Vector3 velocity;

    //display cursor
    private GameObject cursorDisplay;

    // raycast
    private RaycastHit hit;
    public float distance = 0.5f;
    private GameObject previousHoverTarget;
    private int interactableLayer;
    private int highlightedLayer;
    private bool isTargetContainsChild;

    private void Awake()
    {
        player = this.gameObject;

        cameraPlayer = player.transform.Find("Character_Camera");

        controller = GetComponent<CharacterController>();

        cursorDisplay = GameObject.Find("Display_Cursor");
        cursorDisplay.transform.localPosition = Camera.main.ViewportToScreenPoint(new Vector3(0f, 0f, 0f)); // alpha is 40
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        moveSpeed = startMoveSpeed;

        playerInputActions.Player.Accelerate.performed += SpeedUp;
        playerInputActions.Player.Accelerate.canceled += SlowDown;

        // raycast
        interactableLayer = LayerMask.NameToLayer("Interactable");
        highlightedLayer = LayerMask.NameToLayer("Highlighted");

        EnableDefaultLeftClick(true);
    }

    private void FixedUpdate()
    {
        if (!GameManager.instance.GetPlayerStatus())
        {
            return;
        }

        //player movement
        Vector2 inputVector2 = playerInputActions.Player.Movement.ReadValue<Vector2>();
        smoothedInputVector2 = Vector2.SmoothDamp(smoothedInputVector2, inputVector2, ref smoothInputVelocity, smoothSpeed);
        Vector3 movement = new Vector3(smoothedInputVector2.x, 0, smoothedInputVector2.y);
        movement = transform.TransformDirection(movement);

        controller.Move(movement * Time.deltaTime * moveSpeed);

        //player horizontal rotation
        Vector2 mouseDelta = playerInputActions.Player.Look.ReadValue<Vector2>() * mouseRotationSensitivity * Time.deltaTime;
        horizontalRotation = mouseDelta.x;
        player.transform.Rotate(Vector3.up * horizontalRotation);

        //camera vertical rotation
        verticalRotation -= mouseDelta.y;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);
        cameraPlayer.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);

        //gravity behavior
        if (!controller.isGrounded)
        {
            velocity.y += gravity * Time.deltaTime * Time.deltaTime;
            controller.Move(velocity);
        }
        else
        {
            velocity.y = 0;
        }

        // cast ray to interact with object

        if (GameManager.instance.GetPointerControlStatus() || !GameManager.instance.GetGameStatus())
        {
            //don't cast ray when pointer is being directly controlled
            return;
        }
        RayCastOnInteracbleObject();
    }

    void SpeedUp(InputAction.CallbackContext context)
    {
        moveSpeed = fastMoveSpeed;
    }

    void SlowDown(InputAction.CallbackContext context)
    {
        moveSpeed = startMoveSpeed;
    }

    private void RayCastOnInteracbleObject()
    {
        //Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        Ray ray = cameraPlayer.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        // get first raycast object and check its type
        if (Physics.Raycast(ray, out hit, distance)){
            if (hit.collider.gameObject.layer == 10 || hit.collider.gameObject.layer == 11)
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


    // click on raycast object
    public void EnableDefaultLeftClick(bool isEnable)
    {
        if (isEnable)
        {
            playerInputActions.Player.Click.performed += CastRayOnClick;
        }
        else
        {
            playerInputActions.Player.Click.performed -= CastRayOnClick;
        }
    }

    private void CastRayOnClick(InputAction.CallbackContext context)
    {
        Debug.Log("try to ray cast");
        //objects must be interactable so that previousHoverTarget is not null
        if (previousHoverTarget != null)
        {
            //pointer.SetActive(true);
            //pointer.transform.position = hit.point;
            // click
            // DetermineClickEvent(previousHoverTarget.tag);
            Debug.Log(previousHoverTarget);
            if (previousHoverTarget.TryGetComponent(out IInteractive _interactive)){
                _interactive.Interact();
                Debug.Log("cast success");
            }
        }
        else
        {
            Debug.Log("cast failed");
        }
    }
}
