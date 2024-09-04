using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMyaGeneralWardPoster : MonoBehaviour
{
    private bool _isTriggered = false;
    void OnTriggerEnter(Collider other){
        if (!_isTriggered){
            _isTriggered = true;
            GameManager.Instance.gameDataManager.UnlockIllustration("general_ward_mya_poster");
        }
    }
}
