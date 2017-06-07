using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public float distance;
	public float speed;
	public Camera camera;

	public states state;

	public enum states
	{
		IDLE,
		PLAYING
	}
	void Start () {
		
	}

	void Update () {
		
		if (state != states.PLAYING)
			return;
		
		Vector3 pos = camera.transform.localPosition;
		pos.z += speed * Time.deltaTime;
		distance = pos.z;			
		camera.transform.localPosition = pos;
	}
}
