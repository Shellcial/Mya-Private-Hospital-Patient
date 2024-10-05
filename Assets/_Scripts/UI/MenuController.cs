using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public static MenuController Instance;
    private MainMenu_InputActions menuInputActions;
    [SerializeField]
    private CanvasGroup _wholeESCMenu; 
    [SerializeField]
    private ESCMenu _escMainMenu;
    [SerializeField]
    private TutorialMenu _tutorialMenu;
    [SerializeField]
    private LoadPageManager _loadMenu;
    public bool isAllowOpen = false;
    // use isFading to avoid instant trigger repetatedly
    public bool isFading = false;
    private bool _isMenuOpen = false;
    [SerializeField]
    private RawImage overlay;
    [SerializeField]
    private CanvasGroup leaveOverlay;
    void Awake(){
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }

        isFading = false;
        leaveOverlay.blocksRaycasts = false;
    }
    
    void Start()
    {   
        menuInputActions = new MainMenu_InputActions();
        menuInputActions.Menu.Enable();
        menuInputActions.Menu.ESC.performed += TriggerMenu;
    }

    void TriggerMenu(InputAction.CallbackContext context){
        GLogger.Log(GameManager.Instance.GetGameStatus());
        GLogger.Log(context.performed);
        if (isAllowOpen && !isFading){
            if (context.performed){
                if (GameManager.Instance.GetGameStatus()){
                    if (_isMenuOpen){
                        CloseMenu();
                    }
                    else {
                        OpenMenu();
                    }
                }   
            }        
        }
    }

    void OpenMenu(){
        leaveOverlay.blocksRaycasts = false;
        GameManager.Instance.LockCursor(false);
        isFading = true;
        _isMenuOpen = true;

        _wholeESCMenu.DOFade(1, 0.2f);
        _wholeESCMenu.blocksRaycasts = true;
        _wholeESCMenu.interactable = true;
        
        GameManager.Instance.FreezePlayerMove();
        _escMainMenu.EnterPage();
        StartCoroutine(ReactivateTrigger());
    }

    public void CloseMenu(){
        GameManager.Instance.LockCursor(true);
        isFading = true;
        _isMenuOpen = false;
        _wholeESCMenu.DOFade(0, 0.2f);
        _wholeESCMenu.blocksRaycasts = false;
        _wholeESCMenu.interactable = false;
        HideOverlay();
        _escMainMenu.LeavePage();
        _tutorialMenu.LeavePage();
        _loadMenu.SpecialLeavePage();
        leaveOverlay.blocksRaycasts = true;

        GameManager.Instance.ResumePlayer();
        PlayerController.Instance.ShowCursor();
        StartCoroutine(ReactivateTrigger());
    }

    IEnumerator ReactivateTrigger(){
        yield return new WaitForSeconds(0.2f);
        isFading = false;
    }

    public void CheckMenu(){
        if (_isMenuOpen){
            CloseMenu();
        }
    }

    public void ShowOverlay(){
        overlay.DOFade(0.5f, 0.2f);
    }

    public void HideOverlay(){
        overlay.DOFade(0f, 0.2f);
    }

    public async void GoBackToTitle(){
        leaveOverlay.blocksRaycasts = true;
        isFading = true;
        GameManager.Instance.FadeOutAudioMixer(2f);
        await GeneralUIManager.Instance.FadeInBlack(2f);
        SceneManager.LoadScene("Title_Scene");
    }

    public void GoToSpecialChapter(){
        leaveOverlay.blocksRaycasts = true;
        isFading = true;
    }

    public void SpecialOpenTutorial(){
        leaveOverlay.blocksRaycasts = false;
        GameManager.Instance.LockCursor(false);
        isFading = true;
        _isMenuOpen = true;

        _wholeESCMenu.DOFade(1, 0.2f);
        _wholeESCMenu.blocksRaycasts = true;
        _wholeESCMenu.interactable = true;
        
        
        GameManager.Instance.ResumeGame();
        GameManager.Instance.FreezePlayerMove();

        _tutorialMenu.EnterPage();
        ShowOverlay();
        StartCoroutine(ReactivateTrigger());
    }
}
