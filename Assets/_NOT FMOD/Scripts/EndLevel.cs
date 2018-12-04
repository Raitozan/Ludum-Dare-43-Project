using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour {
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			if(!(GameManager.instance.actualLvl == 3))
			{
				GameManager.instance.actualLvl++;
				GameManager.instance.startAbility1 = GameManager.instance.playerAbility1;
				GameManager.instance.startAbility2 = GameManager.instance.playerAbility2;
				GameManager.instance.ResetLvl();
			}
			else
			{
				SceneManager.LoadScene(4);
			}
        }
	}
}
