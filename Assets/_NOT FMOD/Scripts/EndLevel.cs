using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel : MonoBehaviour {
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			Debug.Log("Hello");
			GameManager.instance.actualLvl++;
			GameManager.instance.ResetLvl();
		}
	}
}
