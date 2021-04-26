using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficeCamera : Toggleable
{
    public GameObject CameraCone;
    public SpriteRenderer spriteRenderer;
    public GameObject AlertPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(CameraCone.activeInHierarchy != Open)
        {
            if (Open)
            {
                CameraPowerSounds.PlayCameraPowerOn(gameObject);
            }
            else
            {
                CameraPowerSounds.PlayCameraPowerOff(gameObject);
            }
            CameraCone.SetActive(Open);
        }
    }

    internal void SetPlayerSeen(bool playerSeen)
    {
        spriteRenderer.color = playerSeen ? Color.red : Color.white;
    }

    public void AlertCamera()
    {
        Instantiate(AlertPrefab, transform);
    }
}
