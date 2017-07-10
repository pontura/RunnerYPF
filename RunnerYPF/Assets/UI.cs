using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour {

	public Text scoreField;
	public Text timerField;

	public int score;
	private int totalSec = 59;
	public int sec;
	public int lives;
	public static UI Instance;
	bool timeRunning;

	public Image timerImage;

	public GameObject[] livesAssets;

	void Awake () {
		Instance = this;
		Events.OnGetEnergy += OnGetEnergy;
		Events.RestartAllOver += RestartAllOver;
		Events.StartGame += StartGame;
		Events.OnCharacterDie += OnCharacterDie;
	}
	void OnCharacterDie()
	{
		lives--;
		SetLives ();
	}
	void StartGame()
	{
		scoreField.text = "";
		timerField.text = "";

		if (!timeRunning) {
			TimerLoop ();
			timeRunning = true;
		}
		score = 0;
		lives = 3;
		sec = totalSec;

		SetLives ();
		SetScore ();
	}
	void RestartAllOver()
	{
		foreach (GameObject go in livesAssets)
			go.SetActive (true);
		
		StartGame ();
	}
	void TimerLoop()
	{
		Invoke ("TimerLoop", 1);
		if (Game.Instance.gameManager.state == GameManager.states.ENDING)
			return;
		if (lives == 0) {
			return;
		}
		
		string secText = "";
		if (sec < 10)
			secText = "0" + sec.ToString();
		else
			secText = sec.ToString();
		timerField.text = "00:" + secText;

		if (sec < 1)
			Events.OnTimeOver ();
		else if(Game.Instance.gameManager.state==GameManager.states.PLAYING)
			sec--;

		timerImage.fillAmount = 1-((float)(totalSec-sec)/(float)totalSec);

	}
	void OnGetEnergy () {
		score++;
		SetScore ();
	}

	void SetScore()
	{
		scoreField.color = new Color (1f, 0.45f, 0.19f);
		scoreField.text = score.ToString ();
		Invoke ("ResetScoreColor", 0.1f);
	}

	void ResetScoreColor(){
		scoreField.color = new Color (0.345f, 0.346f, 0.249f);
	}


	void SetLives()
	{
		foreach (GameObject go in livesAssets)
			go.SetActive (false);
		
		if(lives>0)
			livesAssets [0].SetActive (true);
		if(lives>1)
			livesAssets [1].SetActive (true);
		if(lives>2)
			livesAssets [2].SetActive (true);
	}
}
