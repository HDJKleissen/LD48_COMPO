using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepsPlayer : MonoBehaviour
{
public void PlayFootstep()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Footsteps", gameObject);
    }
}
