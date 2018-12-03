using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {


    public static AudioManager instance = null;

    public FMOD.Studio.EventInstance ambienceInstance;
    public FMOD.Studio.EventInstance musicInstance;

    private void Awake()

    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);


        DontDestroyOnLoad(this.gameObject);
    }

    // Use this for initialization
    void Start () {
        ambienceInstance = FMODUnity.RuntimeManager.CreateInstance(FMODPaths.AMBIENCE);
        musicInstance = FMODUnity.RuntimeManager.CreateInstance(FMODPaths.MUSIC);
     //   PlayAmbience();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayAmbience()
    {
        FMOD.Studio.PLAYBACK_STATE _playingAmbience;
        ambienceInstance.getPlaybackState(out _playingAmbience);
        if (_playingAmbience != FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            ambienceInstance.start();
        }

    }

    public void PlayMusic()
    {
        FMOD.Studio.PLAYBACK_STATE _playingMusic;
        musicInstance.getPlaybackState(out _playingMusic);
        if (_playingMusic != FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
          
            musicInstance.start();
        }

    }

    public void StopMusic()
    {
        musicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        // musicInstance.release();
    }


    public void StopAmbience()
    {
        ambienceInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        // musicInstance.release();
    }

    public void SetAmbienceParameter(FMOD.Studio.EventInstance ambienceInstance, string name, int value)
    {
        FMOD.Studio.ParameterInstance ambienceParameter;
        musicInstance.getParameter(name, out ambienceParameter);

        ambienceParameter.setValue(value);
    }


    public void SetMusicParameter(FMOD.Studio.EventInstance musicInstance, string name, int value)
    {
        FMOD.Studio.ParameterInstance musicParameter;
        musicInstance.getParameter(name, out musicParameter);

        musicParameter.setValue(value);
    }





}
