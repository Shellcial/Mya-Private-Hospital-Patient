using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class S_ReceptionDoor : InteractableObject
{
    private float openValue = -80f;
    private bool isOpen = false;
    private float duration = 3f;
    private void Start(){
        EnableInteract();
    }

    public override void Interact()
    {
        if (!isOpen){
            if (SceneManager.GetActiveScene().name == "Hospital_Leave"){
                if (GameManager.Instance.gameDataManager.gameData.isGetReceptionKey){
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
            else{
                FlatAudioManager.Instance.Play("door_locked", false);
            }
        }   
    }

    private void EndAnimation(bool _isDoorOpen)
    {
        isOpen = _isDoorOpen;
    }

}
