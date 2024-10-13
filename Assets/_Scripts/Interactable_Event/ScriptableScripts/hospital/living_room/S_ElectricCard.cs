using UnityEngine;

public class S_ElectricCard : InteractableObject
{
    public override void Interact()
    {
        this.gameObject.SetActive(false);
        FlatAudioManager.Instance.Play("get_card", false);
        SceneManager_LivingRoom.Instance.GetElectricKey();   
    }
}
