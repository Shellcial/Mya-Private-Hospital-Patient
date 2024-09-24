using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpeaker : MonoBehaviour
{
    public void PlaySpeakerMoveSound(){
        FlatAudioManager.Instance.Play("speaker_move", false);
    }
}
