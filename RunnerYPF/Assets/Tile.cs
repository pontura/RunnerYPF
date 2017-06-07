using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

	public Animation anim;

	void Start () {
		
	}	
	void AnimateIn()
	{
		anim.Play ("in");
	}
	void AnimateOut()
	{
		anim.Play ("out");
	}

}
