using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelComplete : MonoBehaviour {

	public GameObject panel;

	void Start () {
		panel.SetActive (false);
		Events.OnLevelComplete += OnLevelComplete;
		Events.RestartAllOver += RestartAllOver;
	}
	void OnLevelComplete()
	{
		panel.SetActive (true);
	}
	void RestartAllOver()
	{
		panel.SetActive (false);
	}
}
