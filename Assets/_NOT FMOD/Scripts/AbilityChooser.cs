using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityChooser : MonoBehaviour
{
	public Sprite empty, dash, burn, climb, doubleJump;

	public Canvas canvas;
	public Image capacity1, capacity2;

	[HideInInspector]
	public Player player;

	public bool waitingForInput = false;

	public int abilityId;

	private void Update()
	{
		if (waitingForInput)
		{
			if (Input.GetButtonDown("Ability1"))
			{
				player.ability1 = abilityId;
				GameManager.instance.playerAbility1 = abilityId;
				Time.timeScale = 1;
				GameManager.instance.gamePaused = false;
				player.velocity = Vector3.zero;
				Destroy(gameObject);
			}
			if (Input.GetButtonDown("Ability2"))
			{
				player.ability2 = abilityId;
				GameManager.instance.playerAbility2 = abilityId;
				Time.timeScale = 1;
				GameManager.instance.gamePaused = false;
				Destroy(gameObject);
			}
		}
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.CompareTag("Player"))
		{
			player = collision.gameObject.GetComponent<Player>();

			switch (player.ability1)
			{
				case -1:
					capacity1.sprite = empty;
					break;
				case 0:
					capacity1.sprite = dash;
					break;
				case 1:
					capacity1.sprite = burn;
					break;
				case 2:
					capacity1.sprite = climb;
					break;
				case 3:
					capacity1.sprite = doubleJump;
					break;
			}
			switch (player.ability2)
			{
				case -1:
					capacity2.sprite = empty;
					break;
				case 0:
					capacity2.sprite = dash;
					break;
				case 1:
					capacity2.sprite = burn;
					break;
				case 2:
					capacity2.sprite = climb;
					break;
				case 3:
					capacity2.sprite = doubleJump;
					break;
			}

			canvas.gameObject.SetActive(true);

			Time.timeScale = 0;
			GameManager.instance.gamePaused = true;
			waitingForInput = true;
		}
	}
}
