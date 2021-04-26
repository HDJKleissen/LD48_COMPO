using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraButtonSounds : MonoBehaviour
{
    public void PlayCameraButtonOn()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/CameraPowerUpButton", gameObject);
    }

    public void PlayCameraButtonOff()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/CameraPowerDownButton", gameObject);
    }
}
