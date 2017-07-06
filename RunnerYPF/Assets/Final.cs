using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Final : SceneObject {

	bool trigged;

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
}
