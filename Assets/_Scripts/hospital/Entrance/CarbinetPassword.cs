using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using DG.Tweening;

public class CarbinetPassword : MonoBehaviour
{
    // pass password to move carbinet
    [SerializeField]
    private AudioSource audioSource;
    private List<int> _password = new List<int>(){7, 21, 13, 13, 25};
    
    private float _moveTime = 3f;
    [SerializeField]
    private GameObject _carbinetObject;
    private int _checkingIndex = 0;
    private bool isCarbinetMoved = false;
    public void PassPassword(int _clickedPassword){
        if (!isCarbinetMoved){
            FlatAudioManager.Instance.Play("press_label", false);
            if (_clickedPassword == _password[_checkingIndex]){
                _checkingIndex++;
                if (_checkingIndex == _password.Count){
                    // open door
                    isCarbinetMoved = true;
                    MoveCarbinet();
                }
            }
            else{
                _checkingIndex = 0;
            }
        }
    }
    void MoveCarbinet(){
        audioSource.Play();
        _carbinetObject.transform.DOMoveX(0.726f, _moveTime);
    }
}
