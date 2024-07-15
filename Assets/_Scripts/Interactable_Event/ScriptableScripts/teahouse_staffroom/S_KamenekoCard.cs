using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_KamenekoCard : InteractableObject
{
    private enum CardIndex{
        teahouse_staffroom_card,
        cleaner_room_card,
    };
    [SerializeField]
    private CardIndex _cardIndex;

    private void Start(){
        if (GameManager.Instance.gameDataManager.gameData.cardStats[_cardIndex.ToString()]){
            this.gameObject.SetActive(false);
        }
    }
    
    public override void Interact()
    {
        GameManager.Instance.gameDataManager.UnlockCard(_cardIndex.ToString());
        this.gameObject.SetActive(false);
    }
}
