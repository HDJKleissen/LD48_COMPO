using System;
using System.Collections.Generic;
using UnityEngine;

public class Door : Toggleable
{
    public PolygonCollider2D ClosedCollider;
    public PolygonCollider2D[] OpenColliders = new PolygonCollider2D[2];

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

    public override void Toggle()
    {
        if (Locked)
        {
            // Dialogue: Hmm.. seems to be locked.
        }
        base.Toggle();
        UpdateCollider();
        UpdateAnimation();
    }

    public override void ToggleLocked()
    {
        base.ToggleLocked();
        if (!Locked)
        {
            // Dialogue: Got it! The door is now unlocked.
        }
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

