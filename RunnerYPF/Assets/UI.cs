using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour {

	public Text scoreField;
	public Text timerField;
	public Text livesField;

	public int score;
	private int totalSec = 59;
	public int sec;
	public int lives;
	public static UI Instance;
	bool timeRunning;
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
		else
			sec--;

	}
	void OnGetEnergy () {
		score++;
		SetScore ();
	}

	void SetScore()
	{
		scoreField.text = "Score: " + score;
	}
	void SetLives()
	{
		livesField.text = "Vidas: " + lives;
	}
}
