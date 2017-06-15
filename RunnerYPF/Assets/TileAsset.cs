using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileAsset : MonoBehaviour {

	public Tile tile;
	private Collider collider;
	public bool isHole;

	public void Init(Tile _tile, bool isHole)
	{
		this.isHole = isHole;
		collider = GetComponent<Collider> ();
		if (collider != null) {
			if(isHole)
				collider.enabled = false;
			else
				collider.enabled = true;
		}
		this.tile = _tile;
		GetComponent<MeshRenderer> ().enabled = !isHole;

		Obstacle obstacle = GetComponentInChildren<Obstacle> ();
		if (obstacle != null)
			obstacle.SetState (!isHole);
	}
}
