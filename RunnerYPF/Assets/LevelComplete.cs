using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelComplete : MonoBehaviour {

	public GameObject panel;

	void Start () {
		panel.SetActive (false);
		Events.OnLevelComplete += OnLevelComplete;
		Events.Restart += Restart;
	}
	void OnLevelComplete()
	{
		panel.SetActive (true);
	}
	void Restart()
	{
		panel.SetActive (false);
	}
}
