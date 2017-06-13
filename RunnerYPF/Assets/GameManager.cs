using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public float distance;

	public float speed;
	private float realSpeed;

	public Camera camera;
	public Character character;

	public states state;

	public enum states
	{
		PLAYING,
		READY,
		DEAD
	}
	void Start () {
		Events.SpeedChange += SpeedChange;
		Events.OnCharacterDie += OnCharacterDie;
		Events.Restart += Restart;
		realSpeed = speed;
	}
	void OnDestroy () {
		Events.SpeedChange -= SpeedChange;
		Events.OnCharacterDie -= OnCharacterDie;
		Events.Restart -= Restart;
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
			realSpeed = speed + (2);
		else if (multiplier == -1)
			realSpeed = speed - 1;
		else
			realSpeed = speed;
	}

	void Update () {
		
		if (state != states.PLAYING)
			return;
		
		Vector3 pos = camera.transform.localPosition;
		pos.z += realSpeed * Time.deltaTime;
		distance = pos.z;			
		camera.transform.localPosition = pos;
		character.UpdateZPosition (pos.z);
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
		Events.Restart ();
	}
}
