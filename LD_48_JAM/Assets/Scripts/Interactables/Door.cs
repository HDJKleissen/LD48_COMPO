using System;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public PolygonCollider2D ClosedCollider;
    public PolygonCollider2D[] OpenColliders = new PolygonCollider2D[2];

    public bool Open, Locked;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        UpdateCollider();
        UpdateAnimation();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleDoor(string interactionTypeString)
    {
        DoorInteraction interactionType = (DoorInteraction)Enum.Parse(typeof(DoorInteraction), interactionTypeString);
        switch (interactionType)
        {
            case DoorInteraction.ToggleOpen:
                if (Locked)
                {
                    // Some dialogue probably
                }
                else
                {
                    Open = !Open;
                }
                break;
            case DoorInteraction.ToggleLocked:
                Locked = !Locked;
                break;
        }
        UpdateCollider();
        UpdateAnimation();
    }

    void UpdateCollider()
    {
        ClosedCollider.enabled = !Open;
        OpenColliders[0].enabled = Open;
        OpenColliders[1].enabled = Open;
    }

    void UpdateAnimation()
    {
        animator.SetBool("DoorOpen", Open);
    }
}

[Serializable]
public enum DoorInteraction
{
    ToggleOpen,
    ToggleLocked
}
