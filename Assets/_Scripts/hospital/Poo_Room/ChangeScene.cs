using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    private bool _isTriggered = false; 
    void OnTriggerEnter(Collider other){
        if (!_isTriggered){
            _isTriggered = true;
            LeaveRoom();
        }
    }

    void LeaveRoom(){
        GLogger.Log("load scene");
        SceneManager_PooRoom.Instance.SwitchScene();
    }
}
