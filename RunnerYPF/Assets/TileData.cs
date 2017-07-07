using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class TileData {
	
	public int height = 1;
	public bool final;
	public bool finalZone;
	public bool isRiver;
	public ObstaclesInLane obstaclesInLane;
	public enum ObstaclesInLane
	{
		NONE,
		CAR
	}
	public SceneObjectData sceneObjectData;
	public bool checkPoint;

}
