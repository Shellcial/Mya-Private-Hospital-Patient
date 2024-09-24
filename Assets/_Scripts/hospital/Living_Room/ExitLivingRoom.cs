using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLivingRoom : MonoBehaviour
{
    bool _isTrigger = false;

    void OnTriggerEnter(Collider other)
    {
        if (!_isTrigger){
            _isTrigger = true;
            SceneManager_LivingRoom.Instance.SwitchScene();
        }
    }
}
