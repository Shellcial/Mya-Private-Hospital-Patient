using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Playables;
using DG.Tweening;
using UnityEngine.Video;
public class MyaEnding : MonoBehaviour
{
    [SerializeField]
    private PlayableDirector _myaEndingDirector;
    private bool isTriggered = false;
    private Vector3 targetCharacterCameraPosition = new Vector3(15.73311f, 1.28f, -4.343f);
    private Vector3 targetRotation = new Vector3(0f, 180f, 0f);
    private Vector3 secondRotation = new Vector3(0f, 0f, 0f);
    [SerializeField]
    private VideoPlayer _bigScreenVideo;
    [SerializeField]
    private Camera characterCamera;

    private void OnTriggerEnter(Collider other)
    {
        if (!isTriggered){
            isTriggered = true;
            GameManager.Instance.PauseGame();
            characterCamera.transform.DOMoveZ(targetCharacterCameraPosition.z, 7.5f).SetEase(Ease.InOutSine);
            characterCamera.transform.DOMoveY(targetCharacterCameraPosition.y, 7.5f).SetEase(Ease.InOutSine);
            characterCamera.transform.DOMoveX(targetCharacterCameraPosition.x, 4f).SetEase(Ease.InOutSine);
            characterCamera.transform.DORotate(targetRotation, 2f).SetEase(Ease.InOutSine);
            StartCoroutine(PlayMyaEnding());
        }
    }

    void Awake(){
        _bigScreenVideo.Prepare();
    }

    IEnumerator PlayMyaEnding(){
        yield return new WaitForSeconds(4f);
        _myaEndingDirector.Play();
        FlatAudioManager.Instance.Play("overhead_door_close", false);
    }

    //  call from director
    public void PlayVideo(){
        _bigScreenVideo.Play();
    }

    public void RotateCamera(){
        characterCamera.transform.DORotate(secondRotation, 1f).SetEase(Ease.Linear);
    }

    //  call from director
    public void EndMyaEnding(){
        GeneralUIManager.Instance.FadeInBlack(0f).Forget();
        GameManager.Instance.gameDataManager.gameData.otherStats["mya_ending"] = true;
        GameManager.Instance.FadeOutAudioMixer(2f);
        StartCoroutine(LeaveEnding());
    }

    IEnumerator LeaveEnding(){
        yield return new WaitForSeconds(3f);
        SceneManager_HospitalLeave.Instance.GoToCredits();
    }
}
