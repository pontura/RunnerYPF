using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileAsset : MonoBehaviour {

	public GameObject river;
	public Tile tile;
	private Collider collider;
	public bool isHole;

	public void Init(Tile _tile, bool isHole, bool isRiver)
	{
		this.isHole = isHole;
		if (isRiver) {
			river.SetActive (true);
			gameObject.SetActive (false);
		} else {
			river.SetActive (false);
			gameObject.SetActive (true);
		}
		collider = GetComponent<Collider> ();
		if (collider != null) {
			if(isHole)
				collider.enabled = false;
			else
				collider.enabled = true;
		}
		this.tile = _tile;

		if(isHole)
			gameObject.SetActive( false );

		Obstacle obstacle = GetComponentInChildren<Obstacle> ();
		if (obstacle != null)
			obstacle.SetState (!isHole);
	}
}
