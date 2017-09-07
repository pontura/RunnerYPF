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
	public VideoClip[] levelClipOut;

	public Text titleText;
	public Text missionText;

	VideoPlayer[] vplayer;

	bool next;

	void Awake(){
		vplayer = GetComponents<VideoPlayer> ();
		foreach(VideoPlayer vp in vplayer)
		vp.loopPointReached += EndReached;
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

		vplayer[0].clip = levelClip [Data.Instance.playerData.level - 1];
		vplayer[0].Play ();
		Invoke ("SetNext", 5);
	}

	void SetNext(){
		next = true;
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
		if (next) {
			if (Input.anyKey) {
				vplayer[0].Pause ();
				vplayer[1].clip = levelClipOut [Data.Instance.playerData.level - 1];
				vplayer[1].Play ();
				next = false;
				Invoke ("Video1Stop", 0.2f);
			}
		}
	}

	void Video1Stop(){
		vplayer[0].Stop ();
	}
}
