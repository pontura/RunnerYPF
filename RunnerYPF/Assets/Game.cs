using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

	public static Game mInstance;

	public static Game Instance
	{
		get
		{
			return mInstance;
		}
	}

	void Awake () {
		mInstance = this;
	}
}
