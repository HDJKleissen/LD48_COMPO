using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPowerSounds
{
    public static void PlayCameraPowerOn(GameObject gameObject)
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/CameraPowerUp", gameObject);
    }

    public static void PlayCameraPowerOff(GameObject gameObject)
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/CameraPowerDown", gameObject);
    }
}
