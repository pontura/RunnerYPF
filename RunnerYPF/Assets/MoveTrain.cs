using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTrain : MonoBehaviour {
	
	public float speed;
	bool moveing;
	void Start()
	{
		moveing = true;
		Vector3 pos = transform.localPosition;
		pos.x = 0;
		transform.localPosition = pos;
	}
	void Update()
	{
		if (!moveing)
			return;
		Vector3 pos = transform.localPosition;
		pos.x -= Time.deltaTime * speed;
		transform.localPosition = pos;
	}
}
