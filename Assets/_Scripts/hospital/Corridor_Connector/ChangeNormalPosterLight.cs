using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChangeNormalPosterLight : MonoBehaviour
{
    [SerializeField]
    private Animator _normalPosterLights;

    private bool _isTriggered = false;
    void Start(){
        _normalPosterLights.speed = 0;
    }

    void OnTriggerEnter(Collider _collider)
    {
        if (!_isTriggered){
            _isTriggered = true;
            _normalPosterLights.speed = 1;
            GameManager.Instance.gameDataManager.UnlockIllustration("corridor_posters");
        }
    }
}
