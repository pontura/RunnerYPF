using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Events.RestartAllOver += RestartAllOver;		
	}

	void OnDestroy(){
		Events.RestartAllOver -= RestartAllOver;		
	}

	void RestartAllOver(bool newGame){
		if(newGame)
			gameObject.SetActive (true);
	}
	
	void Update(){
		if (Game.Instance.gameManager.state==GameManager.states.SPLASH) {
			if (Input.anyKey) {
				Events.StartGame ();
				gameObject.SetActive (false);
			}
		}
	}
}
