using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : Toggleable
{
    public Sprite OffSprite, OnSprite;

    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSprite();
    }

    public override void Toggle()
    {
        base.Toggle();
        if(Open)
        {
            CameraButtonSounds.PlayCameraButtonOn(gameObject);
        }
        else
        {
            CameraButtonSounds.PlayCameraButtonOff(gameObject);
        }
    }

    void UpdateSprite()
    {
        spriteRenderer.sprite = Open ? OnSprite : OffSprite;
    }
}
