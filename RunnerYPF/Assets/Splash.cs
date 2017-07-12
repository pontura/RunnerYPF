using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash : MonoBehaviour {

	public GameObject pivot;
	public GameObject ground;
	public GameObject parallax;

	// Use this for initialization
	void Start () {
		Events.RestartAllOver += RestartAllOver;		
	}

	void OnDestroy(){
		Events.RestartAllOver -= RestartAllOver;		
	}

	void RestartAllOver(bool newGame){
		if (newGame) {
			gameObject.SetActive (true);
			if (pivot != null)
				pivot.SetActive (false);
			if (ground != null)
				ground.SetActive (false);
			if (parallax != null)
				parallax.SetActive (false);
		}
	}
	
	void Update(){
		if (Game.Instance.gameManager.state==GameManager.states.SPLASH) {
			if (Input.anyKey) {
				Events.StartGame ();
				gameObject.SetActive (false);
				if (pivot != null)
					pivot.SetActive (true);
				if (ground != null)
					ground.SetActive (true);
				if (parallax != null)
					parallax.SetActive (true);
			}
		}
	}
}
