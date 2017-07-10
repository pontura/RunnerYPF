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

	int cutsceneLevel;

	void Start () {
		SetActiveAll (false);
		Events.GameOver += GameOver;

	//	Events.RestartAllOver += RestartAllOver;

		Events.OnLevelComplete += OnLevelComplete;
		Events.OnCutsceneComplete += OnCutsceneComplete;
	}

	void OnDestroy(){
		Events.GameOver -= GameOver;
	
		//	Events.RestartAllOver -= RestartAllOver;

		Events.OnLevelComplete -= OnLevelComplete;
		Events.OnCutsceneComplete -= OnCutsceneComplete;
	}

	void SetActiveAll(bool enable){
		foreach(GameObject cs in cutscenesWin)
			cs.SetActive (enable);
		foreach(GameObject cs in cutscenesLose)
			cs.SetActive (enable);
	}

	void OnCutsceneComplete(){
		RestartAllOver ();
	}

	void Update(){
		if (show) {
			if (Input.anyKey)
				RestartAllOver ();
		}
	}

	void GameOver()
	{
		cutsceneLevel = Data.Instance.playerData.level;
		paralaxs.SetActive (false);
		grids.SetActive (false);
		pivot.SetActive (false);
		cutscenesLose [cutsceneLevel-1].SetActive (true);
		//cusc.SetActive (true);
		//acordarse de que el tiempo sea mayor al desaparecimiento del character
		//Invoke("StartShowing", 1);
		show = true;
	}
	void StartShowing()
	{
		show = true;
	}
	void RestartAllOver()
	{
		cutscenesLose [cutsceneLevel-1].SetActive (false);
		cutscenesWin [cutsceneLevel-1].SetActive (false);
		paralaxs.SetActive (true);
		grids.SetActive (true);
		pivot.SetActive (true);
		Events.RestartAllOver ();
		show = false;
	}

	void OnLevelComplete(){
		cutsceneLevel = Data.Instance.playerData.level;
		paralaxs.SetActive (false);
		grids.SetActive (false);
		pivot.SetActive (false);
		cutscenesWin [cutsceneLevel-1].SetActive (true);
		//show = true;
	}
}
