using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PandaAnimationManager : MonoBehaviour
{
    private float intervalTime = 0.2f;
    [SerializeField]
    private List<Animator> _pandaAnimator;
    void Start()
    {
        foreach (Animator animator in _pandaAnimator){
            animator.speed = 0f;
        }
        StartCoroutine(StartPandaAnimation());
    }

    IEnumerator StartPandaAnimation(){
        for (int i = 0; i<_pandaAnimator.Count ;i++){
            _pandaAnimator[i].speed = 1f;
            yield return new WaitForSeconds(intervalTime);
        }
        yield return null; 
    }
}
