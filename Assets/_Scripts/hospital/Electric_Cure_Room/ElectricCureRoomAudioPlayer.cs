using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricCureRoomAudioPlayer : MonoBehaviour
{
    public void PlayLight01(){
        FlatAudioManager.Instance.Play("panda_run_light_flicker_01", false);    
    }

    public void PlayLight033(){
        FlatAudioManager.Instance.Play("panda_run_light_flicker_033", false);    
    }

    public void PlayLight04(){
        FlatAudioManager.Instance.Play("panda_run_light_flicker_04", false);    
    }

    public void PlayCutPaperFall(){
        FlatAudioManager.Instance.Play("cut_paper_fall", false);
    }

    public void PlayPandaRun(){
        FlatAudioManager.Instance.Play("run_footsteps", false);  
    }

    public void PlayMyaRun(){
        FlatAudioManager.Instance.Play("mya_run_footsteps", false);
    }

    public void PlayMyaPunch(){
        FlatAudioManager.Instance.Play("mya_punch", false);
        FlatAudioManager.Instance.Stop("horror_ambience2");
        FlatAudioManager.Instance.Stop("horror_ambience3");
    }

    public void PlayPandaCollision(){
        FlatAudioManager.Instance.Play("mya_body_collision", false);
    }

    public void PlayMyaWalkAway(){
        FlatAudioManager.Instance.Play("mya_away_footsteps", false);
    }

    public void OpenMetalDoor(){
        FlatAudioManager.Instance.Play("metal_door_open", false);
    }

    public void CloseMetalDoor(){
        FlatAudioManager.Instance.Play("metal_door_close", false);
    }
}
