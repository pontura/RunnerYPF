using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Events {
	
	public static System.Action StartGame = delegate { };
	public static System.Action CreatorReset = delegate { };
	public static System.Action OnCharacterDie = delegate { };
	public static System.Action Jump = delegate { };
	public static System.Action OnGetEnergy = delegate { };
	public static System.Action<int> SpeedChange = delegate { };
	public static System.Action PoolAllObjects = delegate { };
	public static System.Action Restart = delegate { };
	public static System.Action<bool> RestartAllOver = delegate { };
	public static System.Action LevelStart = delegate { };
	public static System.Action GameOver = delegate { };
	public static System.Action OnFinal = delegate { };
	public static System.Action OnTimeOver = delegate { };
	public static System.Action OnLevelComplete = delegate { };
	public static System.Action<bool> OnCutsceneComplete = delegate { };
	public static System.Action OnCutsceneFinal = delegate { };

}

