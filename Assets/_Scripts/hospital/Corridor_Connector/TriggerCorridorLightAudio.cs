using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCorridorLightAudio : MonoBehaviour
{

    [SerializeField]
    private AudioSource _lightFlickeringAmbience;
    [SerializeField]
    private AudioSource _firstLightRepeat;
    [SerializeField]
    private AudioSource _secondLightRepeat;

    public void PlayerLightOn1(){
        FlatAudioManager.Instance.Play("light_on1", false);
        _lightFlickeringAmbience.Play();
    }
    
    public void PlayerLightOn2(){
        FlatAudioManager.Instance.Play("light_on2", false);
    }

    public void PlayerLightOn3(){
        FlatAudioManager.Instance.Play("light_on3", false);
    }

    public void StartLight1Repeat(){
        _firstLightRepeat.Play();
    }

    public void StartLight2Repeat(){
        _secondLightRepeat.Play();
    }
}
