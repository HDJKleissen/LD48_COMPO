using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Sprite ClosedSprite, OpenSprite;
    public PolygonCollider2D OpenCollider, ClosedCollider;

    public bool Closed;

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

    public void ToggleDoor()
    {
        Closed = !Closed;
        UpdateSprite();
        UpdateCollider();
    }

    void UpdateSprite()
    {
        spriteRenderer.sprite = Closed ? ClosedSprite : OpenSprite;
    }
    void UpdateCollider()
    {
        OpenCollider.enabled = !Closed;
        ClosedCollider.enabled = Closed;
    }
}
