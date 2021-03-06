﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GenericObject : SceneObject {

	public GameObject[] level1Lane4;
	public GameObject[] level1Lane3;
	public GameObject[] level1Lane2;
	public GameObject[] level1Lane1;
	public GameObject[] level1Lane0;
	public GameObject[] level1LaneFront;

	public GameObject[] level2Lane4;
	public GameObject[] level2Lane3;
	public GameObject[] level2Lane2;
	public GameObject[] level2Lane1;
	public GameObject[] level2Lane0;
	public GameObject[] level2LaneFront;

	public GameObject[] level3Lane4;
	public GameObject[] level3Lane3;
	public GameObject[] level3Lane2;
	public GameObject[] level3Lane1;
	public GameObject[] level3Lane0;
	public GameObject[] level3LaneFront;

	public float speed = 0;
	bool isOut = false;
	SpriteRenderer sr;

	int laneCount=0;

	public Pattern level1Lane4P;
	public Pattern level1Lane1P;
	public Pattern level1LaneFront2P;

	public Pattern level2Lane4P;
	public Pattern level2Lane3P;
	public Pattern level2Lane2P;

	public Pattern level3Lane4P;
	public Pattern level3Lane2P;

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
			//print (laneC + "-" + offset + "/" + moduleLength + "%" + subPatterns.Length);
			int result = ((laneC - offset) / moduleLength) % subPatterns.Length;
			return result;
		}

		public bool ShowSubPattern(int laneC){
			//print (laneC + "%" + moduleLength + "%" + currentSubP.moduleLength+"="+((laneC%moduleLength)%currentSubP.moduleLength));
			bool result = false;
			if ((laneC % moduleLength) > currentSubP.offset) {
				result = ((laneC % moduleLength) % currentSubP.moduleLength) == 0;;
			}
			return result;
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

		//level 1
		if (levelID == 1) {
			if (laneID == 3) {
				AddLinePattern (level1LaneFront2P, level1LaneFront,true);
			} else if (laneID == -1) {
				AddLinePattern (level1Lane1P, level1Lane1,false);
			} else if (laneID == -4) {
				AddLinePattern (level1Lane4P, level1Lane4,false);
			}
		}

		//level 2
		else if (levelID == 2) {
			if (laneID == 2) {
				AddLinePattern (level2Lane2P, level2LaneFront,false,0.2f);
			}else if (laneID == 1){
				SetOn (level2Lane0);
			}else if (laneID == -1){
				AddLinePattern (level2Lane2P, level2Lane2,false,0.1f);
			}else if (laneID == -3){
				AddLinePattern (level2Lane3P, level2Lane3,false);
			}else if (laneID == -4){
				AddLinePattern (level2Lane4P, level2Lane4,false);
			}
		}

		//level 3
		else if (levelID == 3) {
			if (laneID == 2) {
				SetRandomOn (level3LaneFront);
			}else if (laneID == 0) { 
				speed = 2f;
				SetOn (level3Lane1);
			} else if (laneID == -2) {
				AddLinePattern (level3Lane2P, level3Lane2,false);
			} else if (laneID == -4){
				AddLinePattern (level3Lane4P, level3Lane4,false);
			}

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
		if(Data.Instance.playerData.level==1)
			level1Lane4 [UnityEngine.Random.Range (0, level1Lane4.Length)].SetActive (true);
		else if(Data.Instance.playerData.level==2)
			level2Lane4 [UnityEngine.Random.Range (0, level2Lane4.Length)].SetActive (true);
		else if(Data.Instance.playerData.level==3)
			level3Lane4 [UnityEngine.Random.Range (0, level3Lane4.Length)].SetActive (true);
	}

	void AddLinePattern(Pattern p, GameObject[] lane, bool isFront=false, float prob=1f )
	{		
		p.NextModule (laneCount);
		if (p.ShowSubPattern (laneCount)) {
			if (prob > UnityEngine.Random.value) {
				if (lane [p.GetModuleCount (laneCount)] != null) {
					lane [p.GetModuleCount (laneCount)].SetActive (true);
					if (isFront) {
						SpriteRenderer[] srs = lane [p.GetModuleCount (laneCount)].GetComponentsInChildren<SpriteRenderer> ();
						foreach (SpriteRenderer sr in srs)
							if (sr.gameObject.activeSelf)
								sr.sortingOrder = 999;
					}/* else {
					SpriteRenderer[] srs = lane [p.GetModuleCount (laneCount)].GetComponentsInChildren<SpriteRenderer> ();
					foreach (SpriteRenderer sr in srs)
						sr.sortingOrder = 0;
				}*/
				}
			}
		}
	}


	void SetOutOfTile()
	{
		transform.SetParent (Game.Instance.groundManager.container);
	}
	void Reset()
	{
		foreach (GameObject go in level1Lane4)
			if(go!=null)go.SetActive (false);
		foreach (GameObject go in level1Lane3)
			if(go!=null)go.SetActive (false);
		foreach (GameObject go in level1Lane2)
			if(go!=null)go.SetActive (false);
		foreach (GameObject go in level1Lane1)
			if(go!=null)go.SetActive (false);
		foreach (GameObject go in level1Lane0)
			if(go!=null)go.SetActive (false);
		foreach (GameObject go in level1LaneFront)
			if(go!=null)go.SetActive (false);


		foreach (GameObject go in level2Lane4)
			if(go!=null)go.SetActive (false);
		foreach (GameObject go in level2Lane3)
			if(go!=null)go.SetActive (false);
		foreach (GameObject go in level2Lane2)
			if(go!=null)go.SetActive (false);
		foreach (GameObject go in level2Lane1)
			if(go!=null)go.SetActive (false);
		foreach (GameObject go in level2Lane0)
			if(go!=null)go.SetActive (false);
		foreach (GameObject go in level2LaneFront)
			if(go!=null)go.SetActive (false);

		foreach (GameObject go in level3Lane4)
			if(go!=null)go.SetActive (false);
		foreach (GameObject go in level3Lane3)
			if(go!=null)go.SetActive (false);
		foreach (GameObject go in level3Lane2)
			if(go!=null)go.SetActive (false);
		foreach (GameObject go in level3Lane1)
			if(go!=null)go.SetActive (false);
		foreach (GameObject go in level3Lane0)
			if(go!=null)go.SetActive (false);
		foreach (GameObject go in level3LaneFront)
			if(go!=null)go.SetActive (false);
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
