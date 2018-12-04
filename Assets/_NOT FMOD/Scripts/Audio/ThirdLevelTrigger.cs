using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdLevelTrigger : MonoBehaviour {

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            AudioManager.instance.SetAmbienceParameter(AudioManager.instance.ambienceInstance, FMODPaths.LEVEL_NUMBER, 3);
            AudioManager.instance.SetMusicParameter(AudioManager.instance.musicInstance, FMODPaths.PIANO_CHORDS, 0);
            AudioManager.instance.SetMusicParameter(AudioManager.instance.musicInstance, FMODPaths.VIOLIN, 0);
            AudioManager.instance.SetMusicParameter(AudioManager.instance.musicInstance, FMODPaths.SYNTH, 1);
            AudioManager.instance.SetMusicParameter(AudioManager.instance.musicInstance, FMODPaths.BEAT, 1);
            AudioManager.instance.SetMusicParameter(AudioManager.instance.musicInstance, FMODPaths.BASS, 1);

        }
    }
}
