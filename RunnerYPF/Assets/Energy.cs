using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Energy : SceneObject {

	bool catched;

	Character character;
	[Serializable]
	public class EnergyAsset
	{
		public GameObject active;
		public GameObject itemToBar;
	}
	public EnergyAsset[] level1;

	EnergyAsset activeAsset;

	public void Init(int _y)
	{

		foreach (EnergyAsset ea in level1) {
			ea.active.SetActive (false);
			ea.itemToBar.SetActive (false);
		}
		
		activeAsset = level1 [UnityEngine.Random.Range (0, level1.Length)];
		activeAsset.active.SetActive (true);

		catched = false;
		Vector3 pos = transform.localPosition;
		pos.y = _y;
		transform.localPosition = pos;
		transform.localScale = Vector3.one;
	}
	void OnTriggerEnter(Collider other)
	{
		character = other.gameObject.GetComponentInParent<Character> ();
		if (character != null) {
			activeAsset.active.SetActive (false);
			activeAsset.itemToBar.SetActive (true);

			Events.OnGetEnergy ();
			catched = true;
			Invoke ("Done", 0.25f);
		}
	}

	Vector3 dest;
	void Update()
	{		
		if (catched) {
			transform.SetParent (Game.Instance.gameManager.camera.transform);
			dest = transform.position;
			dest.z += 10;
			dest.y = 3;
			transform.position = Vector3.Lerp (transform.position, dest, Time.deltaTime*5);
			//transform.localScale *= 1.01f;
		}
	}
	void Done()
	{
		catched = false;
		Poolme ();
	}
}
