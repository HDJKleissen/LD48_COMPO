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

        Collider2D[] colliders = GetComponents<Collider2D>();

        foreach (Collider2D collider in colliders)
        {
            if (collider.isTrigger)
            {
                return;
            }
        }
        Debug.LogError("Interactable object " + name + " does not have a trigger collider attached.");
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