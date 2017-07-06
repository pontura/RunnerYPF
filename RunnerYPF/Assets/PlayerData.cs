using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour {

	public int level;

	// Use this for initialization
	void Start () {
		Events.OnLevelComplete += OnLevelComplete;
	}

	void OnDestroy(){
		Events.OnLevelComplete -= OnLevelComplete;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnLevelComplete(){		
		level++;
		if (level == 2) {
			level = 3;
		} else if (level > 3) {
			level = 1;
		}
		print ("level: "+level);
	}
}
