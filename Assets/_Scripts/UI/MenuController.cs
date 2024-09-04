using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuController : MonoBehaviour
{
    public static MenuController Instance;
    private MainMenu_InputActions menuInputActions;
    private GameObject _escMenu;
    private bool _isMenuOpen = false;
    void Awake(){
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }

        // _escMenu = GameObject.Find("");
    }
    
    void Start()
    {   
        menuInputActions = new MainMenu_InputActions();
        menuInputActions.Menu.Enable();
        menuInputActions.Menu.ESC.performed += TriggerMenu;
    }

    void TriggerMenu(InputAction.CallbackContext context){
        if (context.performed){
            if (GameManager.Instance.GetPlayerStatus()){
                if (_isMenuOpen){
                    CloseMenu();
                }
                else {
                    OpenMenu();
                }
            }
            
        }
        
    }
    void OpenMenu(){
        _isMenuOpen = true;
        PlayerController.Instance.ShowCursor(0f);
    }

    public void CloseMenu(){
        _isMenuOpen = false;
        PlayerController.Instance.HideCursor(0f);
    }

    public void CheckMenu(){
        if (_isMenuOpen){
            CloseMenu();
        }
    }
}
