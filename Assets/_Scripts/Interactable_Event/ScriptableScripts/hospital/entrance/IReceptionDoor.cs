using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class IReceptionDoor : MonoBehaviour, IInteractive
{
    private float openValue = -80f;
    private bool isOpen = false;
    private float duration = 3f;
    private void Start(){
        GLogger.Log("isGetReceptionKey" + GameManager.Instance.gameDataManager.gameData.isGetReceptionKey);
        if (GameManager.Instance.gameDataManager.gameData.isGetReceptionKey){
            EnableInteract();
        }
        else{
            DisableInteract();
        }
    }

    public void DisableInteract()
    {
        GetComponent<InteractableObject>().interactableStatus.isInteractable = false;
    }

    public void EnableInteract()
    {
        GetComponent<InteractableObject>().interactableStatus.isInteractable = true;
    }

    public void Interact()
    {
        if (!isOpen){
            //open door
            DisableInteract();

            isOpen = true;
            Vector3 targetValue = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y - openValue, transform.eulerAngles.z);
            transform.DORotate(targetValue, duration, RotateMode.Fast).SetEase(Ease.InOutSine).OnComplete(
                () =>
                {
                    EndAnimation(true);
                }
            );
        }   
    }

    public void OnDestroy()
    {
        EnableInteract();
    }

    private void EndAnimation(bool _isDoorOpen)
    {
        isOpen = _isDoorOpen;
    }

}
