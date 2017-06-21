using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxPanel : MonoBehaviour {

	public int speed;
	public int assetLength;

	void Update () {
		GameManager gameManager = Game.Instance.gameManager;

		if (gameManager.state != GameManager.states.PLAYING)
			return;
		
		Vector3 pos = transform.localPosition;
		pos.z -= (gameManager.realSpeed / speed ) * Time.deltaTime;	
		if (pos.z < -assetLength)
			pos.z = 0;
		transform.localPosition = pos;
	}
}
