using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepsPlayer : MonoBehaviour
{

    public void PlayFootstep()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Footsteps", gameObject);
    }

    public void PlayHighHeelFootstep()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/HighHeelFootsteps", gameObject);
    }

    public void PlayPizzFootstep()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/PizzFootsteps", gameObject);
    }
}
