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

	private int totalTiles = 8;

	void Start () {
		state = states.STARTING;
		Events.PoolAllObjects += PoolAllObjects;
		if (transform == null)
			return;
		AddTiles ();
	}
	void OnDestroy () {
		Events.PoolAllObjects -= PoolAllObjects;
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

		if (_x != 1)
			newTile = AddGenericTile ();
		else
			newTile = AddPathTile ();

		tiles.Add (newTile);
		newTile.transform.localPosition = new Vector3 (_x, 0, 0);
		newTile.Init (this);

		newTile.AnimateIn ();
	}
	Tile AddGenericTile()
	{		
		SceneObject so = Data.Instance.pool.AddObjectTo ("TileGeneric", transform);
		if (so == null)
			Debug.LogError ("TileGeneric no Existe");
		return so.GetComponent<Tile>();
	}
	Tile AddPathTile()
	{
		Tile tile = Data.Instance.pool.AddObjectTo ("Tile", transform).GetComponent<Tile>();
		if (tile == null)
			Debug.LogError ("Tile no Existe");
		tile.tileData = Data.Instance.levelsManager.GetNextTileData();
		return tile;
	}

	public void DestroyLine()
	{
		foreach (Tile tile in tiles)
			tile.AnimateOut ();
		tiles.Clear ();
	}
	void OnTriggerEnter(Collider other)
	{				
		if (other.tag == "Border") {
			state = states.ENDING;
			DestroyLine ();
			Invoke ("Reset", 1);
			Events.OnAddNewLine ();
		}
	}
	void PoolAllObjects()
	{
		Invoke ("Reset", 0.2f);
	}
	void Reset()
	{
		tiles.Clear ();
		Destroy (gameObject);
	}
}
