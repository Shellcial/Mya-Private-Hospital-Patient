using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DebugEvents : MonoBehaviour
{
    public PlayerInputActions playerInputActions;

    public bool isScaryEntered = false;

    // Start is called before the first frame update
    void Start()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Debug.performed += SpaceClicked;
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    private void SpaceClicked(InputAction.CallbackContext context)
    {
        if (isScaryEntered){
            ChangeHorrorCloseup.instance.ChangeToNormal();
            isScaryEntered = false;
        }else{
            ChangeHorrorCloseup.instance.ChnageToHorror();
            isScaryEntered = true;
        }
    }
}
