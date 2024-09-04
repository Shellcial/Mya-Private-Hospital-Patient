using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_DoorKey : InteractableObject
{
    public override void Interact()
    {
        this.gameObject.SetActive(false);
        GameManager.Instance.gameDataManager.gameData.isGetReceptionKey = true;
    }
}
