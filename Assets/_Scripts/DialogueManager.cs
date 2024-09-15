using UnityEngine;
using DG.Tweening;
using TMPro;
using System.Collections;
public class DialogueManager : Singleton<DialogueManager>
{

    private CanvasGroup _dialogueCanvasGroup; 
    [SerializeField]
    private CanvasGroup _closeUpCanvasGroup;
    [SerializeField]
    private CanvasGroup _dialogueTextCanvasGroup;
    [SerializeField]
    private TextMeshProUGUI _dialogueText;
    public bool isDialogueEnable = false;
    public bool isSentencePlaying = false; 
    private string showingWords = "";
    private Coroutine textCoroutine;
    protected override void Awake(){
        isDialogueEnable = false;
        isSentencePlaying = false;
        ShowCloseUp(false, 0f);
        ShowDialogueText(false, 0f);

        if (Instance == null){
            Instance = this;
        }
        else{
            Destroy(this.gameObject);
            return;
        }

        _dialogueCanvasGroup = GetComponent<CanvasGroup>();
    }
    
    public void ShowDialogue(bool isShow, float duration = 0.5f){
        if (isShow){
            _dialogueCanvasGroup.DOFade(1, duration);
        }
        else {
            _dialogueCanvasGroup.DOFade(0, duration);
        }
    }


    public void ShowCloseUp(bool isShow, float duration = 0.5f){
        if (isShow){
            _closeUpCanvasGroup.DOFade(1, duration);
        }
        else {
            _closeUpCanvasGroup.DOFade(0, duration);
        }
    }

    public void ShowDialogueText(bool isShow, float duration = 0.5f){
        if (isShow){
            _dialogueTextCanvasGroup.DOFade(1, duration);
        }
        else {
            _dialogueTextCanvasGroup.DOFade(0, duration);
        }
    }

    public void JumpSentence(string newWords){
        if (textCoroutine != null){
            StopCoroutine(textCoroutine);
        }
        _dialogueText.SetText(newWords);
        isSentencePlaying = false;
    }

    public void ShowNextSentence(string newWords, float delay = 0.05f){
        showingWords = newWords;
        textCoroutine = StartCoroutine(ShowText(delay));
    }

    IEnumerator ShowText(float delay = 0.1f){
        isSentencePlaying = true;
        for (int i = 0; i < showingWords.Length; i++){
            _dialogueText.SetText(showingWords.Substring(0, i));
            yield return new WaitForSeconds(delay);
        }
        isSentencePlaying = false;
    }

    public void ClearText(){
        _dialogueText.SetText("");
    }
}
