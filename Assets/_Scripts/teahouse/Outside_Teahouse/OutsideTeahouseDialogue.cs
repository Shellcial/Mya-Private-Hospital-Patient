using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class OutsideTeahouseDialogue : AbstractInputActionsController
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
        await GeneralUIManager.Instance.FadeInBlack();
        SceneManager.LoadScene("Teahouse_Staffroom");
    }
}
