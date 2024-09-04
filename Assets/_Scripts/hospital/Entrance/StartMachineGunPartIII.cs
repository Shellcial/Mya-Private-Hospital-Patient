using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
public class StartMachineGunPartIII : MonoBehaviour
{
    [SerializeField]
    private GameObject _upperInstance;
    [SerializeField]
    private GameObject _lowerInstance;
    [SerializeField]
    private GameObject _upperParent;
    [SerializeField]
    private GameObject _lowerParent;
    [SerializeField]
    private List<GameObject> _upperList = new List<GameObject>();
    [SerializeField]
    private List<GameObject> _lowerList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        _upperParent.SetActive(false);
        _lowerParent.SetActive(false);
        // instaniate 72 gameobject
        for (int i = 0; i < 36; i++){
            GameObject _newUpperObject = Instantiate(_upperInstance);
            _newUpperObject.SetActive(false);
            GameObject _newLowerObject = Instantiate(_lowerInstance);
            _newLowerObject.SetActive(false);
            _upperList.Add(_newUpperObject);            
            _lowerList.Add(_newLowerObject);
            if (i == 0){
                _newUpperObject.transform.parent = _upperParent.transform;
                _newUpperObject.transform.localPosition = new Vector3(0, 0, 0);
                _newLowerObject.transform.parent = _lowerParent.transform;
                _newLowerObject.transform.localPosition = new Vector3(0, 0, 0);
            }
            else{
                _upperList[i].transform.parent = _upperList[i-1].transform;
                _upperList[i].transform.localPosition = new Vector3(0, 0, 0);
                _lowerList[i].transform.parent = _lowerList[i-1].transform;
                _lowerList[i].transform.localPosition = new Vector3(0, 0, 0);
            }
        }
    }

    public IEnumerator StartAnimation(){
        FlatAudioManager.instance.Play("gear_move_high", false);
        float _startScaleTime = 0.5f;

        _upperParent.SetActive(true);
        _upperParent.transform.DOScaleZ(0.05f, _startScaleTime).SetEase(Ease.OutSine).OnComplete(()=>{
            StartCoroutine(StartUpperRotate());
        });

        yield return new WaitForSeconds(0.1f);

        _lowerParent.SetActive(true);
        _lowerParent.transform.DOScaleZ(0.05f, _startScaleTime).SetEase(Ease.OutSine).OnComplete(()=>{
            StartCoroutine(StartLowerRotate());
        });
        
        yield return new WaitForSeconds(2.3f);

        // for (int i = 0; i < 36; i++){
        //     if (i==0){
        //         _upperList[i].transform.localScale = new Vector3(1,1,1);
        //         _upperList[i].transform.localEulerAngles = new Vector3(0,0,0);
        //         _upperList[i].transform.DOLocalRotate(new Vector3(-5, 0f, 0f), 0.1f).SetRelative(true);
        //         yield return new WaitForSeconds(0.05f);
        //         _lowerList[i].transform.localScale = new Vector3(1,1,1);
        //         _lowerList[i].transform.localEulerAngles = new Vector3(0,0,0);
        //         _lowerList[i].transform.DOLocalRotate(new Vector3(-5, 0f, 0f), 0.1f).SetRelative(true);
        //     }
        //     else{
        //         _upperList[i].transform.localScale = new Vector3(1,1,1);
        //         _upperList[i].transform.localEulerAngles = new Vector3(0,0,0);
        //         _upperList[i].transform.DOLocalRotate(new Vector3(-5, 0f, 0f), rotateTime).SetRelative(true);
        //         yield return new WaitForSeconds(rotateTime/2);
        //         _lowerList[i].transform.localScale = new Vector3(1,1,1);
        //         _lowerList[i].transform.localEulerAngles = new Vector3(0,0,0);
        //         _lowerList[i].transform.DOLocalRotate(new Vector3(-5, 0f, 0f), rotateTime).SetRelative(true);
        //         yield return new WaitForSeconds(rotateTime/2);
        //     }
            
        //     _upperList[i].SetActive(true);
        //     _lowerList[i].SetActive(true);
        // }

        _upperParent.SetActive(false);
        _lowerParent.SetActive(false);
        for (int i = 0; i < 36; i++){
            _upperList[i].SetActive(false);
            _lowerList[i].SetActive(false);
        }
    }

    IEnumerator StartUpperRotate(){
        float rotateTime = 0.02f;

        for (int i = 0; i < 36; i++){
            // _upperList[i].transform.DOScaleZ(1f, 0f);
            if (i==0){
                _upperList[i].transform.localScale = new Vector3(1,1,1);
                _upperList[i].transform.localEulerAngles = new Vector3(0,0,0);
                _upperList[i].transform.DOLocalRotate(new Vector3(-5, 0f, 0f), 0.1f);
                yield return new WaitForSeconds(0.05f);
            }
            else{
                _upperList[i].transform.localScale = new Vector3(1,1,1);
                _upperList[i].transform.localEulerAngles = new Vector3(0,0,0);
                _upperList[i].transform.DOLocalRotate(new Vector3(-5, 0f, 0f), rotateTime);
                yield return new WaitForSeconds(rotateTime);
            }
            
            _upperList[i].SetActive(true);
        }
    }

    IEnumerator StartLowerRotate(){
        float rotateTime = 0.02f;

        for (int i = 0; i < 36; i++){
            // _upperList[i].transform.DOScaleZ(1f, 0f);
            if (i==0){
                _lowerList[i].transform.localScale = new Vector3(1,1,1);
                _lowerList[i].transform.localEulerAngles = new Vector3(0,0,0);
                _lowerList[i].transform.DOLocalRotate(new Vector3(-5, 0f, 0f), 0.1f);
                yield return new WaitForSeconds(0.05f);
            }
            else{
                _lowerList[i].transform.localScale = new Vector3(1,1,1);
                _lowerList[i].transform.localEulerAngles = new Vector3(0,0,0);
                _lowerList[i].transform.DOLocalRotate(new Vector3(-5, 0f, 0f), rotateTime);
                yield return new WaitForSeconds(rotateTime);
            }
            
            _upperList[i].SetActive(true);
            _lowerList[i].SetActive(true);
        }
    }
}
