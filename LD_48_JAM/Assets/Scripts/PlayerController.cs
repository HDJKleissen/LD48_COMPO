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
    CircleCollider2D feetCollider;

    PlayerDirection movingDirection = PlayerDirection.None;

    public List<Interactable> interactablesInRange = new List<Interactable>();

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        feetCollider = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            if (interactablesInRange.Count > 0)
            {
                Interactable interactable = interactablesInRange[0];

                interactable.Interact();

                // Can do dialogue or something like that ("Hmm... This door is locked.")
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0).normalized * MoveSpeed * Time.deltaTime;

        UpdatePlayerDirection(movement);

        transform.position += movement;

        UpdateSprite();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Interactable")
        {
            Interactable other = collision.GetComponent<Interactable>();
            if(other != null)
            {
                interactablesInRange.Insert(0, collision.GetComponent<Interactable>());
            }
            else
            {
                Debug.LogWarning("Object " + collision.name + " has the interactable tag but no Interactable component! Please fix!");
            }
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Interactable")
        {
            Interactable other = collision.GetComponent<Interactable>();
            if (other != null)
            {
                if (interactablesInRange.Contains(other))
                {
                    interactablesInRange.Remove(collision.GetComponent<Interactable>());
                }
            }
            else
            {
                Debug.LogWarning("Object " + collision.name + " has the interactable tag but no Interactable component! Please fix!");
            }
        }
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
