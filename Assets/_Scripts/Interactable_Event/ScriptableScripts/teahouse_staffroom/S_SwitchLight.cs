using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Tayx.Graphy.Audio;
using UnityEngine;

public class S_SwitchLight : InteractableObject
{
    [SerializeField]
    private ChangeLight _changeLight;
    private float _triggerCoolDown = 0.5f;

    private bool _isTriggerable = true;
    [SerializeField]
    private List<GameObject> _switches;
    private bool _isOnDisplay = true;
    private void Start(){
        _changeLight = SceneManager_TeahouseStaffroom.Instance.GetComponent<ChangeLight>();
        EnableInteract();
    }

    public override void Interact()
    {
        if (_isTriggerable){
            FlatAudioManager.Instance.Play("light_button", false);
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

}
