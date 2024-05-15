using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PathToTeahouseDialogue : AbstractInputActionsController
{
    // Start is called before the first frame update
    void Awake()
    {
        GeneralUIManager.Instance.SetBlack();
    }

    async void Start(){
        await GeneralUIManager.Instance.FadeOutBlack();
        playerInput.actions["Click"].performed += LeftClick;
    }

    public async void LeftClick(InputAction.CallbackContext context){
        Debug.Log("clicked");
        await GeneralUIManager.Instance.FadeInBlack();
        SceneManager.LoadScene(2);
    }
}
