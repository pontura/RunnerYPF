﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class LevelData {
	
	public int level;
	public int[] tiles;
	public int[] other;
	public int[] powerUp;
	public int[] energyAssets;
	public bool final;

}
