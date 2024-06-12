using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PathToTeahouseDialogue : AbstractInputActionsController
{
    void Awake()
    {
        InitiateInputActions();
        GeneralUIManager.Instance.SetBlack();
    }

    async void Start(){
        await GeneralUIManager.Instance.FadeOutBlack(2f);
        Debug.Log(playerInput);
        playerInput.actions["Click"].performed += LeftClick;
    }

    public async void LeftClick(InputAction.CallbackContext context){
        await GeneralUIManager.Instance.FadeInBlack();
        SceneManager.LoadScene(2);
    }
}
