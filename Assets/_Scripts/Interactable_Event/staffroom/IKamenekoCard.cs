using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKamenekoCard : MonoBehaviour, IInteractive
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

    public void DisableInteract()
    {

    }

    public void EnableInteract()
    {

    }

    public void Interact()
    {
        GameManager.Instance.gameDataManager.UnlockCard(_cardIndex.ToString());
        this.gameObject.SetActive(false);
    }

    public void OnDestroy()
    {

    }
}
