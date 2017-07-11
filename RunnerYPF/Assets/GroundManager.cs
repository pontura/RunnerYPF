using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundManager : MonoBehaviour {

	public GroundTilesLine groundTileLine;
	public Transform container;
	public int z;

	void Start () {
		Events.Restart += Restart;
		Events.RestartAllOver += RestartAllOver;
		Restart ();
	}
	void RestartAllOver(bool newGame)
	{
		Restart ();
	}
	void Restart()
	{
		z = 0;
		for (int a = 0; a < 10; a++) {
			AddNewTileLine (a);
		}
	}
	void AddNewTileLine(int _z)
	{
		z = _z;
		GroundTilesLine newGroundTileLine = Instantiate(groundTileLine);
		newGroundTileLine.transform.SetParent (container);
		newGroundTileLine.transform.localPosition = new Vector3 (0, 0, _z);
	}
	public void OnAddNewLine()
	{
		z++;
		AddNewTileLine (z);
		//groundTilesLine.transform.localPosition = new Vector3 (0, 0, z);
	}
}
