using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMachineGunPartII : MonoBehaviour
{
    [SerializeField]
    private Animator lightAnimator;   
    [SerializeField]
    private Animator gunPartII_1;
    [SerializeField]
    private Animator gunPartII_2;    
    [SerializeField]
    private StartMachineGunPartIII _machineGunPartIII;
    void Start(){
        lightAnimator.speed = 0;
        gunPartII_1.speed = 0;
        gunPartII_2.speed = 0;
    }

    public void StartLightAnimation(){
        lightAnimator.speed = 1;
    }

    public void StartGunPartIIParts(){
        gunPartII_1.speed = 1;
        gunPartII_2.speed = 1;
    }

    public void StartGunPartIIIParts(){
        StartCoroutine(_machineGunPartIII.StartAnimation());
    }

    public void GoBlack(){
        GeneralUIManager.Instance.FadeInBlack(0).Forget();

        // play other audios before going to next scene
        StartCoroutine(GoToNextScene());
    }

    IEnumerator GoToNextScene(){
        yield return new WaitForSeconds(3f);
        Debug.Log("go to next scene");
        // SceneManager.LoadScene(4);
    }
}
