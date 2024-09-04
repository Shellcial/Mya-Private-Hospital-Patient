using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Video;

public class ReadyLive : MonoBehaviour
{
    [SerializeField]
    private List<Light> _allLights = new List<Light>();
    [SerializeField]
    private List<Light> _allSpots = new List<Light>();
    private float _fadeTime = 3f;
    private bool _isTrigger = false;
    [SerializeField]
    private Renderer _livingRoomScreenRenderer;

    [SerializeField]
    private RenderTexture _liveReadyTexture;
    [SerializeField]
    private AudioSource _liveReadyAudio;
    [SerializeField]
    private VideoPlayer _liveReadyVideo;

    void OnTriggerEnter(Collider other)
    {
        if (!_isTrigger){
            _isTrigger = true;
            // GameManager.Instance.PauseGame();
            // play audio
            StartCoroutine(StartAnimation());
        }
    }

    IEnumerator StartAnimation(){
        yield return new WaitForSeconds(3f);
        foreach (Light light in _allLights){
            light.DOIntensity(1f, _fadeTime);
        }

        foreach (Light light in _allSpots){
            light.DOIntensity(0.02f, _fadeTime);
        }

        yield return new WaitForSeconds(_fadeTime);

        // change video
        _livingRoomScreenRenderer.material.SetTexture("_EmissionMap", _liveReadyTexture);
        _liveReadyAudio.Play();
        _liveReadyVideo.Play();
        
        yield return new WaitForSeconds(3f);
        // GameManager.Instance.ResumeGame();
    }
}
