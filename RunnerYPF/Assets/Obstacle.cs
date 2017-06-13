using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {
	
	void OnTriggerEnter(Collider other)
	{
		Character character = other.gameObject.GetComponentInParent<Character> ();
		if (character != null) 
			character.HitWithObstacle ();
	}
	public void SetState(bool state)
	{
		GetComponent<BoxCollider> ().enabled = state;
	}
}
