using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CutAnimationManager : MonoBehaviour
{
    [SerializeField]
    private List<Animator> _animatorList = new List<Animator>();
    private bool _isTrigger = false;
    private bool _isPlaying = false;
    [SerializeField]
    private Camera _playerCamera;
    private Vector3 startCameraPosition;
    private Vector3 startCameraRotation;
    private float startFieldOfView = 50f;
    private Vector3 targetCameraPosition = new Vector3(-72.85683f, 1.173511f, 94.30684f);
    private Vector3 targetCameraAngles = new Vector3(-1.531833f, -0.2514263f, 3.361013f);
    private float targetFieldOfView = 22.8f;
    private float _inAnimationTime = 5f;
    private float _outAnimationTime = 3f;
    [SerializeField]
    private Transform _animatedCameraTransform;
    void Start()
    {
        foreach (Animator animator in _animatorList){
            animator.speed = 0f;
        }
    }

    void Update(){
        if (_isPlaying){
            UpdateCameraPosition();
        }
    }

    void UpdateCameraPosition(){
        _playerCamera.transform.position = _animatedCameraTransform.transform.position;
        _playerCamera.transform.eulerAngles = _animatedCameraTransform.transform.eulerAngles;
    }

    void OnTriggerEnter(Collider other){
        if (!_isTrigger){
            GameManager.Instance.PauseGame();
            MoveCamera();
            _isTrigger = true;        
        }
    }

    void MoveCamera(){
        startCameraPosition = _playerCamera.transform.position;
        startCameraRotation = _playerCamera.transform.eulerAngles;
        _playerCamera.transform.DOMove(targetCameraPosition, _inAnimationTime).SetEase(Ease.InOutSine).OnComplete(()=>{
            StartCoroutine(StartAnimation());
        });
        _playerCamera.transform.DORotate(targetCameraAngles, _inAnimationTime).SetEase(Ease.InOutSine);
        _playerCamera.DOFieldOfView(targetFieldOfView, _inAnimationTime).SetEase(Ease.InOutSine);
    }

    IEnumerator StartAnimation(){
        yield return new WaitForSeconds(3f);
        _isPlaying = true;
        foreach (Animator animator in _animatorList){
            animator.speed = 1f;
        }
    }

    public void ReturnPosition(){
        _isPlaying = false;
        _playerCamera.transform.DOMove(startCameraPosition, _outAnimationTime).SetEase(Ease.InOutSine).OnComplete(()=>{
            GameManager.Instance.ResumeGame();
        });
        _playerCamera.transform.DORotate(startCameraRotation, _outAnimationTime).SetEase(Ease.InOutSine);
        _playerCamera.DOFieldOfView(startFieldOfView, _outAnimationTime).SetEase(Ease.InOutSine);
        // startCameraPosition
    }
}
