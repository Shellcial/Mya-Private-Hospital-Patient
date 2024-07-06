using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ILabels : MonoBehaviour, IInteractive
{
    [SerializeField]
    private int _labelIndex;
    public void EnableInteract(){
        GetComponent<InteractableObject>().interactableStatus.isInteractable = true;
    }

    public void DisableInteract(){
        // set layer to deafult
        GetComponent<InteractableObject>().interactableStatus.isInteractable = false;
    }

    public void Interact()
    {
        SceneManager_HospitalEntrance.Instance.carbinetPassword.PassPassword(_labelIndex);
    }

    public void OnDestroy()
    {
        EnableInteract();
    }
}
