using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

	public static Game mInstance;

	[HideInInspector]
	public GameManager gameManager;
	[HideInInspector]
	public GroundManager groundManager;
	public static Game Instance
	{
		get
		{
			return mInstance;
		}
	}

	void Awake () {
		mInstance = this;
		gameManager = GetComponent<GameManager> ();
		groundManager = GetComponent<GroundManager> ();
	}
}
