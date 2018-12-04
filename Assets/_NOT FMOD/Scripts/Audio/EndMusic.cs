using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndMusic : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.gameObject.tag == "Player")
        {
            AudioManager.instance.SetMusicParameter(AudioManager.instance.musicInstance, FMODPaths.LEVEL_NUMBER, 1);
            AudioManager.instance.SetMusicParameter(AudioManager.instance.musicInstance, FMODPaths.PIANO_CHORDS, 1);
            AudioManager.instance.SetMusicParameter(AudioManager.instance.musicInstance, FMODPaths.VIOLIN, 1);
            AudioManager.instance.SetMusicParameter(AudioManager.instance.musicInstance, FMODPaths.SYNTH, 0);
            AudioManager.instance.SetMusicParameter(AudioManager.instance.musicInstance, FMODPaths.BEAT, 0);
            AudioManager.instance.SetMusicParameter(AudioManager.instance.musicInstance, FMODPaths.BASS, 0);

        }
    }
}
