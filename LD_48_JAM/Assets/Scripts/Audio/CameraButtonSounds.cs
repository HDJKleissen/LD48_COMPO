using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraButtonSounds
{
    public static void PlayCameraButtonOn(GameObject gameObject)
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/CameraPowerUpButton", gameObject);
    }

    public static void PlayCameraButtonOff(GameObject gameObject)
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/CameraPowerDownButton", gameObject);
    }
}
