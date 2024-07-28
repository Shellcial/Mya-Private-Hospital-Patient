using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackCutAnimation : MonoBehaviour
{
    [SerializeField]
    private CutAnimationManager _cutAnimationManager;

    public void StartBackCutAnimation(){
        _cutAnimationManager.ReturnPosition();
    }
}
