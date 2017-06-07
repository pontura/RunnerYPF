using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTilesLine : MonoBehaviour {

	public List<Tile> tiles;
	public states state;
	public enum states
	{
		STARTING,
		IDLE,
		ENDING
	}
	public Tile tile;
	public Tile tileGeneric;
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
		Tile newTile;

		if(_x != 1)
			newTile= Instantiate (tileGeneric);
		else
			newTile= Instantiate (tile);

		tiles.Add (newTile);
		
		newTile.transform.SetParent (transform);
		newTile.transform.localPosition = new Vector3 (_x, 0, 0);
		newTile.Init (this);

		newTile.AnimateIn ();
	}
	public void DestroyLine()
	{
		foreach (Tile tile in tiles)
			tile.AnimateOut ();
	}

}
