using System;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Sprite ClosedSprite, OpenSprite;
    public PolygonCollider2D OpenCollider, ClosedCollider;

    public bool Open, Locked;

    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateSprite();
        UpdateCollider();
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
        UpdateSprite();
        UpdateCollider();
    }

    void UpdateSprite()
    {
        spriteRenderer.sprite = Open ? OpenSprite : ClosedSprite;
    }
    void UpdateCollider()
    {
        OpenCollider.enabled = Open;
        ClosedCollider.enabled = !Open;
    }
}

[Serializable]
public enum DoorInteraction
{
    ToggleOpen,
    ToggleLocked
}
