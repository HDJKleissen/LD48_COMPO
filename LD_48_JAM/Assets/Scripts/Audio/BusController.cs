using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class BusController : MonoBehaviour
{
    Bus Bus;
    public string BusPath;

    // Start is called before the first frame update
    void Start()
    {
        Bus = RuntimeManager.GetBus("bus:/"+ BusPath);
    }

    public void VolumeLevel(float sliderValue)
    {
        Bus.setVolume(sliderValue);
    }
}
