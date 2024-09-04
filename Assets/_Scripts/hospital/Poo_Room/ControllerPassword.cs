using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerPassword : MonoBehaviour
{
    private List<float> _password = new List<float>(){1,6,4,7};
    
    private List<float> _receivedPassword = new List<float>(){};
    
    [SerializeField]
    private List<GameObject> _allButtons = new List<GameObject>();
    private List<S_ControllerButton> _allButtonsController = new List<S_ControllerButton>();
    private int _startOrder = 2;

    [SerializeField]
    private List<GameObject> _screens;
    private List<Material> _screensMaterial = new List<Material>();

    [SerializeField]
    private List<Texture> _screensTexture = new List<Texture>();
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject _button in _allButtons){
            _allButtonsController.Add(_button.GetComponent<S_ControllerButton>());
        }

        foreach (GameObject _screen in _screens){
            _screensMaterial.Add(_screen.GetComponent<Renderer>().material);
        }

        _startOrder = Random.Range(1, 10);
        _receivedPassword.Clear();
        ResetButtonIndex();
    }

    public void ReceivePassword(int _index){
        _receivedPassword.Add(_index);
        _screensMaterial[_receivedPassword.Count-1].SetTexture("_MainTex", _screensTexture[_index]);
        _screensMaterial[_receivedPassword.Count-1].SetTexture("_EmissionMap", _screensTexture[_index]);

        if (_receivedPassword.Count == _password.Count){
            CheckPassword();
        }
        else{
            FlatAudioManager.instance.Play("press_password", false);
        }
    }

    void CheckPassword(){
        for (int i = 0; i < _password.Count; i++){
            if (_password[i] != _receivedPassword[i]){
                _receivedPassword.Clear();
                ResetButtonIndex();
                FlatAudioManager.instance.Play("wrong_password", false);
                return;
            }            
        }

        CorrectPassword();
    }

    void CorrectPassword(){
        _receivedPassword.Clear();
        StopPassword();
        SceneManager_PooRoom.Instance.ForceEndVideo();
    }

    public void EnablePassword(){
        _allButtonsController[0].EnableInteract();
    }

    // call from scene manager either video natural ended or forcely ended
    public void StopPassword(){
        _allButtonsController[0].DisableInteract();
    }

    void ResetButtonIndex(){
        // reset material
        foreach (Material _screenMaterial in _screensMaterial){
            _screenMaterial.SetTexture("_MainTex", _screensTexture[10]) ;
            _screenMaterial.SetTexture("_EmissionMap", _screensTexture[10]) ;
        }

        // each time password entered, shift the button index by 1
        for (int i = 0; i < _allButtons.Count; i++){
            int _assignIndex = i;
            int _assignOrder = _startOrder + i;
            if (_assignOrder >= _allButtons.Count){
                _assignOrder -= _allButtons.Count;
                _allButtonsController[_assignOrder].SetButtonIndex(_assignIndex);
            }
            else{
                _allButtonsController[_assignOrder].SetButtonIndex(_assignIndex);
            }
        }

        if (_startOrder == _allButtons.Count - 1){
            _startOrder = 0;
        }
        else {
            _startOrder++;
        }
    }
}
