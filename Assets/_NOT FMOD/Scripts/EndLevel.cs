using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel : MonoBehaviour {
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			GameManager.instance.actualLvl++;
			GameManager.instance.startAbility1 = GameManager.instance.playerAbility1;
			GameManager.instance.startAbility2 = GameManager.instance.playerAbility2;
			GameManager.instance.ResetLvl();
        }
	}
}
