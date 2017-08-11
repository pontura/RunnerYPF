using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Mission : MonoBehaviour {

	public GameObject ui;
	public GameObject[] levelSign;
	public string[] levelTitle;
	public string[] levelMission;
	public VideoClip[] levelClip;

	public Text titleText;
	public Text missionText;

	VideoPlayer vplayer;

	void Awake(){
		vplayer = GetComponent<VideoPlayer> ();
		vplayer.loopPointReached += EndReached;
	}

	// Use this for initialization
	void Start () {
		/*titleText.text = levelTitle [Data.Instance.playerData.level - 1];	
		missionText.text = levelMission [Data.Instance.playerData.level - 1];
		Invoke ("Hide",5f);*/

		Events.RestartAllOver += RestartAllOver;
		Events.StartGame += Ready;
	}

	void OnEnable(){				
		Ready ();
	}

	void OnDestroy(){
		Events.RestartAllOver -= RestartAllOver;
		Events.StartGame -= Ready;
	}

	void RestartAllOver(bool newGame){
		if(!newGame)
			Ready ();
	}

	void Ready(){
		ui.SetActive (false);
		gameObject.SetActive (true);
		//titleText.text = levelTitle [Data.Instance.playerData.level - 1];	
		//missionText.text = levelMission [Data.Instance.playerData.level - 1];

		//levelSign[Data.Instance.playerData.level - 1].SetActive(true);
		//Invoke ("Hide",11f);	

		vplayer.clip = levelClip [Data.Instance.playerData.level - 1];
		vplayer.Play ();
	}

	void EndReached(UnityEngine.Video.VideoPlayer vp)
	{
		gameObject.SetActive (false);
		ui.SetActive (true);
		Events.LevelStart ();
	}

	void Hide(){
		levelSign[Data.Instance.playerData.level - 1].SetActive(false);
		gameObject.SetActive (false);
		ui.SetActive (true);
		Events.LevelStart ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
