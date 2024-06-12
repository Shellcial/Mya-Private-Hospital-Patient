using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ShowPortrait : MonoBehaviour
{
    private float inTime;
    private float outTime;
    private float inTriggerTime = 1f;
    private float outTriggerTime = 1f;
    private GameObject player;  
    private bool isPlayerIn = false;
    private float minY = 70;
    private float maxY = 110;
    private float playerTransformY;
    private bool isPortraitShow = false;
    [SerializeField]
    private float showTime = 2f;
    private Material _portraitMaterial;
    Tween inTween;
    Tween outTween;
    [SerializeField]
    private IPuzzleButton _puzzleButton;
    private void Start(){
        _portraitMaterial = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_puzzleButton.isPressable){
            return;
        }

        // player get in, portrait has hidden
        if (isPlayerIn){
            if (isPortraitShow){
                return;
            }
            outTime = 0;
            playerTransformY = player.transform.localEulerAngles.y;
            if (playerTransformY >= minY && playerTransformY <= maxY){
                inTime += Time.deltaTime;
                if (inTime >= inTriggerTime){
                    RevealPortrait();
                }
            }
        }
        // player get out, portrait has shown
        else{
            if (!isPortraitShow){
                return;
            }
            inTime = 0;
            outTime += Time.deltaTime;
            if (outTime >= outTriggerTime){
                HidePortrait();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        player = other.gameObject;
        isPlayerIn = true;
    }

    void OnTriggerExit(Collider other)
    {
        // Destroy everything that leaves the trigger
        isPlayerIn = false;
    }

    void RevealPortrait(){
        isPortraitShow = true;
        if (outTween != null){
            outTween.Kill();
        }
        inTween = _portraitMaterial.DOFloat(0.85f, "_fade_value", 2f).SetEase(Ease.Linear);
    }

    void HidePortrait(){
        isPortraitShow = false;
        if (inTween != null){
            inTween.Kill();
        }
        outTween = _portraitMaterial.DOFloat(0f, "_fade_value", 2f).SetEase(Ease.Linear);
    }
}
