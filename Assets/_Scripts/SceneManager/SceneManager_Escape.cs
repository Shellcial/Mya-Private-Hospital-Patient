using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

public class SceneManager_Escape : Singleton<SceneManager_Escape>
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
	}

    async void Start(){
		GameManager.Instance.FadeInAudioMixer(2f);
		// FlatAudioManager.Instance.SetAndFade("bird", 2f, 0f, 1f);
		await UniTask.Delay(1000);
		DialogueManager.Instance.ShowDialogueText(true);
		await UniTask.Delay(500);
		GameManager.Instance.ResumeGame();
		GetComponent<EscapeDialogue>().ClickSentence(true);
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
        SceneManager.LoadScene("General_Ward");
    }
}
