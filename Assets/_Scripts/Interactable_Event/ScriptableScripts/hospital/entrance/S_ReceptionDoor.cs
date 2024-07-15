using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class S_ReceptionDoor : InteractableObject
{
    private float openValue = -80f;
    private bool isOpen = false;
    private float duration = 3f;
    private void Start(){
        GLogger.Log("isGetReceptionKey: " + GameManager.Instance.gameDataManager.gameData.isGetReceptionKey);
        if (GameManager.Instance.gameDataManager.gameData.isGetReceptionKey){
            EnableInteract();
        }
        else{
            DisableInteract();
        }
    }

    public override void Interact()
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

    private void EndAnimation(bool _isDoorOpen)
    {
        isOpen = _isDoorOpen;
    }

}
