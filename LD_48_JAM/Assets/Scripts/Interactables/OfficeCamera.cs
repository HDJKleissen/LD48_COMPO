using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficeCamera : MonoBehaviour
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
        
    }

    public void ToggleCamera()
    {
        CameraCone.SetActive(!CameraCone.activeInHierarchy);
    }

    internal void SetPlayerSeen(bool playerSeen)
    {
        spriteRenderer.color = playerSeen ? Color.red : Color.white;
    }
}
