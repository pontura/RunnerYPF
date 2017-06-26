using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

	public CharacterAsset characterToInstantiate;
	private Rigidbody rb;
	private int jumpForce = 8000;
	public states state;
	private float liveSince;
	public CharacterAsset asset;


	public enum states
	{
		IDLE,
		RUNNING,
		STARTJUMPING,
		JUMPING,
		DEAD
	}

	void Start () {
		Events.Jump += Jump;
		Events.SpeedChange += SpeedChange;
		Events.Restart += Restart;
		Events.RestartAllOver += RestartAllOver;

		asset = Instantiate (characterToInstantiate);
		asset.transform.SetParent (transform);
		Restart ();
	}
	void RestartAllOver()
	{
		Restart ();
	}
	void Restart()
	{
		liveSince = 0;
		state = states.IDLE;
		asset.transform.localPosition = new Vector3 (0.7f, 4, 0);
		rb = asset.rigidBody;
		rb.velocity = Vector3.zero;
		asset.Init (this);
		OnFloor ();
	}
	void OnDestroy () {
		Events.Jump -= Jump;
		Events.SpeedChange -= SpeedChange;
	}
	public void UpdateZPosition(float _z)
	{
		if (asset.transform.position.y < 0 && state == states.RUNNING)
			Die ();
		
		liveSince += Time.deltaTime;
		Vector3 pos = transform.localPosition;
		pos.z = _z-4;
		transform.localPosition = pos;
	}
	int jumps = 0;
	void Jump () {
		
		if (Game.Instance.gameManager.state != GameManager.states.PLAYING)
			return;
		
		if (state == states.JUMPING) {
			if(jumps > 1) return;
		} else if (state != states.RUNNING)
			return;
		if (jumps == 0) {
			state = states.STARTJUMPING;
			Invoke ("JumpStartEnd", 0.1f);
		} else {
			state = states.JUMPING;
		}
		jumps++;
		rb.velocity = Vector3.zero;
		rb.AddForce (0, jumpForce, 0);
		asset.Jump ();
	}
	void JumpStartEnd()
	{
		if (state == states.STARTJUMPING)
			state = states.JUMPING;
	}
	public void OnFloor()
	{
		if (state == states.RUNNING || state == states.DEAD)
			return;
		jumps = 0;
		state = states.RUNNING;
		asset.OnFloor ();
	}
	int realSpeed = 0;
	void SpeedChange (int multiplier) {
		this.realSpeed = multiplier;
		if (Game.Instance.gameManager.state != GameManager.states.PLAYING)
			return;
		asset.Run (realSpeed);
	}
	public void HitWithObstacle()
	{
		if (liveSince < 1)
			return;
		Die ();
		asset.Hit();
	}
	void Die()
	{
		if (state == states.DEAD)
			return;
		state = states.DEAD;
		Events.OnCharacterDie ();
	}
}
