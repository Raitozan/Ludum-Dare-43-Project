using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {


    public static AudioManager instance = null;

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
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
