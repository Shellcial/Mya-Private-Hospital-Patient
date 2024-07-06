using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ILockerCurtain : MonoBehaviour, IInteractive
{
    private Animator animator;
    private bool isOpen;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.speed = 0f;
        EnableInteract();
    }

    public void Interact(){
        if (!isOpen){
            isOpen = true;
            DisableInteract();
            animator.speed = 1f;
            StartCoroutine(GetComponent<LockerCurtainCollider>().ShrinkCollider());
        }
    }

    public void EnableInteract(){
        GetComponent<InteractableObject>().interactableStatus.isInteractable = true;
    }

    public void DisableInteract(){
        // set layer to deafult
        GetComponent<InteractableObject>().interactableStatus.isInteractable = false;
    }

    public void OnDestroy(){
        EnableInteract();
    }
}
