using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : SceneObject {

	bool catched;

	Character character;

	public void Init(int _y)
	{
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
