using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Final : SceneObject {

	void OnTriggerEnter(Collider other)
	{
		Events.OnFinal ();
		Character character = other.gameObject.GetComponentInParent<Character> ();
		if (character != null) 
			character.FinalDone ();		
	}
}
