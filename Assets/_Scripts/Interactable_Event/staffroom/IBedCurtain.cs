using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IBedCurtain : MonoBehaviour, IInteractive
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
            animator.speed = 1f;
            DisableInteract();
            StartCoroutine(GetComponent<BedCurtainCollider>().ShrinkCollider());
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
