using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CutOutScene : MonoBehaviour
{
    private GameObject cutOutParent;
    private Image sakuraMask;
    private float scaleDown = 0f;
    private float scaleUp = 13000f;
    private float scaleDuration = 0.6f;

    public static CutOutScene instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }

        cutOutParent = GameObject.Find("CutOut_Mask");
        cutOutParent.SetActive(false);
        cutOutParent.GetComponent<CanvasGroup>().alpha = 1;

        sakuraMask = cutOutParent.transform.Find("sakura_mask").GetComponent<Image>();
    }

    public void CutOutAnimation()
    {
        LeanTween.value(scaleUp, scaleDown, scaleDuration).setOnUpdate((float val) =>
        {
            sakuraMask.rectTransform.sizeDelta = new Vector2(val, val);
        }).setEase(LeanTweenType.easeOutCubic);
    }

    public IEnumerator StartGameCutIn()
    {
        cutOutParent.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        CutInAnimation();
        GameManager.Instance.ResumeGame();
    }

    public void CutInAnimation()
    {
        cutOutParent.SetActive(true);
        LeanTween.value(scaleDown, scaleUp, scaleDuration).setOnUpdate((float val) => {
            sakuraMask.rectTransform.sizeDelta = new Vector2(val, val);
        }).setEase(LeanTweenType.easeInCubic);
    }
}
