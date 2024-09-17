using System.Collections.Generic;
using UnityEngine.InputSystem;

public class PathToTeahouseDialogue : AbstractInputActionsController
{
    private List<string> dialogueList = new List<string>(){
        "我有時會在假日出去嘗試一些新餐廳（主要是找新甜品吃）",
        "最近在網上看到有很多人推薦一間叫「幻花茶屋」的餐廳，今天便決定試試",
        "但是，跟着網上寫的地址及地圖走，郤來到了偏僻的郊外",
        "沿着山路走，不知走了多久，終於看到標示着幻花茶屋的路牌",
        "轉左走就是幻花茶屋……還有一間醫院叫「米亞私立醫院」？",
        "為甚麼在這麼偏僻的地方會有醫院？",
        "路牌上好像還寫着轉右有甚麼東西，不過被啡色的污漬弄污了",
    };

    private int currentSentence = 0;

    void Awake()
    {
        InitiateInputActions();
        playerInput.actions["Click"].performed += LeftClick;
    }

    public void LeftClick(InputAction.CallbackContext context){
        ClickSentence();
    }

    public void ClickSentence(){
        GLogger.Log(DialogueManager.Instance.isDialogueEnable);
        if (DialogueManager.Instance.isDialogueEnable && currentSentence <= dialogueList.Count){
            if (DialogueManager.Instance.isSentencePlaying){
                DialogueManager.Instance.JumpSentence(dialogueList[currentSentence-1]);
            }
            else{
                switch (currentSentence){
                    case 2:
                        SceneManager_Path_To_Teahouse.Instance.ShowPathVisual();
                        break;
                    case 4:
                        SceneManager_Path_To_Teahouse.Instance.ShowRoadSign();
                        break;
                    case 7:
                        SceneManager_Path_To_Teahouse.Instance.SwitchScene();
                        break;
                    default:
                        DialogueManager.Instance.ShowNextSentence(dialogueList[currentSentence]);
                        break;
                } 
                currentSentence++;
            }
        }
    }
}
