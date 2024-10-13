using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using Cysharp.Threading.Tasks;

public class SceneManager_OutsideTeahouse : Singleton<SceneManager_OutsideTeahouse>
{

    [SerializeField]
    private Camera _outsideCamera;
    [SerializeField]
    private Camera _insideCamera;
    [SerializeField]
    private GameObject coffee;
    [SerializeField]
    private Renderer distortionMaterial;
    [SerializeField]
    private Light directionalLight;
    override protected void Awake(){
        InitializeScene();

		GeneralUIManager.Instance.SetBlack();
		GameManager.Instance.PauseGame();
	}

    async void Start(){
		GameManager.Instance.FadeInAudioMixer(2f);
        FlatAudioManager.Instance.SetAndFade("bird", 2f, 0f, 1f);
        await GeneralUIManager.Instance.FadeOutBlack(2f);
		DialogueManager.Instance.ShowDialogue(true);
        DialogueManager.Instance.ShowDialogueText(true);
		await UniTask.Delay(500);
		DialogueManager.Instance.isDialogueEnable = true;
        GameManager.Instance.ResumeGame();
		GetComponent<OutsideTeahouseDialogue>().ClickSentence();
    }

    public void InitializeScene()
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

        directionalLight.intensity = 1;

        _outsideCamera.gameObject.SetActive(true);
        _insideCamera.gameObject.SetActive(false);
    }

	public async void GetInHouse(string  text){
		DialogueManager.Instance.ClearText();
		DialogueManager.Instance.ShowDialogue(false, 1f);
		DialogueManager.Instance.isDialogueEnable = false;
		await GeneralUIManager.Instance.FadeInBlack(1f);
        _outsideCamera.gameObject.SetActive(false);
        _insideCamera.gameObject.SetActive(true);
        directionalLight.intensity = 2;
        await UniTask.Delay(1000);
        FlatAudioManager.Instance.Play("place_cup", false);
        await UniTask.Delay(500);
        await GeneralUIManager.Instance.FadeOutBlack(1f);
		DialogueManager.Instance.ShowDialogue(true);
        await UniTask.Delay(500);
        DialogueManager.Instance.ShowNextSentence(text);
		// GetComponent<OutsideTeahouseDialogue>().ClickSentence(true);
        await UniTask.Delay(100);
		DialogueManager.Instance.isDialogueEnable = true;
	}

    public async void TakeCoffee(string  text){
        DialogueManager.Instance.ClearText();
		DialogueManager.Instance.isDialogueEnable = false;
        FlatAudioManager.Instance.Play("place_cup", false);
        coffee.SetActive(false);
        await UniTask.Delay(500);
        DialogueManager.Instance.ShowNextSentence(text);
        // GetComponent<OutsideTeahouseDialogue>().ClickSentence(true);
        await UniTask.Delay(100);
        DialogueManager.Instance.isDialogueEnable = true;
    } 

    public async void FeelDizzy(string  text){
        DialogueManager.Instance.ClearText();
		DialogueManager.Instance.isDialogueEnable = false;
        distortionMaterial.material.DOFloat(0.01f, "_distortion_strength",2f);
        await UniTask.Delay(1000);
        DialogueManager.Instance.ShowNextSentence(text);
        // GetComponent<OutsideTeahouseDialogue>().ClickSentence(true);
        await UniTask.Delay(100);
        DialogueManager.Instance.isDialogueEnable = true;
    }

    public async void GoToilet(string  text){
        DialogueManager.Instance.ClearText();
        DialogueManager.Instance.isDialogueEnable = false;
        DialogueManager.Instance.ShowDialogue(false, 2f);
        await GeneralUIManager.Instance.FadeInBlack(2f);
        FlatAudioManager.Instance.Play("toilet_flush", false);
        await UniTask.Delay(5000);
        FlatAudioManager.Instance.Play("wash_hand", false);
        await UniTask.Delay(1000);
        DialogueManager.Instance.ShowDialogue(true);
        await UniTask.Delay(500);
        DialogueManager.Instance.ShowNextSentence(text);
        // GetComponent<OutsideTeahouseDialogue>().ClickSentence(true);
        await UniTask.Delay(100);
        DialogueManager.Instance.isDialogueEnable = true;
    }

    public async void SwitchScene()
    {
        DialogueManager.Instance.ClearText();
        DialogueManager.Instance.isDialogueEnable = false;
		GameManager.Instance.FadeOutAudioMixer(2f);
		DialogueManager.Instance.ShowDialogue(false,2f);
        GameManager.Instance.PauseGame();
		await GeneralUIManager.Instance.FadeInBlack(2f);
        SceneManager.LoadScene("Teahouse_Staffroom");
    }
}
