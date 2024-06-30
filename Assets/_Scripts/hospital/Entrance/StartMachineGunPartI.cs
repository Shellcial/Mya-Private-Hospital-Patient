using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMachineGunPartI : MonoBehaviour
{
    [SerializeField]
    private Animator machineGunPartIIAnimator;   
    void Start(){
        this.GetComponent<Animator>().speed = 0;
        machineGunPartIIAnimator.speed = 0;
    }

    public void StartGunPartII(){
        machineGunPartIIAnimator.speed = 1;
    }
}
