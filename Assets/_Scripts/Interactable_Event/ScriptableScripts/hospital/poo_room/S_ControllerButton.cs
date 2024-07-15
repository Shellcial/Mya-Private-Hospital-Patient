using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_ControllerButton : InteractableObject
{
    [SerializeField]
    private int buttonIndex;

    [SerializeField]
    private ControllerPassword _controllerPassword;

    public void SetButtonIndex(int _index){
        buttonIndex = _index;
    }

    public override void Interact()
    {
        _controllerPassword.ReceivePassword(buttonIndex);
    }
}
