﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public float distance;

	public float speed;
	private float realSpeed;

	public Camera camera;
	public Character character;

	public states state;
	public bool dontAddGenericObjects;

	public enum states
	{
		PLAYING,
		ENDING,
		READY,
		DEAD
	}
	void Start () {
		Events.SpeedChange += SpeedChange;
		Events.OnCharacterDie += OnCharacterDie;
		Events.Restart += Restart;
		Events.RestartAllOver += Restart;
		Events.OnFinal += OnFinal;
		realSpeed = speed;
		Events.StartGame ();
	}
	void OnFinal()
	{
		StartCoroutine (OnFinalCoroutine ());
	}
	IEnumerator OnFinalCoroutine()
	{
		dontAddGenericObjects = true;
		yield return new WaitForSeconds (1);
		state = states.ENDING;
		yield return new WaitForSeconds (2);
		state = states.READY;
		Events.OnLevelComplete ();
		yield return new WaitForSeconds (1);
		Events.PoolAllObjects ();
		yield return new WaitForSeconds (2);
		Events.RestartAllOver ();
		dontAddGenericObjects = false;
		yield return null;
	}
	void Restart()
	{
		state = states.PLAYING;
		Vector3 pos = camera.transform.localPosition;
		pos.z = 4.5f;		
		camera.transform.localPosition = pos;
	}
	void SpeedChange(int multiplier)
	{
		if (multiplier == 1)
			realSpeed = speed + (2.5f);
		else if (multiplier == -1)
			realSpeed = speed - 2;
		else
			realSpeed = speed;
	}

	void Update () {
		if (state == states.PLAYING) {		
			Vector3 pos = camera.transform.localPosition;
			pos.z += realSpeed * Time.deltaTime;
			distance = pos.z;	
			camera.transform.localPosition = pos;
			character.UpdateZPosition (pos.z);
		} else if (state == states.ENDING) {
			Vector3 pos = character.transform.localPosition;
			pos.z += speed * Time.deltaTime;
			character.transform.localPosition = pos;
		}
	}
	void OnCharacterDie()
	{
		state = states.DEAD;
		StartCoroutine (RestartCoroutine());
	}
	IEnumerator RestartCoroutine()
	{
		yield return new WaitForSeconds (1);
		Events.PoolAllObjects ();
		yield return new WaitForSeconds (2);
		if (UI.Instance.lives > 0)
			Events.Restart ();
		else {
			Events.GameOver ();
			yield return new WaitForSeconds (3);
			Events.RestartAllOver();
		}
	}
}
