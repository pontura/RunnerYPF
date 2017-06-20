using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericObject : SceneObject {

	public GameObject[] level1Generic;
	public GameObject[] level1LaneFront;
	public float speed = 0;
	bool isOut = false;
	SpriteRenderer sr;

	public void Init(int levelID, int laneID) {
		isOut = false;
		Reset ();
		GameObject go;
		speed = 0;

		Vector3 pos = transform.localPosition;
		pos.z =0;
		transform.localPosition = pos;

		if (levelID == 1) {
			if (laneID == 2)
				SetRandomOn (level1LaneFront);
			else if (laneID == -3 || laneID == -2)
			{
				speed = 2;
				SetOn (level1Generic[0]);
			} else if (laneID == -1 || laneID == 0)
			{
				speed = -1.2f;
				SetOn (level1Generic[1]);
				Invoke ("SetOutOfTile", 1);
			}
		}



		sr = GetComponentInChildren<SpriteRenderer> ();
		if (sr == null)
			return;
		Color c = sr.color;
		c.a =1;
		sr.color = c;
	}
	void SetOutOfTile()
	{
		transform.SetParent (Game.Instance.groundManager.container);
	}
	void Reset()
	{
		foreach (GameObject go in level1Generic)
			go.SetActive (false);
		foreach (GameObject go in level1LaneFront)
			go.SetActive (false);
	}
	void SetRandomOn(GameObject[] all)
	{
		SetOn(all [Random.Range (0, all.Length)]);
	}
	void SetOn(GameObject go)
	{
		go.SetActive (true);
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
