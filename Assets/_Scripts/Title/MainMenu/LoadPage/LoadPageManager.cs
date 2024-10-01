using System;
using System.Collections.Generic;
using System.Diagnostics;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadPageManager : ICanvasPage
{
    private List<bool> _isTriggerable = new List<bool>();
    [SerializeField]
    private List<Image> disableImages = new List<Image>();
    private float _animationTime = 0.5f;
    private float _quickFadeTime = 0.1f;
    public RawImage arrow;
    [SerializeField]
    private RawImage clickableLeft;
    [SerializeField]
    private RawImage clickableRight;
    public List<LoadChapter> chapters = new List<LoadChapter>();
    public int currentSelectedTexture = 0;
    [SerializeField]
    private TextMeshProUGUI chapterTitleText;
    [SerializeField]
    private TextMeshProUGUI chapterDescriptionText;
    private List<string> chapterTitle = new List<string>(){
        "序章",
        "幻花茶屋",
        "醫院入口",
        "醫院病房",
        "醫院大廳",
    };
    private List<string> chapterDescription = new List<string>(){
        "序章：你有聽說過街角那間幻花茶屋嗎？",
        "幻花茶屋：雖然茶屋的員工都很可愛，但是食物質素就……",
        "醫院入口：整個大堂也空無一人的米亞私立醫院（幻花茶屋分部）。",
        "醫院病房：醫院內充斥各種臭味及污漬，走廊及房間充滿着熊貓造型的裝飾。",
        "醫院大廳：一群在大廳內看着大螢幕的熊貓，究竟牠們是在等待着甚麼？",
    };
    public List<Texture> loadChapterTextures = new List<Texture>();

    void Awake(){
        SpecialLeavePage();
        _isTriggerable.Clear();
        currentSelectedTexture = 0;
        SetChapterText();

        Dictionary<string, bool> sceneCheckPoints = GameManager.Instance.gameDataManager.gameData.sceneCheckPoints;

        foreach (bool isEnable in sceneCheckPoints.Values){
            _isTriggerable.Add(isEnable);
        }

        for (int i=0; i < chapters.Count; i++){
            EnableChapter(chapters[i], chapters[i]._currentTextureIndex);
        }
    }

    public void EnableChapter(LoadChapter chapter, int textureIndex){
        bool isEnable = _isTriggerable[textureIndex];
        
        chapter.clickButton.interactable = isEnable;
        chapter.blackImage.SetActive(!isEnable);
        chapter.chapterImage.enabled = isEnable;
    }

    void SetChapterText(){
        chapterTitleText.SetText(chapterTitle[currentSelectedTexture]);
        chapterDescriptionText.SetText(chapterDescription[currentSelectedTexture]);
    }

    public void MoveLeft(){
        // left image will move to center, so images are going right actually
        if (!TitleUIManager.Instance.isEnterChapter){     
            GeneralClickEvent(-1);

            foreach(LoadChapter chapter in chapters){
                chapter.Move(true, currentSelectedTexture);
            }

            arrow.DOFade(0,_quickFadeTime).OnComplete(()=>{
                SetChapterText();
            });

            arrow.DOFade(1,_quickFadeTime).SetDelay(_animationTime-_quickFadeTime).OnComplete(()=>{
                clickableLeft.raycastTarget = true;
                clickableRight.raycastTarget = true;
            });
            
        }
    }

    public void MoveRight(){
        // right image will move to center, so images are going left actually
        if (!TitleUIManager.Instance.isEnterChapter){     
            GeneralClickEvent(1);

            foreach(LoadChapter chapter in chapters){
                chapter.Move(false, currentSelectedTexture);
            }

            arrow.DOFade(0,_quickFadeTime).OnComplete(()=>{
                SetChapterText();
            });;

            arrow.DOFade(1,_quickFadeTime).SetDelay(_animationTime-_quickFadeTime).OnComplete(()=>{
                clickableLeft.raycastTarget = true;
                clickableRight.raycastTarget = true;
            });
        }
    }

    public void GeneralClickEvent(int index){
        currentSelectedTexture += index;

        if (currentSelectedTexture < 0){
            currentSelectedTexture = chapterTitle.Count-1;
        }
        else if (currentSelectedTexture >= chapterTitle.Count){
            currentSelectedTexture = currentSelectedTexture - chapterTitle.Count;
        }

        clickableLeft.raycastTarget = false;
        clickableRight.raycastTarget = false;
        chapterTitleText.DOFade(0, _quickFadeTime);
        chapterDescriptionText.DOFade(0, _quickFadeTime);
        chapterTitleText.DOFade(1, _quickFadeTime).SetDelay(_animationTime-_quickFadeTime);
        chapterDescriptionText.DOFade(1, _quickFadeTime).SetDelay(_animationTime-_quickFadeTime);
    }

    public void SpecialEnterPage(){
        _canvasGroup.gameObject.SetActive(true);
        _canvasGroup.DOFade(1, 0.2f);
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;
    }

    public void SpecialLeavePage(){
        _canvasGroup.DOFade(0, 0.2f).OnComplete(()=>{
            _canvasGroup.gameObject.SetActive(false);
        });
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
    }
}
