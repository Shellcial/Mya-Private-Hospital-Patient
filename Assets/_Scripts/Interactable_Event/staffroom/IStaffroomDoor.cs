using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class IStaffroomDoor : MonoBehaviour, IInteractive
{
    private float closedValue = 0f;
    private float openValue = 90f;
    private bool isOpen = false;
    private float duration = 3f;
    private void Start(){
        EnableInteract();
    }
    public void Interact(){

        DisableInteract();
        
        if (isOpen){
            // close door
            isOpen = false;
            Vector3 targetValue = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + openValue, transform.eulerAngles.z);
            transform.DORotate(targetValue, duration, RotateMode.Fast).SetEase(Ease.InOutSine).OnComplete(
                () =>
                {
                    EndAnimation(false);
                }
            );
        }
        else{
            //open door
            Vector3 targetValue = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y - openValue, transform.eulerAngles.z);
            transform.DORotate(targetValue, duration, RotateMode.Fast).SetEase(Ease.InOutSine).OnComplete(
                () =>
                {
                    EndAnimation(true);
                }
            );
        }
    }
    
    private void EndAnimation(bool _isDoorOpen)
    {
        isOpen = _isDoorOpen;
    }

    public void EnableInteract(){
        GetComponent<InteractableObject>().interactableStatus.isInteractable = true;
    }

    public void DisableInteract(){
        // set layer to deafult
        GetComponent<InteractableObject>().interactableStatus.isInteractable = false;
    }

    public void OnDestroy(){
        EnableInteract();
    }

}
