using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

	public Image bar;
	public Text percent;

	public Text timer;

	public Sprite empty, dash, burn, climb, doubleJump;
	public Image capacity1, capacity2;

	// Use this for initialization
	void Start () {
		updateEnergyInfo();
		updateTimer();
		updateAbilities();
	}
	
	// Update is called once per frame
	void Update () {
		updateEnergyInfo();
		updateTimer();
		updateAbilities();
	}

	public void updateEnergyInfo()
	{
		bar.fillAmount = (float)GameManager.instance.playerEnergy / 100;
		percent.text = GameManager.instance.playerEnergy.ToString() + " %";
	}

	public void updateTimer()
	{
		timer.text = ((int)(GameManager.instance.LevelTimer)).ToString();
	}

	public void updateAbilities()
	{
		switch (GameManager.instance.playerAbility1)
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
		switch (GameManager.instance.playerAbility2)
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
	}
}
