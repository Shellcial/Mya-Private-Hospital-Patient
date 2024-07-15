using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Labels : InteractableObject
{
    [SerializeField]
    private int _labelIndex;
 
    public override void Interact()
    {
        SceneManager_HospitalEntrance.Instance.carbinetPassword.PassPassword(_labelIndex);
    }
}
