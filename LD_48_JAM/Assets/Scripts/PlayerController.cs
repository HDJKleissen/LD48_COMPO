using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float MoveSpeed;

    public Sprite[] sprites;

    SpriteRenderer spriteRenderer;

    PlayerDirection movingDirection = PlayerDirection.None;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0) * MoveSpeed * Time.deltaTime;
        transform.position += movement;
        if (Input.GetButtonDown("Vertical"))
        {
            if(movement.y < 0)
            {
                movingDirection = PlayerDirection.Down;
            }
            else
            {

                movingDirection = PlayerDirection.Up;
            }
        }
        else if (Input.GetButtonDown("Horizontal"))
        {
            if(movement.x < 0)
            {
                movingDirection = PlayerDirection.Left;
            }
            else
            {
                movingDirection = PlayerDirection.Right;
            }
        }

        UpdateSprite();
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
