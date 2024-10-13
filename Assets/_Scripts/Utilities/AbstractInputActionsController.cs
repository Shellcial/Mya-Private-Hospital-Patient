using UnityEngine;
using UnityEngine.InputSystem;

public class AbstractInputActionsController : MonoBehaviour
{
    private protected PlayerInput playerInput;
    public bool AutoStartInputs;
    public virtual void InitiateInputActions(){
        playerInput = GetComponent<PlayerInput>();
        if (AutoStartInputs){
            EnablePlayerInput();
        }
        else{
            DisablePlayerInput();
        }

    }
    public virtual void EnablePlayerInput(){
        try {
            playerInput.ActivateInput();
        }
        catch {
            GLogger.LogError("no player input found");
        }
    }
    public virtual void DisablePlayerInput(){
        try {
            playerInput.DeactivateInput();
        }
        catch {
            GLogger.LogError("no player input found");
        }
    }
}
