using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPowerSounds : MonoBehaviour
{
    public void PlayCameraPowerOn()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/CameraPowerUp", gameObject);
    }

    public void PlayCameraPowerOff()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/CameraPowerDown", gameObject);
    }
}
