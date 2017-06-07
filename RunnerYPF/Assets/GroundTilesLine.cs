using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTilesLine : MonoBehaviour {

	public states state;
	public enum states
	{
		STARTING,
		IDLE,
		ENDING
	}
	public Tile tile;
	private int totalTiles = 8;

	void Start () {
		AddTiles ();
	}
	public void AddTiles()
	{
		for (int a = 0; a < totalTiles; a++) {
			AddTile(a-(totalTiles/2));
		}
	}
	public void AddTile(int _x)
	{
		Tile newTile = Instantiate (tile);
		newTile.transform.SetParent (transform);
		newTile.transform.localPosition = new Vector3 (_x, 0, 0);
	}

}
