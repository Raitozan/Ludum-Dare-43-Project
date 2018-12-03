using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoChordsTrigger : MonoBehaviour {


    void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.gameObject.tag == "Player")
        {
            AudioManager.instance.SetMusicParameter(AudioManager.instance.musicInstance, FMODPaths.PIANO_CHORDS, 1);
        }
    }

}
