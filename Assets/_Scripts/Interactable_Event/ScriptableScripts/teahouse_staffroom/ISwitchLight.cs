using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class ISwitchLight : MonoBehaviour, IInteractive
{
    [SerializeField]
    private ChangeLight _changeLight;
    private float _triggerCoolDown = 0.5f;

    private bool _isTriggerable = true;
    [SerializeField]
    private List<GameObject> _switches;
    private bool _isOnDisplay = true;
    private void Start(){
        _changeLight = GameManager.Instance.GetComponent<ChangeLight>();
        EnableInteract();
    }

    public void Interact()
    {
        if (_isTriggerable){
            _changeLight.SwitchLight();
            _switches[0].SetActive(_isOnDisplay);
            _isOnDisplay = !_isOnDisplay;
            _switches[1].SetActive(_isOnDisplay);
            StartCoroutine(TriggerCoolDown());
        }
    }

    IEnumerator TriggerCoolDown(){
        _isTriggerable = false;
        yield return new WaitForSeconds(_triggerCoolDown);
        _isTriggerable = true;
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
