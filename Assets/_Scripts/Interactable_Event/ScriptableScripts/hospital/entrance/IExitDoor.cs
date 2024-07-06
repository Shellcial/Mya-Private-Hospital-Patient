using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IExitDoor : MonoBehaviour, IInteractive
{
    private float openValue = -90f;
    private bool isOpen = false;
    private float duration = 3f;
    
    public void DisableInteract()
    {
        GetComponent<InteractableObject>().interactableStatus.isInteractable = false;
    }

    public void EnableInteract()
    {
        GetComponent<InteractableObject>().interactableStatus.isInteractable = true;
    }

    public void Interact()
    {
        if (!isOpen){
            //open door
            DisableInteract();

            isOpen = true;
            Vector3 targetValue = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y - openValue, transform.eulerAngles.z);
            GameManager.Instance.PauseGame();
            transform.DORotate(targetValue, duration, RotateMode.Fast).SetEase(Ease.InOutSine).OnComplete(
                () =>
                {
                    SceneManager.LoadScene("Gummy_Road");
                }
            );

            GeneralUIManager.Instance.FadeInBlack(2f).Forget();
        }   
    }

    public void OnDestroy()
    {
        EnableInteract();
    }

}
