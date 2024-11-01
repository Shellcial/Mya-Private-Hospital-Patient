using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BTSPageManager : ICanvasPage
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
    [SerializeField]
    private List<GameObject> _btsPages = new List<GameObject>();
    [SerializeField]
    private List<Scrollbar> _scrollbars = new List<Scrollbar>();
    [SerializeField]
    private List<Button> buttons = new List<Button>();
    private List<bool> _isTriggerable = new List<bool>();
    private int LastSelectedIndex; 

    [SerializeField]
    private Slider _slider;

    void Awake(){
        LeavePage(0f);
        fadeWaitTime = moveHoverTime - lineFadeTime * 2f;
        _masterHoverLine.DOFade(0, 0f);
    }

    void Start(){
        // get bool from Game manager save file 
        OpenBTS(-1);
        _isTriggerable.Clear();
        Dictionary<string, bool> cardStats = GameManager.Instance.gameDataManager.gameData.cardStats;
        int i = 0;
        foreach (bool isEnable in cardStats.Values){
            _isTriggerable.Add(isEnable);
            buttons[i].interactable = isEnable;
            buttons[i].GetComponent<Image>().raycastTarget = isEnable;
            i++;
        }
    }

    public void OnHoverBTS(int targetIndex){
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

    public void ExitBTSPage(){
        LeavePage();
        ExitHoverBTS();
    }

    public void ExitHoverBTS(){
        isFirstHover = true;
        _masterHoverLine.DOFade(0, directFadeTime);
    }

    public void OpenBTS(int index){
        for (int i = 0; i < _btsPages.Count; i++){
            _btsPages[i].SetActive(i == index);
            if (i == index){
                _scrollbars[index].value = 1;
            }
        }
        LastSelectedIndex = index;
    }

    void Update(){
        if (LastSelectedIndex >= 0){
            _slider.value = 1 - _scrollbars[LastSelectedIndex].value;
        }
    }
}
