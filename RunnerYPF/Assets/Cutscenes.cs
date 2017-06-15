using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscenes : MonoBehaviour {

	public GameObject panel;

	void Start () {
		panel.SetActive (false);
		Events.GameOver += GameOver;
		Events.RestartAllOver += RestartAllOver;
	}
	void GameOver()
	{
		panel.SetActive (true);
	}
	void RestartAllOver()
	{
		panel.SetActive (false);
	}
}
