using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burning : MonoBehaviour {

	public float lifetime;
	[HideInInspector]
	public Player player;

	private void Update()
	{
		lifetime -= Time.deltaTime;
		if (lifetime <= 0.0f)
		{
			player.isBurning = false;
			Destroy(gameObject);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.CompareTag("Flammable"))
		{
			Destroy(collision.gameObject);
			Destroy(GetComponent<CircleCollider2D>());
		}
	}
}
