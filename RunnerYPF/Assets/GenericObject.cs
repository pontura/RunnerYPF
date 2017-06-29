using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GenericObject : SceneObject {

	public GameObject[] level1Lane4;
	public GameObject[] level1Lane3;
	public GameObject[] level1Lane2;
	public GameObject[] level1Lane1;
	public GameObject[] level1LaneFront;

	public float speed = 0;
	bool isOut = false;
	SpriteRenderer sr;

	int laneCount=0;

	public Pattern fondoLevel1;

	[Serializable]
	public class Pattern {
		//public List<PatternModule> modules;
		public int offset;
		public int moduleLength;
		public Pattern[] subPatterns;
		public Pattern currentSubP;

		public void NextModule(int laneC){			
			currentSubP = subPatterns [GetModuleCount(laneC)];
		}

		public int GetModuleCount(int laneC){
			int result = ((laneC - offset) / moduleLength) % subPatterns.Length;
			return result;
		}

		public bool ShowSubPattern(int laneC){
			return ((laneC%moduleLength) - currentSubP.offset)% currentSubP.moduleLength == 0;
		}
	}

	[Serializable]
	public class PatternModule {
		public int[] length;
	}

	public void Init(int levelID, int laneID, int laneZ) {
		isOut = false;
		Reset ();
		GameObject go;
		speed = 0;
		laneCount = laneZ;
		Vector3 pos = transform.localPosition;
		pos.z =0;
		transform.localPosition = pos;

		if (levelID == 1) {
			if (laneID == 2)
				SetRandomOn (level1LaneFront);
			else if (laneID == -1) { 
				speed = 2f;
				SetOn (level1Lane1);
			} else if (laneID == -2) {
				speed = 1.2f;
				SetOn (level1Lane2);
				Invoke ("SetOutOfTile", 1);
			} /*else if (laneID == -3)
				SetOn (level1Lane3);*/
			else if (laneID == -4)
				AddLinePattern (fondoLevel1);
				//AddMolino ();
						 
		}



		sr = GetComponentInChildren<SpriteRenderer> ();
		if (sr == null)
			return;
		Color c = sr.color;
		c.a =1;
		sr.color = c;
	}
	void AddMolino()
	{
		level1Lane4 [UnityEngine.Random.Range (0, level1Lane4.Length)].SetActive (true);
	}
	void AddLinePattern(Pattern p)
	{		
		p.NextModule (laneCount);
		if(p.ShowSubPattern(laneCount))
			level1Lane4 [p.GetModuleCount(laneCount)].SetActive (true);
	}
	void SetOutOfTile()
	{
		transform.SetParent (Game.Instance.groundManager.container);
	}
	void Reset()
	{
		foreach (GameObject go in level1Lane4)
			go.SetActive (false);
		foreach (GameObject go in level1Lane3)
			go.SetActive (false);
		foreach (GameObject go in level1Lane2)
			go.SetActive (false);
		foreach (GameObject go in level1Lane1)
			go.SetActive (false);
		foreach (GameObject go in level1LaneFront)
			go.SetActive (false);
	}
	void SetRandomOn(GameObject[] all)
	{
		all [UnityEngine.Random.Range (0, all.Length)].SetActive (true);
	}
	void SetOn(GameObject[] go)
	{
		go [UnityEngine.Random.Range (0, go.Length)].SetActive (true);
	}
	void Update()
	{
		if (speed == 0)
			return;
		Vector3 pos = transform.localPosition;
		pos.z -= Time.deltaTime * speed;
		transform.localPosition = pos;
		if (isOut) {
			
			Color c = sr.color;
			c.a -= Time.deltaTime*6;
			sr.color = c;
			if (c.a < 0) {
				Poolme ();
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{				
		if (other.tag == "Border") {
			isOut = true;
		}
	}

}
