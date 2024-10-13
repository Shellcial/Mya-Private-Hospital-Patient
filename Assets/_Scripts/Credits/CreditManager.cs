using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreditManager : MonoBehaviour
{
    [SerializeField]
    private Transform creditTexts;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private List<CreditText> creditTextsList = new List<CreditText>();
    private bool _isFirstClicked = false;
    [SerializeField]
    private TextMeshProUGUI _jumpText;
    [SerializeField]
    private RawImage overlay;
    private bool isFading = false;
    private float maximumY = 7130;
    private float minY = -3300;
    private bool isFadingNotice = false;
    private int totalScore = 0;
    [SerializeField]
    private TextMeshProUGUI totalScoreText;
    
    [SerializeField]
    private TextMeshProUGUI extra0;
    // Start is called before the first frame update
    void Awake()
    {
       GameManager.Instance.FadeInAudioMixer(2f);
       creditTexts.localPosition = new Vector2(creditTexts.localPosition.x, minY); 
       _jumpText.DOFade(0, 0f);
    }

    void Start(){
        UpdateAllCredit();
        extra0.SetText("");
        GameManager.Instance.LockCursor(false);
    }

    // Update is called once per frame
    void Update()
    {
        float rectY = creditTexts.localPosition.y + moveSpeed * Time.deltaTime;
        rectY = Mathf.Clamp(rectY, minY, maximumY);

        if (rectY >= maximumY && !isFading){
            isFading = true;
            StartCoroutine(ShowZero());
        }
        else if (rectY <= maximumY){
            creditTexts.localPosition = new Vector2(creditTexts.localPosition.x, rectY);
        }
    }

    public void UpdateAllCredit(){
        UpdateEachCredit(GameManager.Instance.gameDataManager.gameData.IllustrationStats["food"], 0, "food");
        UpdateEachCredit(GameManager.Instance.gameDataManager.gameData.otherStats["teahouse_password"], 1, "teahouse_password");
        UpdateEachCredit(GameManager.Instance.gameDataManager.gameData.IllustrationStats["portrait_mya"], 2, "portrait_mya");
        UpdateEachCredit(GameManager.Instance.gameDataManager.gameData.IllustrationStats["portrait_rumii"], 3, "portrait_rumii");
        UpdateEachCredit(GameManager.Instance.gameDataManager.gameData.IllustrationStats["portrait_rabbi"], 4, "portrait_rabbi");
        UpdateEachCredit(GameManager.Instance.gameDataManager.gameData.IllustrationStats["portrait_luna"], 5, "portrait_luna");
        UpdateEachCredit(GameManager.Instance.gameDataManager.gameData.cardStats["teahouse_staffroom_card"], 6, "teahouse_staffroom_card");

        UpdateEachCredit(GameManager.Instance.gameDataManager.gameData.IllustrationStats["hospital_entrance_mya"], 7, "hospital_entrance_mya");
        UpdateEachCredit(GameManager.Instance.gameDataManager.gameData.cardStats["hospital_entrance_card"], 8, "hospital_entrance_card");
        
        UpdateEachCredit(GameManager.Instance.gameDataManager.gameData.IllustrationStats["video_room_mya"], 9, "video_room_mya");
        UpdateEachCredit(GameManager.Instance.gameDataManager.gameData.otherStats["watch_whole_poo_room_video"], 10, "watch_whole_poo_room_video");
        UpdateEachCredit(GameManager.Instance.gameDataManager.gameData.otherStats["skip_poo_room_video"], 11, "skip_poo_room_video");
        
        UpdateEachCredit(GameManager.Instance.gameDataManager.gameData.IllustrationStats["cleaner_room_mya_poster"], 12, "cleaner_room_mya_poster");
        UpdateEachCredit(GameManager.Instance.gameDataManager.gameData.IllustrationStats["cleaner_room_gummy_poster"], 13, "cleaner_room_gummy_poster");
        UpdateEachCredit(GameManager.Instance.gameDataManager.gameData.cardStats["cleaner_room_card"], 14, "cleaner_room_card");

        UpdateEachCredit(GameManager.Instance.gameDataManager.gameData.IllustrationStats["corridor_posters"], 15, "corridor_posters");

        UpdateEachCredit(GameManager.Instance.gameDataManager.gameData.IllustrationStats["general_ward_mya_poster"], 16, "general_ward_mya_poster");
        UpdateEachCredit(GameManager.Instance.gameDataManager.gameData.IllustrationStats["general_ward_little_cat_poster"], 17, "general_ward_little_cat_poster");
        
        UpdateEachCredit(GameManager.Instance.gameDataManager.gameData.cardStats["general_ward_card"], 18, "general_ward_card");
        UpdateEachCredit(GameManager.Instance.gameDataManager.gameData.otherStats["mya_ending"], 19, "mya_ending");
        
        UpdateEachCredit(GameManager.Instance.gameDataManager.gameData.otherStats["gummy_ending"], 20, "gummy_ending");
        UpdateEachCredit(GameManager.Instance.gameDataManager.gameData.IllustrationStats["gummy_tachie"], 21, "gummy_tachie");

        totalScoreText.SetText(totalScore.ToString());
    }

    public void UpdateEachCredit(bool isActivated, int creditIndex, string text){
        totalScore += creditTextsList[creditIndex].UpdateText(isActivated, text);
    }

    public void ClickPage(){
        if (!_isFirstClicked || !isFadingNotice){
            isFadingNotice = true;
            _jumpText.DOFade(1, 0.2f).OnComplete(()=>{
                _isFirstClicked = true;
            });
        }
        else if (!isFading){
            isFading = true;
            DirectSwitchScene();
        }
    }

    public IEnumerator ShowZero(){
        isFading = true;
        yield return new WaitForSeconds(2f);
        extra0.SetText("0");
        FlatAudioManager.Instance.Play("typing", false);
        yield return new WaitForSeconds(1f);
        extra0.SetText("00");
        FlatAudioManager.Instance.Play("typing", false);
        yield return new WaitForSeconds(2f);
        SwtichScene();
    }

    public void SwtichScene(){
        overlay.DOFade(1, 1f).SetDelay(3f).OnComplete(()=>{
            SceneManager.LoadScene("Title_Scene");
        });
    }

    public void DirectSwitchScene(){
        overlay.DOFade(1, 1f).OnComplete(()=>{
            SceneManager.LoadScene("Title_Scene");
        });
    }

}
