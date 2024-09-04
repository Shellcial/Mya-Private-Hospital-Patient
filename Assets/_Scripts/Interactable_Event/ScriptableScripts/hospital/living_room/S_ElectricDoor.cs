using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class S_ElectricDoor : InteractableObject
{
    private bool isTrigger = false;
    [SerializeField]
    private Transform _leftDoor; 
    [SerializeField]
    private Transform _rightDoor;
    private float _targetLeftZ = -10.876f; 
    private float _targetrightZ = -7.169f;
    private float _doorOpenTime = 3f;

    private Color _color = new Color(0,1,0,1);
    [SerializeField]
    private Renderer _lightRenderer;
    
    private 
    void Start(){
        EnableInteract();
    }
    
    public override void Interact()
    {
        if (SceneManager_LivingRoom.Instance.isGetElectricKey && !isTrigger){
            isTrigger = true;
            _leftDoor.DOLocalMoveZ(_targetLeftZ, _doorOpenTime).SetEase(Ease.InOutSine);
            _rightDoor.DOLocalMoveZ(_targetrightZ, _doorOpenTime).SetEase(Ease.InOutSine);
            _lightRenderer.material.SetColor("_EmissionColor", _color);
            DisableInteract();
        }
    }
}
