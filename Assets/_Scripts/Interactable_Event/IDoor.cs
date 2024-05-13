using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class IDoor : MonoBehaviour, IInteractive
{
    private float closedValue = 0f;
    private float openValue = 90f;
    private bool isOpen = false;
    private float duration = 3f;
    public void Interact(){

        this.gameObject.GetComponent<InteractableObject>().interactableStatus.isInteractable = false;
        this.gameObject.GetComponent<InteractableObject>().interactableStatus.isHighlightable = false;
        
        if (isOpen){
            // close door
            isOpen = false;
            Vector3 targetValue = new Vector3(gameObject.transform.eulerAngles.x, gameObject.transform.eulerAngles.y + openValue, gameObject.transform.eulerAngles.z);
            this.gameObject.transform.DORotate(targetValue, duration, RotateMode.Fast).SetEase(Ease.InOutSine).OnComplete(
                () =>
                {
                    EndAnimation(false);
                }
            );
        }
        else{
            //open door
            Vector3 targetValue = new Vector3(gameObject.transform.eulerAngles.x, gameObject.transform.eulerAngles.y - openValue, gameObject.transform.eulerAngles.z);
            this.gameObject.transform.DORotate(targetValue, duration, RotateMode.Fast).SetEase(Ease.InOutSine).OnComplete(
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
        GetComponent<InteractableObject>().interactableStatus.isHighlightable = true;
        GetComponent<InteractableObject>().interactableStatus.isInteractable = true;
        this.gameObject.GetComponent<InteractableObject>().interactableStatus.isInteractable = true;
        this.gameObject.GetComponent<InteractableObject>().interactableStatus.isHighlightable = true;
    }
}
