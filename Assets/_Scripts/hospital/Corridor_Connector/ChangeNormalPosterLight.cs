using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChangeNormalPosterLight : MonoBehaviour
{
    [SerializeField]
    private Animator _normalPosterLights;

    private bool _isTriggered = false;
    [SerializeField]
    private int lightOrder;

    [SerializeField]
    private AudioSource _corridorAmbience;
    void Start(){
        _normalPosterLights.speed = 0;
    }

    void OnTriggerEnter(Collider _collider)
    {
        if (!_isTriggered){
            _isTriggered = true;
            _normalPosterLights.speed = 1;
            switch (lightOrder){
                case 0:
                    _corridorAmbience.Play();
                    FlatAudioManager.Instance.Play("lights_off", false);
                    break;
                case 1:
                    FlatAudioManager.Instance.Play("light_flicker_start", false);
                    break;
                case 2:
                    FlatAudioManager.Instance.Play("light_flicker_start", false);
                    GameManager.Instance.gameDataManager.UnlockIllustration("corridor_posters");
                    break;
                default:
                    GLogger.LogError("index out of range of corridor light trigger");
                    break;
            }
        }
    }
}
