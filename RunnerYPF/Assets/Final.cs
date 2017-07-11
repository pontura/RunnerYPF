using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Final : SceneObject {

	public bool trigged;

	// Use this for initialization
	void Start () {
		Events.RestartAllOver += RestartAllOver;
	}

	void OnDestroy(){
		Events.RestartAllOver -= RestartAllOver;
	}

	void OnTriggerEnter(Collider other)
	{
		if (!trigged) {			
			Events.OnFinal ();
		
			Character character = other.gameObject.GetComponentInParent<Character> ();
			if (character != null)
				character.FinalDone ();		
				
			trigged = true;
		}
	}

	void RestartAllOver(bool newGame){
		trigged = false;
	}
}
