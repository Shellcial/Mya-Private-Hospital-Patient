using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMachineGunPartII : MonoBehaviour
{
    [SerializeField]
    private Animator lightAnimator;   
    [SerializeField]
    private Animator gunPartII_1;
    [SerializeField]
    private Animator gunPartII_2;    
    void Start(){
        lightAnimator.speed = 0;
        gunPartII_1.speed = 0;
        gunPartII_2.speed = 0;
    }

    public void StartLightAnimation(){
        lightAnimator.speed = 1;
    }

    public void StartGunPartIIParts(){
        gunPartII_1.speed = 1;
        gunPartII_2.speed = 1;
    }
}
