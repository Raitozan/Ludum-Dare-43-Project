using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;

	public int actualLvl;

	public float LevelTimer;

	public float[] LevelTime = {60, 60, 60};

	public int startAbility1 = -1;
	public int startAbility2 = -1;
	public int playerAbility1 = -1;
	public int playerAbility2 = -1;
	public int playerEnergy;

	public bool gamePaused = false;

	void Awake()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);
		DontDestroyOnLoad(this);

		LevelTimer = LevelTime[actualLvl - 1];
		playerAbility1 = startAbility1;
		playerAbility2 = startAbility2;
	}

	private void Update()
	{
		LevelTimer -= Time.deltaTime;
		if (LevelTimer <= 0)
			ResetLvl();
    //    AudioManager.instance.SetMusicParameter(AudioManager.instance.musicInstance, FMODPaths.BATTERY_LEVEL, playerEnergy); // enable it later when AudioManager is in Main_Menu, otherwise errors because it can't find AudioManger
	}

	public void ResetLvl()
	{
		playerEnergy = 100;
		LevelTimer = LevelTime[actualLvl - 1];
		playerAbility1 = startAbility1;
		playerAbility2 = startAbility2;
		SceneManager.LoadScene(actualLvl);
	}
}
