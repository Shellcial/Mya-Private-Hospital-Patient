using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using Unity.VisualScripting;
using UnityEngine;

public class PosterDown : MonoBehaviour
{
    private bool _isTriggered = false;
    private bool _isAnimationPlayed = false;
    [SerializeField]
    private List<Animator> _animatorList = new List<Animator>();
    void Start(){
        _isTriggered = false;
        _isAnimationPlayed = false;
        foreach (Animator _animator in _animatorList){
            _animator.speed = 0f;
        } 
    }

    void OnTriggerEnter(Collider other){
        if (!_isTriggered){
            _isTriggered = true;
            if (!_isAnimationPlayed){
                _isAnimationPlayed = true;
                GameManager.Instance.gameDataManager.UnlockIllustration("cleaner_room_mya_poster");
                GameManager.Instance.gameDataManager.UnlockIllustration("cleaner_room_gummy_poster");
                foreach (Animator _animator in _animatorList){
                    _animator.speed = 1f;
                }
            }
        }
    }

    public void PlaySlipSound(){
        FlatAudioManager.Instance.Play("paper_slip", false);
    }
    
    public void PlayFallSound(){
        FlatAudioManager.Instance.Play("paper_fall", false);
    }
}
