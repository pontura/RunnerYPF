using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscenes : MonoBehaviour {

	public GameObject[] cutscenesWin;
	public GameObject[] cutscenesLose;
	public GameObject paralaxs;
	public GameObject grids;
	public GameObject pivot;

	bool show;

	void Start () {
		foreach(GameObject cs in cutscenesWin)
			cs.SetActive (false);
		foreach(GameObject cs in cutscenesLose)
			cs.SetActive (false);
		Events.GameOver += GameOver;

	//	Events.RestartAllOver += RestartAllOver;

		Events.OnLevelComplete += OnLevelComplete;
	}

	void OnDestroy(){
		Events.GameOver -= GameOver;
	
		//	Events.RestartAllOver -= RestartAllOver;

		Events.OnLevelComplete -= OnLevelComplete;
	}

	void Update(){
		if (show) {
			if (Input.anyKey)
				RestartAllOver ();
		}
	}

	void GameOver()
	{
		paralaxs.SetActive (false);
		grids.SetActive (false);
		pivot.SetActive (false);
		cutscenesLose [Data.Instance.playerData.level-1].SetActive (true);
		//cusc.SetActive (true);
		//acordarse de que el tiempo sea mayor al desaparecimiento del character
		Invoke("StartShowing", 6);
	}
	void StartShowing()
	{
		show = true;
	}
	void RestartAllOver()
	{
		cutscenesLose [Data.Instance.playerData.level-1].SetActive (false);
		cutscenesWin [Data.Instance.playerData.level-1].SetActive (false);
		paralaxs.SetActive (true);
		grids.SetActive (true);
		pivot.SetActive (true);
		Events.RestartAllOver ();
		show = false;
	}

	void OnLevelComplete(){
		paralaxs.SetActive (false);
		grids.SetActive (false);
		pivot.SetActive (false);
		cutscenesWin [Data.Instance.playerData.level-1].SetActive (true);
		show = true;
	}
}
