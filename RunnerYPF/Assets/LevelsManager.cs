using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsManager : MonoBehaviour {
	
	Settings settings;
	public List<TileData> tilesData;
	public int tileID;

	void Start ()
	{
		Events.Restart += Restart;
		settings = GetComponent<Settings> ();
		foreach (LevelData levelData in settings.levels) {
			AddFreeTiles ();
			for (int a = 0; a < levelData.tiles.Length; a++) {
				TileData tileData = new TileData ();

				int height = levelData.tiles [a];
				int energyAssets = levelData.energyAssets [a];

				tileData.height = height;
				if (energyAssets != 0) {
					tileData.sceneObjectData = new SceneObjectData ();
					tileData.sceneObjectData.type = SceneObjectData.types.ENERGY;
					tileData.sceneObjectData.height = energyAssets;
				}
				tilesData.Add (tileData);
			}
		}
	}
	void Restart()
	{
		tileID = 0;
	}
	void AddFreeTiles()
	{
		for (int a = 0; a < 10; a++) {
			TileData tileData = new TileData ();
			tilesData.Add (tileData);
		}
	}
	public TileData GetNextTileData()
	{		
		TileData tileData = tilesData [tileID];
		tileID++;
		return tileData;
	}
}
