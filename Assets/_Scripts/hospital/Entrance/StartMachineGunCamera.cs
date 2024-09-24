using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DG.Tweening;
using Tayx.Graphy.Audio;
using UnityEngine;

public class StartMachineGunCamera : MonoBehaviour
{
    [SerializeField]
    private GameObject _animateCamera;
    [SerializeField]
    private GameObject _playerCamera;
    private bool _isPlayed = false;

    private Vector3 _targetRot = new Vector3(-65f, -95f, 0.701f);
    private Vector3 _targetRot2 = new Vector3(-12.406f, -111.619f, 0.701f);
    private Vector3 _targetPos = new Vector3(-21.547f, 1.532f, 3.675f);
    
    [SerializeField]
    private Animator machineGunAnimator;    

    void Start(){
        _animateCamera.SetActive(false);
        _playerCamera.SetActive(true);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!_isPlayed){
            _isPlayed = true;
            GameManager.Instance.PauseGame();
            StartCoroutine(StartMachineGunAniamtion());
        }
    }

    IEnumerator StartMachineGunAniamtion(){
        // rotate to front
        _playerCamera.transform.DORotate(new Vector3(0,-90,0), 2f).SetEase(Ease.InOutSine);
        _playerCamera.transform.DOMove(_targetPos, 3f).SetEase(Ease.InOutSine);
        yield return new WaitForSeconds(4f);
        FlatAudioManager.Instance.Play("gear_move_low", false);
        yield return new WaitForSeconds(1f);
        _playerCamera.transform.DORotate(_targetRot, 1f).SetEase(Ease.InOutSine);
        yield return new WaitForSeconds(2f);
        machineGunAnimator.speed = 1;
        yield return new WaitForSeconds(3f);
        _playerCamera.transform.DORotate(_targetRot2, 2f);
    }
}
