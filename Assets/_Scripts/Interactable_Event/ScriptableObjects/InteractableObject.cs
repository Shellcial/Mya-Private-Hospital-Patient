using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    public InteractableScriptableObject interactableStatus;
    public abstract void Interact();
    public virtual void EnableInteract(){
        interactableStatus.isInteractable = true;
    }
    public virtual void DisableInteract(){
        interactableStatus.isInteractable = false;
    }
    public virtual void OnDestroy(){
        EnableInteract();
    }
}
