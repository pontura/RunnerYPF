using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSceneObject : SceneObject {

	public float speed;
	bool moveing;
	public void Init()
	{
		moveing = true;
		Vector3 pos = transform.localPosition;
		pos.z = 0;
		transform.localPosition = pos;
	}
	void Update()
	{
		if (!moveing)
			return;
		Vector3 pos = transform.localPosition;
		pos.z -= Time.deltaTime * speed;
		transform.localPosition = pos;
	}
}
