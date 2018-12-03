using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

	public Image bar;
	public Text percent;

	public Text timer;

	// Use this for initialization
	void Start () {
		updateEnergyInfo();
		updateTimer();
	}
	
	// Update is called once per frame
	void Update () {
		updateEnergyInfo();
		updateTimer();
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
}
