using System.Collections.Generic;
using UnityEngine.InputSystem;

public class PathToTeahouseDialogue : AbstractInputActionsController
{
    private List<string> dialogueList = new List<string>(){
        "最近在網上看到很多人推薦一間叫「幻花茶屋」的餐廳",
        "本來想試試這間新餐廳，因此跟着網上寫的地址走……",
        "走着走着，郤來到了偏僻的郊外",
        "……前面有個路牌",
        "轉左走就是幻花茶屋……還有一間醫院叫「米亞私立醫院」？",
        "為甚麼在這麼偏僻的地方會有醫院？",
        "路牌上好像還寫着右邊有甚麼東西，不過被啡色的污漬弄污了",
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

    public void ClickSentence(bool isByPass = false){
        
        if (!GameManager.Instance.GetPlayerStatus()){
            return;
        }

        if (!isByPass){
            if (!DialogueManager.Instance.isDialogueEnable){
                return;
            }
        }

        if (currentSentence <= dialogueList.Count){
            if (DialogueManager.Instance.isSentencePlaying){
                DialogueManager.Instance.JumpSentence(dialogueList[currentSentence-1]);
            }
            else{
                switch (currentSentence){
                    case 2:
                        SceneManager_Path_To_Teahouse.Instance.ShowPathVisual(dialogueList[currentSentence]);
                        break;
                    case 4:
                        SceneManager_Path_To_Teahouse.Instance.ShowRoadSign(dialogueList[currentSentence]);
                        break;
                    case 7:
                        SceneManager_Path_To_Teahouse.Instance.SwitchScene();
                        break;
                    default:
                        // GLogger.Log("Show dialogue: " + dialogueList[currentSentence]);
                        DialogueManager.Instance.ShowNextSentence(dialogueList[currentSentence]);
                        break;
                } 
                currentSentence++;
            }
        }
    }

    void OnDestroy(){
        playerInput.actions["Click"].performed -= LeftClick;
    }
}
