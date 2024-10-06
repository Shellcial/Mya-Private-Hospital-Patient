using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class SceneManager_Gummy_Road : Singleton<SceneManager_Gummy_Road>
{
    [SerializeField]
    private RawImage gradualBlack1;
    [SerializeField]
    private RawImage gradualBlack2;
    [SerializeField]
    private RawImage wholeBlack;
    [SerializeField]
    private RawImage gummyImage;
	protected override void Awake()
	{
		if( !Instance )
		{
			Instance = this;
		}
		else if( Instance != this )
		{
			Destroy( gameObject );
			return;     
		}

        gradualBlack1.DOFade(0,0f);
        gradualBlack2.DOFade(0,0f);
        wholeBlack.DOFade(0,0f);
        gummyImage.DOFade(0,0f);

		GeneralUIManager.Instance.SetBlack();
		GameManager.Instance.PauseGame();
		GameManager.Instance.LockCursor(true);
	}

    async void Start(){
		GameManager.Instance.FadeInAudioMixer(2f);
        FlatAudioManager.Instance.SetAndFade("ambience_horror", 2f, 0f, 0.2f);
        await GeneralUIManager.Instance.FadeOutBlack(2f);
		DialogueManager.Instance.ShowDialogue(true);
        DialogueManager.Instance.ShowDialogueText(true);
		await UniTask.Delay(500);
		DialogueManager.Instance.isDialogueEnable = true;
		GetComponent<GummyRoadDialogue>().ClickSentence();
    }

    public async void PlayFootstepSounds(string text){
        DialogueManager.Instance.isDialogueEnable = false;
        FlatAudioManager.Instance.Play("panda_footsteps", false);
        await UniTask.Delay(1000);
        DialogueManager.Instance.isDialogueEnable = true;
        DialogueManager.Instance.ShowNextSentence(text);
        // GetComponent<GummyRoadDialogue>().ClickSentence();
    }

    public void FadeBlack1(string text){
        DialogueManager.Instance.ClearText();
        DialogueManager.Instance.isDialogueEnable = false;
        gradualBlack1.DOFade(0.5f, 1f).OnComplete(()=>{
            DialogueManager.Instance.isDialogueEnable = true;
            DialogueManager.Instance.ShowNextSentence(text);
            // GetComponent<GummyRoadDialogue>().ClickSentence();
        });
    }

    public void FadeBlack2(string text){
        wholeBlack.DOFade(0.5f, 3f);
        DialogueManager.Instance.ShowNextSentence(text);
    }

    public void FadeBlack3(string text){
        DialogueManager.Instance.ClearText();
        DialogueManager.Instance.isDialogueEnable = false;
        gradualBlack1.DOFade(1f, 2f);
        gradualBlack2.DOFade(1f, 2f).OnComplete(()=>{
            DialogueManager.Instance.isDialogueEnable = true;
            DialogueManager.Instance.ShowNextSentence(text);
            // GetComponent<GummyRoadDialogue>().ClickSentence();
        });
    }

    public async void ShowGummy(string text){
        FlatAudioManager.Instance.Play("gummy_footsteps", false);
        DialogueManager.Instance.isDialogueEnable = false;
        wholeBlack.DOFade(0.5f, 2f);
        gummyImage.DOFade(1, 2f);
        await UniTask.Delay(500);
        DialogueManager.Instance.isDialogueEnable = true;
        // GetComponent<GummyRoadDialogue>().ClickSentence();
        DialogueManager.Instance.ShowNextSentence(text);
    }

    public async void SwitchScene(){
        DialogueManager.Instance.ClearText();
        DialogueManager.Instance.isDialogueEnable = false;
		GameManager.Instance.FadeOutAudioMixer(2f);
		DialogueManager.Instance.ShowDialogue(false,2f);
		await GeneralUIManager.Instance.FadeInBlack(2f);
        SceneManager.LoadScene("Ending_Credits");
    }
}
