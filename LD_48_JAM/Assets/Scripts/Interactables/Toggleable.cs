using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Toggleable : MonoBehaviour
{
    public bool Open, Locked;

    public virtual void Toggle()
    {
        if (!Locked)
        {
            Open = !Open;
        }
    }

    public virtual void ToggleLocked()
    {
        Locked = !Locked;
    }
}