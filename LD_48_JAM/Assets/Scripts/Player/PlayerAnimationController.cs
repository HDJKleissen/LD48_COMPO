using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public SpriteMask spriteMask;

    public RuntimeAnimatorController[] animatorControllers = new RuntimeAnimatorController[Enum.GetNames(typeof(PlayerDisguise)).Length];

    public void ChangeDisguise(PlayerDisguise disguise)
    {
        animator.runtimeAnimatorController = animatorControllers[(int)disguise];
    }

    void Update()
    {
        spriteMask.sprite = spriteRenderer.sprite;
        if (spriteRenderer.flipX)
        {
            spriteMask.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            spriteMask.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

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
}
