using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable: MonoBehaviour
{
    public UnityEvent OnInteract;
    public bool CanInteract;

    void Awake()
    {
        if (OnInteract == null)
            OnInteract = new UnityEvent();
    }

    public void ToggleCanInteract()
    {
        CanInteract = !CanInteract;
    }

    public bool Interact()
    {
        if (CanInteract)
        {
            OnInteract.Invoke();
        }

        return CanInteract;
    }
}
