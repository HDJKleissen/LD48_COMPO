using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AnimatedSpriteMask : MonoBehaviour
{
    SpriteMask spriteMask;
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer= GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
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

    private void OnValidate()
    {
        spriteMask = GetComponentInChildren<SpriteMask>();
    }
}
