using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpeaker : MonoBehaviour
{
    public void PlaySpeakerMoveSound(){
        FlatAudioManager.instance.Play("speaker_move", false);
    }
}
