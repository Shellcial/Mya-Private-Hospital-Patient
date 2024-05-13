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
    }

    public void Interact(){
        if (!isOpen){
            isOpen = true;
            animator.speed = 1f;
            StartCoroutine(GetComponent<BedCurtainCollider>().ShrinkCollider());
        }
    }
}
