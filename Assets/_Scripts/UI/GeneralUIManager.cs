using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;
public class GeneralUIManager : Singleton<GeneralUIManager>
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private CanvasGroup blackCanvas;
    [SerializeField] private CanvasGroup whiteCanvas;
    const float fadeInDuration = 1f;
    const float fadeOutDuration = 1f;

    public void SetBlack(){
        blackCanvas.alpha = 1;
    }

    public void SetWhite(){
        whiteCanvas.alpha = 1;
    }

    public async UniTask FadeInBlack(float _fadeInDuration = fadeInDuration, bool _isAutoLock = true)
    {
        if (_isAutoLock)
        {
            canvasGroup.blocksRaycasts = true;
        }

        await blackCanvas.DOFade(1, _fadeInDuration).AsyncWaitForCompletion(); 
    }

    public async UniTask FadeOutBlack(float _fadeOutDuration = fadeOutDuration, bool _isAutoLock = true){
        await blackCanvas.DOFade(0, _fadeOutDuration).AsyncWaitForCompletion();
        
        if (_isAutoLock){
            canvasGroup.blocksRaycasts = false;
        }
    }

    public async UniTask FadeInWhite(float _fadeInDuration = fadeInDuration, bool _isAutoLock = true){
        if (_isAutoLock){
            canvasGroup.blocksRaycasts = true;
        }

        await whiteCanvas.DOFade(1, _fadeInDuration).AsyncWaitForCompletion();
    }

    public async UniTask FadeOutWhite(float _fadeOutDuration = fadeOutDuration, bool _isAutoLock = true){
        await whiteCanvas.DOFade(0, _fadeOutDuration).AsyncWaitForCompletion();
        
        if (_isAutoLock){
            canvasGroup.blocksRaycasts = false;
        }
    }
}
