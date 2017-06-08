using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundManager : MonoBehaviour {

	public GroundTilesLine groundTileLine;
	public Transform container;
	public int z;

	void Start () {
		Events.OnAddNewLine += OnAddNewLine;

		for (int a = 0; a < 10; a++) {
			AddTileLine (a);
		}
	}
	void AddTileLine(int _z)
	{
		z = _z;
		GroundTilesLine newGroundTileLine = Instantiate(groundTileLine);
		newGroundTileLine.transform.SetParent (container);
		newGroundTileLine.transform.localPosition = new Vector3 (0, 0, _z);
	}
	void OnAddNewLine()
	{
		z++;
		AddTileLine(z);
	}
}
