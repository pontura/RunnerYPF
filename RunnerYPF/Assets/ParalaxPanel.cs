using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxPanel : MonoBehaviour {

	public int speed;
	public int assetLength;

	void Update () {	

		if (Game.Instance.gameManager.state != GameManager.states.PLAYING)
			return;
		
		Vector3 pos = transform.localPosition;
		pos.z -= (Game.Instance.gameManager.realSpeed / speed ) * Time.deltaTime;	
		if (pos.z < -assetLength)
			pos.z = 0;
		transform.localPosition = pos;
	}
}
