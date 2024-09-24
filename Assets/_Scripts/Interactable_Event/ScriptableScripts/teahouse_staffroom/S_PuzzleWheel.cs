using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG;
using DG.Tweening;
using UnityEngine.UIElements;

public class S_PuzzleWheel : InteractableObject
{
    private bool _isPlayable = true;
    private bool _isPlayingAnimation = false;
    public int chosenIndex = 0;
    private float _duration = 0.5f;    

    private void Start(){
        EnableInteract();
    }

    public override void Interact(){
        if (_isPlayable && !_isPlayingAnimation) PlayAnimation();
    }

    void PlayAnimation(){

        _isPlayingAnimation = true;

        if (chosenIndex == 3){
            chosenIndex = 0;
        }
        else{
            chosenIndex++;   
        }

        Vector3 targetValue = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z - 90f);
        int randomAudio = Random.Range(1, 5);
        FlatAudioManager.Instance.Play("wood_wipe_"+randomAudio, false);
        transform.DORotate(targetValue, _duration, RotateMode.Fast).SetEase(Ease.InOutSine).OnComplete(()=>{
            _isPlayingAnimation = false;
        });
    }

    // triggered
    public void DisableWheel(){
        _isPlayable = false;
        DisableInteract();
    }

}
