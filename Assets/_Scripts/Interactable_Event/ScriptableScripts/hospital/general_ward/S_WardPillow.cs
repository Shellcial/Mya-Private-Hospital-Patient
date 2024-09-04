using UnityEngine;
using DG.Tweening;
public class S_WardPillow : InteractableObject
{
    private bool isTrigger = false;
    private float _startX = -11.09f;
    private float _endX = -11.547f;
    private float _animationTime = 1f;

    void Start(){
        EnableInteract();
    }

    public override void Interact()
    {
        if (!isTrigger){
            isTrigger = true;
            GameManager.Instance.gameDataManager.UnlockIllustration("general_ward_little_cat_poster");
            this.transform.DOLocalMoveX(_endX, _animationTime).SetEase(Ease.InOutSine);
            DisableInteract();
        }
    }


}
