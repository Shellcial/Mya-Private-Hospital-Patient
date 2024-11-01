using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OutsideTeahouseDialogue : AbstractInputActionsController
{
    private List<string> dialogueList = new List<string>(){
        "終於到了，明明是深山一樣的地方，郤有超多人光顧",
        "……嗯，驟眼看店員都是超級可愛的女孩子，不怪得有這麼多人光顧",

        "入座後，替我下單的是一位粉紅色頭髮，戴着熊貓耳朵頭飾的女孩子",
        "她推薦一定要嘗試由她親自調製的屎水",
        "看了看餐牌的圖片，估計是朱古力或者咖啡類型的飲品吧",
        "雖然命名充滿着惡趣味，不過就試一下",

        "……",
        "喝了一口，味道完全不像朱古力或者咖啡",
        "說不上難喝，但就是一種難以形容的味道",
        
        "……",
        "……肚子突然有點痛……",

        "借用了一下餐廳的洗手間，但肚子依然很痛。",
        "對了，剛剛路牌寫着旁邊有間醫院，試試去那兒看醫生吧。"
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

    
    public void ClickSentence(bool isByPass=false){
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
                        SceneManager_OutsideTeahouse.Instance.GetInHouse(dialogueList[currentSentence]);
                        break;
                    case 6:
                        SceneManager_OutsideTeahouse.Instance.TakeCoffee(dialogueList[currentSentence]);
                        break;
                    case 9:
                        SceneManager_OutsideTeahouse.Instance.FeelDizzy(dialogueList[currentSentence]);
                        break;
                    case 11:
                        SceneManager_OutsideTeahouse.Instance.GoToilet(dialogueList[currentSentence]);
                        break;
                    case 13:
                        SceneManager_OutsideTeahouse.Instance.SwitchScene();
                        break;
                    default:
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
