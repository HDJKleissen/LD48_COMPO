using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public LayerMask WallsLayer;

    public float MoveSpeed;
    public float FeetRayLength;
    public Transform FeetPosition;

    public Sprite[] sprites;

    SpriteRenderer spriteRenderer;
    BoxCollider2D feetCollider;

    PlayerDirection movingDirection = PlayerDirection.None;


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        feetCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0) * MoveSpeed * Time.deltaTime;

        UpdatePlayerDirection(movement);

        movement = CheckForWalls(movement);

        transform.position += movement;

        UpdateSprite();
    }

    private void UpdatePlayerDirection(Vector3 movement)
    {
        if (Input.GetButton("Vertical"))
        {
            if (movement.y < 0)
            {
                movingDirection = PlayerDirection.Down;
            }
            else
            {

                movingDirection = PlayerDirection.Up;
            }
        }
        else if (Input.GetButton("Horizontal"))
        {
            if (movement.x < 0)
            {
                movingDirection = PlayerDirection.Left;
            }
            else
            {
                movingDirection = PlayerDirection.Right;
            }
        }
    }

    Vector3 CheckForWalls(Vector3 movement)
    {
        RaycastHit2D hitDown = Physics2D.Raycast(feetCollider.bounds.center + new Vector3(0, -feetCollider.bounds.extents.y, 0), new Vector2(0, -1), FeetRayLength, WallsLayer);
        RaycastHit2D hitUp = Physics2D.Raycast(feetCollider.bounds.center + new Vector3(0, feetCollider.bounds.extents.y, 0), new Vector2(0, 1), FeetRayLength, WallsLayer);
        RaycastHit2D hitLeft = Physics2D.Raycast(feetCollider.bounds.center + new Vector3(-feetCollider.bounds.extents.x, 0, 0), new Vector2(-1, 0), FeetRayLength, WallsLayer);
        RaycastHit2D hitRight = Physics2D.Raycast(feetCollider.bounds.center + new Vector3(feetCollider.bounds.extents.x, 0, 0), new Vector2(1, 0), FeetRayLength, WallsLayer);

        //Debug.DrawRay(feetCollider.bounds.center + new Vector3(0, -feetCollider.bounds.extents.y , 0), new Vector2(0, -FeetRayLength), Color.green);
        //Debug.DrawRay(feetCollider.bounds.center + new Vector3(0, feetCollider.bounds.extents.y, 0), new Vector2(0, FeetRayLength), Color.green);
        //Debug.DrawRay(feetCollider.bounds.center + new Vector3(-feetCollider.bounds.extents.x , 0, 0), new Vector2(-FeetRayLength, 0), Color.green);
        //Debug.DrawRay(feetCollider.bounds.center + new Vector3(feetCollider.bounds.extents.x , 0, 0), new Vector2(FeetRayLength, 0), Color.green);

        // If it hits something...
        if (hitUp.collider != null)
        {
            movement.y = Mathf.Clamp(movement.y, movement.y, 0);
        }
        if (hitDown.collider != null)
        {
            movement.y = Mathf.Clamp(movement.y, 0, movement.y);
        }
        if (hitLeft.collider != null)
        {
            movement.x = Mathf.Clamp(movement.x, 0, movement.x);
        }
        if (hitRight.collider != null)
        {
            movement.x = Mathf.Clamp(movement.x, movement.x, 0);
        }

        return movement;
    }

    void UpdateSprite()
    {
        if((int)movingDirection < sprites.Length)
        {
            spriteRenderer.sprite = sprites[(int)movingDirection];
        }
        else
        {
            spriteRenderer.sprite = sprites[1];
        }
        
    }

    enum PlayerDirection {
        Up,
        Down,
        Left,
        Right,
        None
    }
}
