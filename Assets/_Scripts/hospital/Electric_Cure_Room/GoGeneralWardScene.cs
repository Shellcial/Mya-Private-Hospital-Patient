using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoGeneralWardScene : MonoBehaviour
{
    private bool _isTriggered = false; 
    void OnTriggerEnter(Collider other){
        if (!_isTriggered){
            _isTriggered = true;
            LeaveRoom();
        }
    }

    void LeaveRoom(){
        SceneManager_PooRoom.Instance.SwitchScene();
    }
}
