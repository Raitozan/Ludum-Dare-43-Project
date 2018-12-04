using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicChange : MonoBehaviour {

   
    void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.gameObject.tag == "Player")
        {
            AudioManager.instance.SetMusicParameter(AudioManager.instance.musicInstance, FMODPaths.LEVEL_NUMBER, 2);
        }
    }
}
