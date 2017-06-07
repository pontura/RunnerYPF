using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundManager : MonoBehaviour {

	public GroundTilesLine groundTileLine;
	public Transform container;

	void Start () {
		for (int a = 0; a < 10; a++) {
			AddTileLine (a);
		}
	}

	void AddTileLine(int _z)
	{
		GroundTilesLine newGroundTileLine = Instantiate(groundTileLine);
		newGroundTileLine.transform.SetParent (container);
		newGroundTileLine.transform.localPosition = new Vector3 (0, 0, _z);
	}
}
