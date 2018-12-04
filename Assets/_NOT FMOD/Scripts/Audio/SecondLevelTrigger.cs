using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondLevelTrigger : MonoBehaviour {


    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {           
            AudioManager.instance.SetAmbienceParameter(AudioManager.instance.ambienceInstance, FMODPaths.LEVEL_NUMBER, 2);
            AudioManager.instance.SetMusicParameter(AudioManager.instance.musicInstance, FMODPaths.SYNTH, 0);
            AudioManager.instance.SetMusicParameter(AudioManager.instance.musicInstance, FMODPaths.VIOLIN, 1);
        }
    }

}
