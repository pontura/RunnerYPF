using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mission : MonoBehaviour {

	public string[] levelTitle;
	public string[] levelMission;

	public Text titleText;
	public Text missionText;

	// Use this for initialization
	void Start () {
		/*titleText.text = levelTitle [Data.Instance.playerData.level - 1];	
		missionText.text = levelMission [Data.Instance.playerData.level - 1];
		Invoke ("Hide",5f);*/

		Events.RestartAllOver += RestartAllOver;
		Events.StartGame += Ready;
	}

	void OnDestroy(){
		Events.RestartAllOver -= RestartAllOver;
		Events.StartGame -= Ready;
	}

	void RestartAllOver(bool newGame){
		Ready ();
	}

	void Ready(){
		gameObject.SetActive (true);
		titleText.text = levelTitle [Data.Instance.playerData.level - 1];	
		missionText.text = levelMission [Data.Instance.playerData.level - 1];
		Invoke ("Hide",2f);	
	}

	void Hide(){
		gameObject.SetActive (false);
		Events.LevelStart ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
