using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class CreditText : MonoBehaviour
{
    private TextMeshProUGUI _title;
    private TextMeshProUGUI _score;
    [SerializeField]
    private int score;
    public string _titleText;
    void Awake(){
        _title = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        _score = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }
    public int UpdateText(bool isActive, string text){
        _titleText = text;
        _score.enabled = isActive;
        if (isActive){
            _title.color = new Color(1,1,1,1);
            return score;
        }
        else{
            _title.color = new Color(0.5f,0.5f,0.5f,1f);
            return 0;
        }
    }
}