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
        int horizontalMovement = Math.Sign(movement.x);
        int verticalMovement = Math.Sign(movement.y);

        animator.SetInteger("HorizontalMovement", horizontalMovement);
        animator.SetInteger("VerticalMovement", verticalMovement);

        if(horizontalMovement > 0)
        {
            spriteRenderer.flipX = true;
        }
        else if(horizontalMovement < 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    internal void ColorSprite(Color color)
    {
        spriteRenderer.color = color;
    }
}
