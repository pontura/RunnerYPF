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

		RestartAllOver (true);
	}
	void RestartAllOver(bool newGame)
	{
		tileID = 0;
		tilesData.Clear ();
		List<LevelData> levels;

		switch (Data.Instance.playerData.level) {
		case 1:
			levels = settings.level1;
			break;
		case 2:
			levels = settings.level2;
			break;
		default:
			levels = settings.level3;
			break;
		}

		foreach (LevelData levelData in levels) {
			AddFreeTiles ();
			for (int a = 0; a < levelData.tiles.Length; a++) {

				TileData tileData = new TileData ();

				int powerUp = 0;
				if (levelData.powerUp != null && levelData.powerUp [a] !=0)
					powerUp = levelData.powerUp[a];
				
				if (levelData.other != null && levelData.other [a] == 1)
					tileData.isRiver = true;
				else if (levelData.other != null && levelData.other [a] == 2)
					tileData.obstaclesInLane = TileData.ObstaclesInLane.CAR;
				else {
					tileData.isRiver = false;
					tileData.obstaclesInLane = TileData.ObstaclesInLane.NONE;
				}

				if (levelData.final == true && a == 2) {
					tileData.final = true;
					tileData.finalZone = true;
				}else if(levelData.final)
					tileData.finalZone = true;

				int height = levelData.tiles [a];
				int energyAssets = levelData.energyAssets [a];

				tileData.height = height;
				if (powerUp != 0) {
					tileData.sceneObjectData = new SceneObjectData ();
					tileData.sceneObjectData.type = SceneObjectData.types.POWERUP;
					tileData.sceneObjectData.height = powerUp;
				} else
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
