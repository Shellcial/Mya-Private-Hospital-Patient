using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMachineGunPartAudio : MonoBehaviour
{
    public void PlayPipeExtend(){
        FlatAudioManager.Instance.Play("machine_move", false);
    }

    public void PlayPipeTransform(){
        FlatAudioManager.Instance.Play("machine_move2", false);
    }
}
