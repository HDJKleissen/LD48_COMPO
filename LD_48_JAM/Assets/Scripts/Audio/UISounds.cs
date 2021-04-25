using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISounds : MonoBehaviour
{

    public void PlayUIConfirm()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/UIConfirm");
    }

    public void PlayUISecondary()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/UISecondary");
    }

    public void PlayUIBack()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/UIBack");
    }
}
