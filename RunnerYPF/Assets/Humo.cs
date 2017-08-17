using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Humo : MonoBehaviour {

	ParticleSystem ps;

	// Use this for initialization
	void Awake () {	
		ps = GetComponent<ParticleSystem> ();
		Events.LevelStart += LevelStart;
		if(Game.Instance.gameManager.state!=GameManager.states.READY)
			ps.Play ();
	}

	void OnEnable(){
		if(ps==null)
			ps = GetComponent<ParticleSystem> ();
		if(Game.Instance.gameManager.state!=GameManager.states.READY)
			ps.Play ();
	}

	void OnDestroy(){
		Events.LevelStart -= LevelStart;
	}

	void LevelStart(){
		ps.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
