using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;
public class S_StaffroomDoor : InteractableObject
{
    private float openValue = 90f;
    private bool isOpen = false;
    private float duration = 3f;
    [SerializeField]
    private bool isExitDoor;
    private void Start(){
        EnableInteract();
    }
    public override void Interact(){

        DisableInteract();
        
        if (!isOpen){
            //open door
            
            isOpen = true;
            Vector3 targetValue = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y - openValue, transform.eulerAngles.z);
            if (isExitDoor){
                GameManager.Instance.PauseGame();
                SceneManager_TeahouseStaffroom.Instance.SwitchScene(); 
            }
            else{
                FlatAudioManager.Instance.Play("staffroom_door", false);
            }

            transform.DORotate(targetValue, duration, RotateMode.Fast).SetEase(Ease.InOutSine);
        }
    }
}
