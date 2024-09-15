using System.Collections.Generic;
using UnityEngine.InputSystem;

public class PooRoomDialogue : AbstractInputActionsController
{
    private List<string> dialogueList = new List<string>(){
        "終於到了，明明是在深山中一樣的地方，郤有超多人光顧呢",
        "一……二……三……四……",
        "驟眼看整間店有4位店員，每一位也是超級可愛的女孩子，不怪得會有這麼多人光顧",
        "入座後，替我下單的是一位粉紅色頭髮，戴着熊貓耳朵頭飾的女孩子",
        "她推薦我一定要嘗試她們店親自調製的屎水",
        "看了看餐牌的圖片，估計是朱古力及咖啡類型的飲品吧",
        "雖然名稱充滿着惡趣味，不過就試一下吧。",
        "喝了一口，和想像中的味道完全不同",
        "原本以為是朱古力或者咖啡味，但這味道郤是從未品嘗過的",
        "說不上難吃，難以形容的味道。",
        "……總感覺，肚子好像越來越痛了……",
        "在洗手間待了一會，但肚子依然很痛……",
        "對了，剛剛路牌寫着旁邊有間醫院，試試去那兒看醫生吧。"
    };

    private List<int> pauseNumbers = new List<int>(){
        1, 3, 5
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
        if (DialogueManager.Instance.isDialogueEnable){
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
                    case 6:
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
