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

    }
	
	// Update is called once per frame
	void Update () {


        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlayMusic();
            PlayAmbience();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            StopMusic();
            StopAmbience();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetMusicParameter(musicInstance, FMODPaths.PIANO_CHORDS, 1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SetMusicParameter(musicInstance, FMODPaths.VIOLIN, 1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SetMusicParameter(musicInstance, FMODPaths.LEVEL_NUMBER, 2);
            SetAmbienceParameter(ambienceInstance, FMODPaths.LEVEL_NUMBER, 2);
        }
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

    /*
    AudioManager.SetMusicParameter(audioManager.musicInstance, FMODPaths.LEVEL_NUMBER, 1);
       AudioManager.SetMusicParameter(audioManager.musicInstance, FMODPaths.LEVEL_NUMBER, 2);

      AudioManager.SetAmbienceParameter(audioManager.ambienceInstance, FMODPaths.LEVEL_NUMBER, 1);
        AudioManager.SetAmbienceParameter(audioManager.ambienceInstance, FMODPaths.LEVEL_NUMBER, 2);

           AudioManager.SetMusicParameter(audioManager.musicInstance, FMODPaths.VIOLIN, 1);
           AudioManager.SetMusicParameter(audioManager.musicInstance, FMODPaths.PIANO_CHORDS, 1);

     AudioManager.SetMusicParameter(audioManager.musicInstance, FMODPaths.BATTERY_LEVEL, VARIABLE);



       */




}
