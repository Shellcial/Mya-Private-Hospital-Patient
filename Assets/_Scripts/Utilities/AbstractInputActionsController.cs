using UnityEngine;
using UnityEngine.InputSystem;

public class AbstractInputActionsController : MonoBehaviour
{
    private protected PlayerInput playerInput;
    public bool AutoStartInputs;
    public virtual void InitiateInputActions(){
        playerInput = GetComponent<PlayerInput>();
        Debug.Log(playerInput);
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
            Debug.LogError("no player input found");
        }
    }
    public virtual void DisablePlayerInput(){
        try {
            playerInput.DeactivateInput();
        }
        catch {
            Debug.LogError("no player input found");
        }
    }
}
