using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

	public Animation anim;
	GroundTilesLine line;

	public void Init(GroundTilesLine line)
	{
		this.line = line;
	}
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Border") {
			line.DestroyLine ();
			Events.OnAddNewLine ();
			GetComponent<BoxCollider> ().enabled = false;
		}
	}
	public void AnimateIn()
	{
		anim.Play ("in");
	}
	public void AnimateOut()
	{
		Invoke ("Reset", 0.2f);
		anim.Play ("out");
	}
	void Reset()
	{
		Destroy (line.gameObject);
	}


}
