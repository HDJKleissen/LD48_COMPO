using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public Sprite OffSprite, OnSprite;

    public bool On;

    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ToggleSwitch()
    {
        On = !On;
        UpdateSprite();
    }

    void UpdateSprite()
    {
        spriteRenderer.sprite = On ? OnSprite : OffSprite;
    }
}
