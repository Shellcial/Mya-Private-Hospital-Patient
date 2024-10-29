using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class S_Labels : InteractableObject
{
    [SerializeField]
    private int _labelIndex;
    bool isPlaying = false;
 
    public override void Interact()
    {
        if (!isPlaying){
            isPlaying = true;
            SceneManager_HospitalLeave.Instance.carbinetPassword.PassPassword(_labelIndex);
            transform.DOLocalMoveY(-0.0003f, 0.3f).SetRelative().OnComplete(()=>{
                transform.DOLocalMoveY(0.0003f, 0.3f).SetRelative().OnComplete(()=>{
                    isPlaying = false;
                });
            });
        }
    }
}
