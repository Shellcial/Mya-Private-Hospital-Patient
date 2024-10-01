using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GalleryPageManager : ICanvasPage
{
    private bool isClickable = true;
    public List<bool> _isTriggerable = new List<bool>();
    [SerializeField]
    private List<GalleryItem> galleryItems = new List<GalleryItem>();
    [SerializeField]
    private CanvasGroup fullSizeCanvasGroups;
    public List<RawImage> fullSizePhotos = new List<RawImage>();
    public Dictionary<int, List<int>> triggerIndex = new Dictionary<int, List<int>>(){
        {0, new List<int>(){0, 1, 2, 3}},
        {1, new List<int>(){4}},
        {2, new List<int>(){5}},
        {3, new List<int>(){6}},
        {4, new List<int>(){7}},
        {5, new List<int>(){8}},
        {6, new List<int>(){9, 10}},
        {7, new List<int>(){11}},
        {8, new List<int>(){12}},
        {9, new List<int>(){13,14,15,16}},
        {10, new List<int>(){17}},
        {11, new List<int>(){18}},
        {12, new List<int>(){19}},
    };

    [SerializeField]
    private Scrollbar _scrollbars;
    [SerializeField]
    private Slider _slider;
    private int selectedIndex = 0;

    void Awake(){
        LeavePage(0f);
    }

    void Start()
    {
        _isTriggerable.Clear();
        Dictionary<string, bool> illustrationStats = GameManager.Instance.gameDataManager.gameData.IllustrationStats;
        int i = 0;
        foreach (bool isEnable in illustrationStats.Values){
            _isTriggerable.Add(isEnable);
            foreach(int index in triggerIndex[i]){
                galleryItems[index].photoThumbnails.SetActive(isEnable);
                galleryItems[index].unknownNames.SetActive(!isEnable);
                galleryItems[index].revealNames.SetActive(isEnable);
                galleryItems[index].clickButtons.GetComponent<GalleryClick>().imageIndex = index;
                galleryItems[index].clickButtons.SetActive(isEnable);
            }
            
            _isTriggerable.Add(isEnable);
            i++;
        }
    }

    void Update(){
        _slider.value = 1 - _scrollbars.value;
    }

    public void ChoosePhoto(int index){
        isClickable = false;
        fullSizeCanvasGroups.DOFade(1, 0.5f).OnComplete(()=>{
            isClickable = true;
        });
        fullSizeCanvasGroups.interactable = true;
        fullSizeCanvasGroups.blocksRaycasts = true;
        fullSizePhotos[index].gameObject.SetActive(true);
        selectedIndex = index;
    }

    public void ExitPhoto(){
        if (isClickable){
            fullSizeCanvasGroups.DOFade(0, 0.2f).OnComplete(()=>{
                fullSizeCanvasGroups.interactable = false;
                fullSizeCanvasGroups.blocksRaycasts = false;
                fullSizePhotos[selectedIndex].gameObject.SetActive(false);
            });
        }
    }
}
