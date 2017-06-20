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
		Events.RestartAllOver += RestartAllOver;
		settings = GetComponent<Settings> ();
		foreach (LevelData levelData in settings.levels) {
			AddFreeTiles ();
			for (int a = 0; a < levelData.tiles.Length; a++) {
				
				TileData tileData = new TileData ();

				if (levelData.other != null && levelData.other [a] == 1)
					tileData.isRiver = true;
				else if (levelData.other != null && levelData.other [a] == 2)
					tileData.obstaclesInLane = TileData.ObstaclesInLane.CAR;
				else {
					tileData.isRiver = false;
					tileData.obstaclesInLane = TileData.ObstaclesInLane.NONE;
				}
				
				if (levelData.final == true && a == 0)
					tileData.final = true;
				
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
	void RestartAllOver()
	{
		tileID = 0;
	}
	void Restart()
	{
		for (int a = tileID; a > 0; a--) {
			if (tilesData [a].checkPoint) {
				tileID = a;
				return;
			}
		}
	}
	void AddFreeTiles()
	{
		for (int a = 0; a < 10; a++) {
			TileData tileData = new TileData ();
			if (a == 0)
				tileData.checkPoint = true;
			tilesData.Add (tileData);
		}
	}
	public TileData GetTileData()
	{
		return tilesData [tileID];
	}
	public TileData NextTileData()
	{		
		TileData tileData = tilesData [tileID];
		tileID++;
		return tileData;
	}
}
