using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapCreator : MonoBehaviour {

	public GameObject trapPrefab;

	public float instantiateTime;
	float timer;

	// Use this for initialization
	void Start () {
		timer = instantiateTime;
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if (timer <= 0)
		{
			Instantiate(trapPrefab, transform.position, Quaternion.identity, this.transform);
			timer = instantiateTime;
		}
	}
}
