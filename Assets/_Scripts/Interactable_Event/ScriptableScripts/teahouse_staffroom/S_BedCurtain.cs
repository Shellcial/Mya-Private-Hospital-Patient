using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_BedCurtain : InteractableObject
{
    private Animator animator;
    private bool isOpen;
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.speed = 0f;
        EnableInteract();
    }

    public override void Interact(){
        if (!isOpen){
            isOpen = true;
            animator.speed = 1f;
            DisableInteract();
            StartCoroutine(GetComponent<BedCurtainCollider>().ShrinkCollider());
        }
    }
}
