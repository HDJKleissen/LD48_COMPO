using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSystemPlayer : MonoBehaviour
{

    FMOD.Studio.EventInstance music;
    public int panicForMusic = 0;

    // Start is called before the first frame update
    void Start()
    {
        music = FMODUnity.RuntimeManager.CreateInstance("event:/Background_Music");
        music.start();
        music.release();
    }

    // Update is called once per frame
    void Update()
    {
        music.setParameterByName("Panic", panicForMusic, false);
    }

    private void OnDestroy()
    {
        music.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
