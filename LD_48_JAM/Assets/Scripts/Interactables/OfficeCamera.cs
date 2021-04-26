using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficeCamera : Toggleable
{
    public GameObject CameraCone;
    public SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(CameraCone.activeInHierarchy != Open)
        {
            CameraCone.SetActive(Open);
        }
    }

    internal void SetPlayerSeen(bool playerSeen)
    {
        spriteRenderer.color = playerSeen ? Color.red : Color.white;
    }
}
