using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this class specifically handles the hinged door behavior 
public class HingedDoor : MonoBehaviour
{
    public bool isDoorOpen;
    private float closedValue = 0f;
    private float openValue = 90f;
    private float duration = 1f;
    private float doorXPos;
    private void Awake()
    {
        isDoorOpen = false;
    }

    private void Start()
    {
        doorXPos = this.gameObject.transform.localPosition.x;
    }

    public void TriggerHingedDoorEvent(float playerXPos)
    {
        GetComponent<InteractableObject>().interactableStatus.isInteractable = false;
        GetComponent<InteractableObject>().interactableStatus.isHighlightable = false;

        if (!isDoorOpen)
        {
            //open door
            if (doorXPos >= playerXPos)
            {
                LeanTween.rotateY(gameObject, openValue, duration).setEase(LeanTweenType.easeInOutSine).setOnComplete(
                () =>
                {
                    EndAnimation(true);
                }
                );
            }
            else
            {
                LeanTween.rotateY(gameObject, -openValue, duration).setEase(LeanTweenType.easeInOutSine).setOnComplete(
                () =>
                {
                    EndAnimation(true);
                }
                );
            }
        }
        else
        {
            //close door
            isDoorOpen = false;
            LeanTween.rotateY(gameObject, closedValue, duration).setEase(LeanTweenType.easeInOutSine).setOnComplete(
                () =>
                {
                    EndAnimation(false);
                }
                );
        }
    }

    private void EndAnimation(bool _isDoorOpen)
    {
        isDoorOpen = _isDoorOpen;
        GetComponent<InteractableObject>().interactableStatus.isHighlightable = true;
        GetComponent<InteractableObject>().interactableStatus.isInteractable = true;
    }
}
