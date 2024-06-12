using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;

// class to control fade in/out black/white overlay
public class GeneralUIManager : Singleton<GeneralUIManager>
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private CanvasGroup blackCanvas;
    [SerializeField] private CanvasGroup whiteCanvas;
    const float FADEINDURATION = 1f;
    const float FADEOUTDURATION = 1f;

    public void SetBlack(){
        blackCanvas.alpha = 1;
    }

    public void SetWhite(){
        whiteCanvas.alpha = 1;
    }

    public async UniTask FadeInBlack(float _fadeInDuration = FADEINDURATION, float _waitTime=0f, bool _isBlockRaycast = true)
    {
        canvasGroup.blocksRaycasts = _isBlockRaycast;
        await UniTask.WaitForSeconds(_waitTime);
        await blackCanvas.DOFade(1, _fadeInDuration).SetEase(Ease.InOutSine).AsyncWaitForCompletion(); 
    }

    public async UniTask FadeOutBlack(float _fadeOutDuration = FADEOUTDURATION, float _waitTime=0f, bool _isBlockRaycast = false){
        await UniTask.WaitForSeconds(_waitTime);
        await blackCanvas.DOFade(0, _fadeOutDuration).SetEase(Ease.InOutSine).AsyncWaitForCompletion();
        canvasGroup.blocksRaycasts = _isBlockRaycast;
    }

    public async UniTask FadeInWhite(float _fadeInDuration = FADEINDURATION, float _waitTime=0f, bool _isBlockRaycast = true){
        canvasGroup.blocksRaycasts = _isBlockRaycast;
        await UniTask.WaitForSeconds(_waitTime);
        await whiteCanvas.DOFade(1, _fadeInDuration).SetEase(Ease.InOutSine).AsyncWaitForCompletion();
    }

    public async UniTask FadeOutWhite(float _fadeOutDuration = FADEOUTDURATION, float _waitTime=0f, bool _isBlockRaycast = false){
        await UniTask.WaitForSeconds(_waitTime);
        await whiteCanvas.DOFade(0, _fadeOutDuration).SetEase(Ease.InOutSine).AsyncWaitForCompletion();
        canvasGroup.blocksRaycasts = _isBlockRaycast;
    }
}
