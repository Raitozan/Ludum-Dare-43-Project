using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burning : MonoBehaviour {

	public float lifetime = 0.05f;

	private void Update()
	{
		lifetime -= Time.deltaTime;
		if (lifetime <= 0.0f)
			Destroy(gameObject);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.CompareTag("Flammable"))
		{
			Destroy(collision.gameObject);
		}
	}
}
