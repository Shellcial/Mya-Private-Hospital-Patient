using System.Collections;
using DG.Tweening;
using UnityEngine;

public class S_RefuseDoor : InteractableObject
{
    private float _openValue = 90f;
    public bool isOpen = false;
    private float duration = 2f;
    [SerializeField]
    private bool isRight;
    [SerializeField]
    private S_RefuseDoor _linkedDoor;
    [SerializeField]
    private bool isFinalDoor = false;
    [SerializeField]
    private Rigidbody _pandaHeadRigidBody;
    private void Start(){
        EnableInteract();
    }

    public override void Interact()
    {
        if (!isOpen){
            //open door
            DisableInteract();

            isOpen = true;
            _linkedDoor.isOpen = true;

            if (isRight){
                _openValue = -_openValue;
            }
            
            FlatAudioManager.instance.Play("refuse_door_open_1", false);

            Vector3 targetValue = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y - _openValue, transform.eulerAngles.z);
            transform.DORotate(targetValue, duration, RotateMode.Fast).SetEase(Ease.InOutSine);
            
            _openValue = -_openValue;
            targetValue = new Vector3(_linkedDoor.transform.eulerAngles.x, _linkedDoor.transform.eulerAngles.y - _openValue, _linkedDoor.transform.eulerAngles.z);
            _linkedDoor.transform.DORotate(targetValue, duration, RotateMode.Fast).SetEase(Ease.InOutSine);

            if (isFinalDoor){
                FlatAudioManager.instance.Play("refuse_door_open_2", false);
                StartCoroutine(MovePandaHead());
            }
        }   
    }

    IEnumerator MovePandaHead(){
        yield return new WaitForSeconds(0.8f);
        _pandaHeadRigidBody.constraints = RigidbodyConstraints.None;
    }

}
