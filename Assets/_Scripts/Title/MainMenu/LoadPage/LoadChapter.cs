using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections;

public class LoadChapter : MonoBehaviour
{
    public int _currentPositionIndex = 0;
    public int _currentTextureIndex = 0;
    [SerializeField]
    private Canvas _canvas;
    [SerializeField]
    private CanvasGroup _canvasGroup;

    [SerializeField]
    private RectTransform _rectTransform;
    public RawImage chapterImage;
    public GameObject blackImage;
    public RawImage farOverlay;
    public Button clickButton;
    private float fadeAnimationTime = 0.5f;
    private List<Vector2> targetPos = new List<Vector2>(){
        new Vector2(-1447f, 232.9421f),
        new Vector2(-1087.3f, 232.9421f),
        new Vector2(-514.2f, 232.9421f),
        new Vector2(29.6328f, 232.9421f),
        new Vector2(572.2f, 232.9421f),
        new Vector2(1077.1f, 232.9421f),
        new Vector2(1415f, 232.9421f),
    };

    private List<Vector2> targetScale = new List<Vector2>(){
        new Vector2(0.4f, 0.4f),
        new Vector2(0.615f, 0.615f),
        new Vector2(0.855f, 0.855f),
        new Vector2(1,1),
        new Vector2(0.855f, 0.855f),
        new Vector2(0.615f, 0.615f),
        new Vector2(0.4f, 0.4f),
    };
    private List<Texture> _loadChapterTextures = new List<Texture>();

    void Awake(){
        _loadChapterTextures = TitleUIManager.Instance.loadPageManager.loadChapterTextures;
        AdjustIndex();
    }

    public void Move(bool isMoveleft, int centerIndex){
        int imageChangeIndex = centerIndex;

        if (isMoveleft){
            // change Position
            _currentPositionIndex += 1;
            AdjustIndex();
            _rectTransform.DOLocalMove(targetPos[_currentPositionIndex], fadeAnimationTime).SetEase(Ease.OutSine);
            _rectTransform.DOScale(targetScale[_currentPositionIndex], fadeAnimationTime).SetEase(Ease.OutSine);
            
            // assign new image
            if (_currentPositionIndex == 1){
                imageChangeIndex = AdjustImageIndex(imageChangeIndex+3);
                chapterImage.texture = _loadChapterTextures[imageChangeIndex];
                _currentTextureIndex = imageChangeIndex;
                TitleUIManager.Instance.loadPageManager.EnableChapter(this, _currentTextureIndex);
            }
        }
        else {
            // change Position
            _currentPositionIndex -= 1;
            AdjustIndex();
            
            _rectTransform.DOLocalMove(targetPos[_currentPositionIndex], fadeAnimationTime).SetEase(Ease.OutSine);
            _rectTransform.DOScale(targetScale[_currentPositionIndex], fadeAnimationTime).SetEase(Ease.OutSine);

            // assign new image
            if (_currentPositionIndex == targetPos.Count-2){
                imageChangeIndex = AdjustImageIndex(imageChangeIndex - 3);
                chapterImage.texture = _loadChapterTextures[imageChangeIndex];
                _currentTextureIndex = imageChangeIndex;
                TitleUIManager.Instance.loadPageManager.EnableChapter(this, _currentTextureIndex);
            }
        }
        
    }

    public int AdjustImageIndex(int imageChangeIndex){
        if (imageChangeIndex >= _loadChapterTextures.Count){
            return imageChangeIndex - _loadChapterTextures.Count;
        }
        else if (imageChangeIndex < 0){
            return _loadChapterTextures.Count+imageChangeIndex;
        }
        else {
            return imageChangeIndex;
        }
    }

    public async void AdjustIndex(){
        if (_currentPositionIndex >= targetPos.Count){
            _currentPositionIndex = 0;
        }
        else if (_currentPositionIndex < 0){
            _currentPositionIndex = targetPos.Count - 1;
        };
        
        switch (_currentPositionIndex){
            case 0:
                _canvasGroup.DOFade(0, fadeAnimationTime);
                StartCoroutine(ChangeSortingOrder(-1));
                clickButton.interactable = false;
                break;
            case 1:
                farOverlay.DOFade(0.9f, fadeAnimationTime);
                _canvasGroup.DOFade(1, fadeAnimationTime);
                StartCoroutine(ChangeSortingOrder(0));
                clickButton.interactable = false;
                break;
            case 2:
                farOverlay.DOFade(0.5f, fadeAnimationTime);
                StartCoroutine(ChangeSortingOrder(1));
                clickButton.interactable = false;
                break;
            case 3:
                farOverlay.DOFade(0, fadeAnimationTime);
                StartCoroutine(ChangeSortingOrder(2));
                clickButton.interactable = true;
                break;
            case 4:
                farOverlay.DOFade(0.5f, fadeAnimationTime);
                StartCoroutine(ChangeSortingOrder(1));
                clickButton.interactable = false;
                break;
            case 5:
                farOverlay.DOFade(0.9f, fadeAnimationTime);
                _canvasGroup.DOFade(1, fadeAnimationTime);
                StartCoroutine(ChangeSortingOrder(0));
                clickButton.interactable = false;
                break;
            case 6:
                _canvasGroup.DOFade(0, fadeAnimationTime);
                StartCoroutine(ChangeSortingOrder(-1));
                clickButton.interactable = false;
                break;
            default:
                GLogger.LogError("load index out of range: " + _currentPositionIndex);
                break;
        }
    }

    IEnumerator ChangeSortingOrder(int _newOrder){
        yield return new WaitForSeconds(fadeAnimationTime/2);
        _canvas.sortingOrder = _newOrder;
    }
}
