using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class StartMachineGunPartII : MonoBehaviour
{
    [SerializeField]
    private Animator lightAnimator;   
    [SerializeField]
    private GameObject _pandaSmoke;
    [SerializeField]
    private Animator gunPartII_1;
    [SerializeField]
    private Animator gunPartII_2;    
    [SerializeField]
    private StartMachineGunPartIII _machineGunPartIII;
    void Start(){
        lightAnimator.speed = 0;
        gunPartII_1.speed = 0;
        gunPartII_2.speed = 0;
    }

    public void StartLightAnimation(){
        lightAnimator.speed = 1;
        FlatAudioManager.instance.Play("alarm", false);
    }

    public void StartGunPartIIParts(){
        gunPartII_1.speed = 1;
        gunPartII_2.speed = 1;
    }

    public void StartGunPartIIIParts(){
        StartCoroutine(_machineGunPartIII.StartAnimation());
    }

    public void GoBlack(){
        FlatAudioManager.instance.Stop("smoke");
        FlatAudioManager.instance.Stop("shake");
        FlatAudioManager.instance.Stop("alarm");
        FlatAudioManager.instance.Stop("gear_move_high");
        FlatAudioManager.instance.Stop("machine_move");
        FlatAudioManager.instance.Stop("machine_move2");
        GeneralUIManager.Instance.FadeInBlack(0).Forget();
        GameManager.Instance.FadeOutAudioMixer(2f);
        StartCoroutine(GoToNextScene());
    }

    IEnumerator GoToNextScene(){
        // fade audios before going to next scene
        yield return new WaitForSeconds(3f);
        SceneManager_HospitalEntrance.Instance.SwitchScene();
    }

    public void StartSmoke(){
        _pandaSmoke.SetActive(true);
        FlatAudioManager.instance.SetAndFade("smoke", 1f, 0, 0.5f);
    }

    public void PlayScan(){
        FlatAudioManager.instance.Play("scan", false);
    }
    public void StopScan(){
        FlatAudioManager.instance.Stop("scan");
    }

    public void PlayShake(){
        FlatAudioManager.instance.Play("shake", false);
    }

    public void PlayShoot(){
        FlatAudioManager.instance.Play("shoot", false);
    }
}
