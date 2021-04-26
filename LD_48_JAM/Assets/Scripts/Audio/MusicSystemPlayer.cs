using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSystemPlayer : MonoBehaviour
{

    //make this a singleton

    FMOD.Studio.EventInstance music;
    public int panicForMusic = 0;

    public static MusicSystemPlayer Instance;

    private void Awake()
    {
        int musicPlayerCount = FindObjectsOfType<MusicSystemPlayer>().Length;
        if (musicPlayerCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            MusicSystemPlayer.Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

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

    public void SetMenuMusic()
    {
        music.setParameterByName("Menu", 1, false);
    }

    public void StartLevelMusic()
    {
        music.setParameterByName("Menu", 0, false);
        music.setParameterByName("Panic", 0, false);
        music.setParameterByName("YouLose", 0, false);
    }

    public void StartYouLoseMusic()
    {
        music.setParameterByName("YouLose", 1, false);
        music.setParameterByName("Menu", 1, false);
    }

    private void OnDestroy()
    {
        music.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
