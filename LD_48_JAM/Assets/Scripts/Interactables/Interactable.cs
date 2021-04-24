using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public UnityEvent OnInteract;

    void Awake()
    {
        if (OnInteract == null)
            OnInteract = new UnityEvent();
    }


    public void Interact()
    {
        OnInteract.Invoke();
    }
}