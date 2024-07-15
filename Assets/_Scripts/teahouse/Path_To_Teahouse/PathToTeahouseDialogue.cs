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
        GLogger.Log(playerInput);
        playerInput.actions["Click"].performed += LeftClick;
    }

    public async void LeftClick(InputAction.CallbackContext context){
        // GLogger.Log("left clicked");
        await GeneralUIManager.Instance.FadeInBlack();
        SceneManager_Path_To_Teahouse.Instance.SwitchScene();
    }
}
