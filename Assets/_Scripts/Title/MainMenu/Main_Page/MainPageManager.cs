using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MainPageManager : ICanvasPage
{
    private bool isFirstHover = true;
    [SerializeField]
    private CanvasGroup _masterHoverLine;
    [SerializeField]
    private Image _hoverTail;
    private float directFadeTime = 0.1f;
    private float lineFadeTime = 0.1f;
    private float moveHoverTime = 0.4f;
    private float fadeWaitTime;

    [SerializeField]
    private List<RawImage> _lineIamges = new List<RawImage>();

    void Awake(){
        fadeWaitTime = moveHoverTime - lineFadeTime * 2f;
        _masterHoverLine.DOFade(0, 0f);
    }

    public void OnHoverMainMenu(int targetIndex){
        float targetPositionY = _lineIamges[targetIndex].transform.position.y;

        if (isFirstHover){
            isFirstHover = false;
            _masterHoverLine.DOFade(1, directFadeTime);
            _hoverTail.DOFade(1,0f);
            _masterHoverLine.transform.position = new Vector2(_masterHoverLine.transform.position.x, targetPositionY);
        }
        else{
            _hoverTail.DOFade(0,lineFadeTime/2);
            _masterHoverLine.transform.DOMoveY(targetPositionY, moveHoverTime).SetEase(Ease.OutQuad);
            _hoverTail.DOFade(1,lineFadeTime).SetDelay(fadeWaitTime);
        }
        
        // move line to current hover index
        
    }

    public void ExitHoverMainMenu(){
        isFirstHover = true;
        _masterHoverLine.DOFade(0, directFadeTime);
    }
}
