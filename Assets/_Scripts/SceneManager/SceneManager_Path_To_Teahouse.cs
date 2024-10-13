using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

public class SceneManager_Path_To_Teahouse : Singleton<SceneManager_Path_To_Teahouse>
{
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

		GeneralUIManager.Instance.SetBlack();
		GameManager.Instance.PauseGame();
	}

    async void Start(){
		GameManager.Instance.gameDataManager.UnlockScene("Prologue");
		GameManager.Instance.FadeInAudioMixer(2f);
		FlatAudioManager.Instance.SetAndFade("bird", 2f, 0f, 1f);
		await UniTask.Delay(1000);
		DialogueManager.Instance.ShowDialogueText(true);
		await UniTask.Delay(500);
		GameManager.Instance.ResumeGame();
		GetComponent<PathToTeahouseDialogue>().ClickSentence(true);
		await UniTask.Delay(100);
		DialogueManager.Instance.isDialogueEnable = true;
    }

	public async void ShowPathVisual(string text){
		DialogueManager.Instance.ClearText();
		DialogueManager.Instance.ShowDialogueText(false, 0f);
		DialogueManager.Instance.isDialogueEnable = false;
		await GeneralUIManager.Instance.FadeOutBlack(2f);
		await UniTask.Delay(500);
		DialogueManager.Instance.ShowDialogueText(true);
		await UniTask.Delay(500);
		DialogueManager.Instance.ShowNextSentence(text);
		// GetComponent<PathToTeahouseDialogue>().ClickSentence(true);
		await UniTask.Delay(100);
		DialogueManager.Instance.isDialogueEnable = true;
	}

	public async void ShowRoadSign(string text){
		DialogueManager.Instance.ClearText();
		DialogueManager.Instance.ShowDialogueText(false, 0f);
		DialogueManager.Instance.isDialogueEnable = false;
		DialogueManager.Instance.ShowCloseUp(true, 2f);
		await UniTask.Delay(2000);
		DialogueManager.Instance.ShowDialogueText(true);
		await UniTask.Delay(500);
		DialogueManager.Instance.ShowNextSentence(text);
		// GetComponent<PathToTeahouseDialogue>().ClickSentence(true);
		await UniTask.Delay(100);
		DialogueManager.Instance.isDialogueEnable = true;
	}

    public async void SwitchScene(){
		DialogueManager.Instance.ClearText();
		DialogueManager.Instance.isDialogueEnable = false;
		GameManager.Instance.FadeOutAudioMixer(2f);
		GameManager.Instance.PauseGame();
		DialogueManager.Instance.ShowDialogue(false, 2f);
		await GeneralUIManager.Instance.FadeInBlack(2f);
        SceneManager.LoadScene("Outside_Teahouse");
    }
}
