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


    public override void Toggle(bool ignoreLock)
    {
        base.Toggle(ignoreLock);
        if(Open)
        {
            CameraButtonSounds.PlayCameraButtonOn(gameObject);
            spriteRenderer.sprite = OnSprite;
        }
        else
        {
            CameraButtonSounds.PlayCameraButtonOff(gameObject);
            spriteRenderer.sprite = OffSprite;
        }
    }
}
