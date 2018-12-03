using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstLevelTrigger : MonoBehaviour {




    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {

            AudioManager.instance.SetMusicParameter(AudioManager.instance.musicInstance, FMODPaths.PIANO_CHORDS, 0);
            AudioManager.instance.SetMusicParameter(AudioManager.instance.musicInstance, FMODPaths.VIOLIN, 0);
        }
    }


}
