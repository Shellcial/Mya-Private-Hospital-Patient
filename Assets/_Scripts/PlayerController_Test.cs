using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using DG.Tweening;

//this class control the player movement and object click event

public class PlayerController_Test : MonoBehaviour
{
    private GameObject player;
    private CharacterController controller;
    private PlayerInputActions playerInputActions;

    //control movement
    public float moveSpeed = 2f;
    public float startMoveSpeed = 2f;
    public float fastMoveSpeed = 3f;
    private Vector2 _smoothedInputVector2;
    private Vector2 _smoothInputVelocity; //no need to use this value in this case
    public float smoothSpeed = 0.1f;

    private float _cameraZoomSpeed = 0.5f;
    private float _zoomInValue = 20;
    private float _zoomOutValue = 50;
    private Tween zoomTween;

    //control rotation
    private float mouseRotationSensitivity = 5f;
    private float horizontalRotation = 0f;
    private float verticalRotation = 0f;
    private Transform cameraPlayer;

    //gravity
    private float gravity = -9.81f;
    private Vector3 velocity;

    //display cursor
    private Image cursorDisplay;
    private Image cursorDisplayFocus;

    // raycast
    private RaycastHit hit;
    public float distance = 0.25f;
    [SerializeField]
    private LayerMask _ignore_RayCast;
    private GameObject previousHoverTarget;
    private int interactableLayer;
    private int highlightedLayer;
    private bool isTargetContainsChild;

    public static PlayerController_Test Instance;
    private Camera _characterCamera;
    [SerializeField]
    private Vector3 _startRotation;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }

        player = this.gameObject;

        cameraPlayer = player.transform.Find("Character_Camera");
        _characterCamera = cameraPlayer.GetComponent<Camera>();

        controller = GetComponent<CharacterController>();

        cursorDisplay = GameObject.Find("Display_Cursor").GetComponent<Image>();
        cursorDisplay.transform.localPosition = Camera.main.ViewportToScreenPoint(new Vector3(0f, 0f, 0f)); // alpha is 40
        cursorDisplayFocus = cursorDisplay.transform.GetComponentInChildren<Image>();
        cursorDisplay.color = new Color(1f,1f,1f,1f);
        cursorDisplayFocus.color = new Color(1f,1f,1f,0f);

        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        moveSpeed = startMoveSpeed;

        playerInputActions.Player.Accelerate.performed += SpeedUp;
        playerInputActions.Player.Accelerate.canceled += SlowDown;
        playerInputActions.Player.RightClick.performed += ZoomCamera;
        playerInputActions.Player.RightClick.canceled += ZoomCamera;

        // raycast
        interactableLayer = LayerMask.NameToLayer("Interactable");
        highlightedLayer = LayerMask.NameToLayer("Highlighted");

        EnableDefaultLeftClick(true);

        GameManager.Instance.LockCursor(true);
        GameManager.Instance.ResumeGame();

        transform.localEulerAngles = _startRotation;
    }

    private void FixedUpdate()
    {
        if (!GameManager.Instance.GetPlayerStatus())
        {
            return;
        }
        
        UpdatePosition();
        
        // cast ray to interact with object

        if (GameManager.Instance.GetPointerControlStatus() || !GameManager.Instance.GetGameStatus())
        {
            //don't cast ray when pointer is being directly controlled
            return;
        }
        RayCastOnInteracbleObject();
    }

    void UpdatePosition(){
        //update player movement
        Vector2 inputVector2 = playerInputActions.Player.Movement.ReadValue<Vector2>();
        _smoothedInputVector2 = Vector2.SmoothDamp(_smoothedInputVector2, inputVector2, ref _smoothInputVelocity, smoothSpeed);
        Vector3 movement = new Vector3(_smoothedInputVector2.x, 0, _smoothedInputVector2.y);
        movement = transform.TransformDirection(movement);

        controller.Move(movement * Time.deltaTime * moveSpeed);

        //update player horizontal rotation
        Vector2 mouseDelta = playerInputActions.Player.Look.ReadValue<Vector2>() * mouseRotationSensitivity * Time.deltaTime;
        horizontalRotation = mouseDelta.x;
        player.transform.Rotate(Vector3.up * horizontalRotation);

        //update camera vertical rotation
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
        Ray ray = _characterCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        // get first raycast object and check its type
        if (Physics.Raycast(ray, out hit, distance, ~_ignore_RayCast)){

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
        else if (previousHoverTarget != null){
            // unhighlight previous object if current raycast is null
            UnHighlightObject();
        }
    }
    
    private void HighlightObject(GameObject hitGameObject, InteractableObject targetStatus)
    {
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

    public void UnHighlightObject()
    {   
        if (previousHoverTarget != null){
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
    }


    // click on raycast object
    public void EnableDefaultLeftClick(bool _isEnable)
    {
        if (_isEnable)
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
        //objects must be interactable so that previousHoverTarget is not null
        GLogger.Log("previousHoverTarget: " + previousHoverTarget);
        if (previousHoverTarget != null)
        {
            if (previousHoverTarget.TryGetComponent(out InteractableObject _interactable)){
                _interactable.Interact();
                GLogger.Log("cast success");
            }
            else{
                GLogger.LogError("no IInteractive class on object");
            }
        }
        else
        {
            GLogger.LogWarning("cast failed");
        }
    }

    private void ZoomCamera(InputAction.CallbackContext context){
        if (context.performed){
            zoomTween.Kill();
            zoomTween = _characterCamera.DOFieldOfView(_zoomInValue, _cameraZoomSpeed).SetEase(Ease.InOutSine);
        }
        else if (context.canceled){
            zoomTween.Kill();
            zoomTween = _characterCamera.DOFieldOfView(_zoomOutValue, _cameraZoomSpeed).SetEase(Ease.InOutSine);
        }
    }
}
