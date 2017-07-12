using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour {

	public int level;

	// Use this for initialization
	void Start () {
		Events.OnLevelComplete += OnLevelComplete;
		Events.GameOver += GameOver;
	}

	void OnDestroy(){
		Events.OnLevelComplete -= OnLevelComplete;
		Events.GameOver -= GameOver;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void GameOver(){
			level = 1;
	}

	void OnLevelComplete(){		
		Invoke ("NextLevel", 0.1f);
	}

	void NextLevel(){
		level++;
		if (level == 2) {
			level = 3;
		} else if (level > 3) {
			level = 1;
		}
		print ("level: "+level);
	}
}
