using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_ExitDoor : InteractableObject
{
    private float openValue = -90f;
    private bool isOpen = false;
    private float duration = 3f;

    public override void Interact()
    {
        if (!isOpen){
            //open door
            DisableInteract();

            isOpen = true;
            Vector3 targetValue = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y - openValue, transform.eulerAngles.z);
            GameManager.Instance.PauseGame();
            GameManager.Instance.FadeOutAudioMixer(3f);
            GeneralUIManager.Instance.FadeInBlack(3f).Forget();
            transform.DORotate(targetValue, duration).SetEase(Ease.InOutSine).OnComplete(
                () =>
                {
                    SceneManager_HospitalLeave.Instance.GoToGummyEnding();
                }
            );
        }   
    }
}
