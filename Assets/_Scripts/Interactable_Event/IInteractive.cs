using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractive
{
    void Interact();
    void EnableInteract();
    void DisableInteract();
    void OnDestroy();
}
