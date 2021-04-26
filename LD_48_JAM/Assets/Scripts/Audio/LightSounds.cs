using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSounds : MonoBehaviour
{
    GameManager gameManager;
    bool lightsOn;

    private void Start()
    {

    }

    public void PlayLightSwitchSound()
    {
        gameManager = FindObjectOfType<GameManager>();
        lightsOn = gameManager.LightsOn;

        if (lightsOn)
        {
            FMODUnity.RuntimeManager.PlayOneShotAttached("event:/LightsOn", gameObject);
        }
        else
        {
            FMODUnity.RuntimeManager.PlayOneShotAttached("event:/LightsOff", gameObject);
        }
    }
}
