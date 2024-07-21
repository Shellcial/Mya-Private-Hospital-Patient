using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DG.Tweening;
using UnityEngine;

public class S_PuzzleButton : InteractableObject
{
    public bool isPressable = true;
    private List<int> _correctPassword = new List<int>(){0, 1, 3, 2};
    private bool isCorrect = true;
    private Vector3 _startPos;
    private Vector3 _endPos = new Vector3 (0.8207f, 0.8808f, 0.640121f);
    private float _duration = 0.5f;
    private bool _isPlayingAnimation = false;
    [SerializeField]
    private ChangeLight _changeLight;
    [SerializeField]
    private List<S_PuzzleWheel> _puzzleWheels = new List<S_PuzzleWheel>();

    private void Start(){
        _startPos = this.transform.localPosition;
        _endPos = new Vector3(_startPos.x + 0.0015f, _startPos.y + 0.001f, _startPos.z);
        EnableInteract();
    }

    public override void Interact(){
        if (!isPressable) return;
        
        // play animation
        if (!_isPlayingAnimation){
            _isPlayingAnimation = true;
            transform.DOLocalMove(_endPos, _duration).SetEase(Ease.InOutSine).OnComplete(() => {
                transform.DOLocalMove(_startPos, _duration).SetEase(Ease.InOutSine).OnComplete(() => {
                    _isPlayingAnimation = false;
                });
            });
        }

        // check password
        for (int i = 0; i < _puzzleWheels.Count; i++){
            if (_puzzleWheels[i].chosenIndex != _correctPassword[i]){
                isCorrect = false;
                break;
            }
        }

        if (isCorrect){
            isPressable = false;
            // turn on light and change light, trigger animation and sound
            _changeLight.ShowCorrectLight();
            DisableInteract();
        }
    }

    public override void DisableInteract(){
        // set layer to deafult
        GetComponent<InteractableObject>().interactableStatus.isInteractable = false;
        PlayerController.Instance.UnHighlightObject();
        _puzzleWheels.ForEach(wheel => {
            wheel.DisableWheel();
        });        
    }
}
