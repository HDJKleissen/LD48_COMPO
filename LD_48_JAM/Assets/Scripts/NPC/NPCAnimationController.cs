using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnimationController : MonoBehaviour
{
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    public void UpdateAnimator(Vector3 movement)
    {
        int horizontalMovement, verticalMovement;
        if(movement.x != 0)
        {
            horizontalMovement = Math.Sign(movement.x);
        }
        else
        {
            horizontalMovement = 0;
        }
        if (movement.y != 0)
        {
            verticalMovement = Math.Sign(movement.y);
        }
        else
        {
            verticalMovement = 0;
        }
        animator.SetInteger("HorizontalMovement", horizontalMovement);
        animator.SetInteger("VerticalMovement", verticalMovement);

        if (horizontalMovement > 0)
        {
            transform.parent.localScale = new Vector3(-1, 1, 1);
        }
        else if (horizontalMovement < 0)
        {
            transform.parent.localScale = new Vector3(1, 1, 1);
        }
    }

    internal void ColorSprite(Color color)
    {
        spriteRenderer.color = color;
    }
}
