using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : SceneObject {

	public Animation anim;
	GroundTilesLine line;
	BoxCollider collider;
	public Transform container;

	public void Init(GroundTilesLine line)
	{
		collider = GetComponent<BoxCollider> ();
		this.line = line;
		if (collider != null) {
			collider.enabled = true;
		} else {
			if (Random.Range (0, 100) < 50) {
				SceneObject so = Data.Instance.pool.AddObjectTo ("GenericObject", container);
			}
		}
	}
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Border") {
			line.DestroyLine ();
			Events.OnAddNewLine ();
			collider.enabled = false;
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
		if(collider != null)
			collider.enabled = false;
		Data.Instance.pool.PoolObject (this);
		if (container != null && container.childCount > 0) {
			SceneObject so = container.GetComponentInChildren<SceneObject> ();
			Data.Instance.pool.PoolObject (so);
		}
	}


}
