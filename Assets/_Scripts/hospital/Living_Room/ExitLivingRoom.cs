using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLivingRoom : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        ExitScene();
    }
    
    public async void ExitScene(){
        await GeneralUIManager.Instance.FadeInBlack();
        SceneManager.LoadScene("Ending_Credits");
    }
}
