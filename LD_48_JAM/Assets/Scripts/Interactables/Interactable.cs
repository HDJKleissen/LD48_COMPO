using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class Interactable : MonoBehaviour
{
    public UnityEvent OnInteract;

    void Awake()
    {
        if (OnInteract == null)
            OnInteract = new UnityEvent();
    }

    void OnValidate()
    {
        gameObject.tag = "Interactable";

        if(!GetComponent<Collider2D>().isTrigger)
        {
            Debug.LogError("Interactable object " + name + " does not have a trigger collider attached.");
        }
    }

    public void Interact()
    {
        OnInteract.Invoke();
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}