using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PandaDown : MonoBehaviour
{
    private bool isTriggered = false;
    void OnCollisionEnter(Collision other){
        GLogger.Log("ground collided: " + other.gameObject.name);
        if (!isTriggered){
            if (other.gameObject.layer == LayerMask.NameToLayer("PandaHead")){
                isTriggered = true;
                FlatAudioManager.instance.Play("head_drop", false);
            }
        }
    }

}
