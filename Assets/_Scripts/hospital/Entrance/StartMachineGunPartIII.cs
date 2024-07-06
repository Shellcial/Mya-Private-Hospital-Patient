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
            _newUpperObject.SetActive(true);
            GameObject _newLowerObject = Instantiate(_lowerInstance);
            _newLowerObject.SetActive(true);
            _upperList.Add(_newUpperObject);            
            _lowerList.Add(_newLowerObject);
            if (i == 0){
                _newUpperObject.transform.parent = _upperParent.transform;
                _newLowerObject.transform.parent = _lowerParent.transform;

            }
            else{
                _upperList[i].transform.parent = _upperList[i-1].transform;
                _lowerList[i].transform.parent = _lowerList[i-1].transform;
            }
        }
    }

    public IEnumerator StartAnimation(){
        float _startScaleTime = 0.5f;
        float rotateTime = 0.02f;

        _upperParent.SetActive(true);
        _upperParent.transform.DOScaleZ(0.05f, _startScaleTime).SetEase(Ease.OutSine).OnComplete(()=>{
            for (int i = 0; i < 36; i++){
                _upperList[i].transform.DOScaleZ(1f, 0f);
            }
        });


        yield return new WaitForSeconds(0.1f);

        _lowerParent.SetActive(true);
        _lowerParent.transform.DOScaleZ(0.05f, _startScaleTime).SetEase(Ease.OutSine).OnComplete(()=>{
            for (int i = 0; i < 36; i++){
                _lowerList[i].transform.DOScaleZ(1f, 0f);
            }
        });
        
        yield return new WaitForSeconds(_startScaleTime-0.1f);

        for (int i = 0; i < 36; i++){
            if (i==0){
                _upperList[i].transform.DOLocalRotate(new Vector3(-5, 0f, 0f), 0.1f);
                yield return new WaitForSeconds(0.05f);
                _lowerList[i].transform.DOLocalRotate(new Vector3(-5, 0f, 0f), 0.1f);
            }
            else{
                _upperList[i].transform.DOLocalRotate(new Vector3(-5, 0f, 0f), rotateTime);
                yield return new WaitForSeconds(rotateTime/2);
                _lowerList[i].transform.DOLocalRotate(new Vector3(-5, 0f, 0f), rotateTime);
                yield return new WaitForSeconds(rotateTime/2);
            }
        }
        
        yield return new WaitForSeconds(0.7f);

        _upperParent.SetActive(false);
        _lowerParent.SetActive(false);
        for (int i = 0; i < 36; i++){
            _upperList[i].SetActive(false);
            _lowerList[i].SetActive(false);
        }
    }
}
