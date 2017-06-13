using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

	public CharacterAsset characterToInstantiate;
	private Rigidbody rb;
	private int jumpForce = 5000;
	public states state;
	public enum states
	{
		IDLE,
		RUNNING,
		STARTJUMPING,
		JUMPING,
		DEAD
	}
	CharacterAsset asset;
	void Start () {
		Events.Jump += Jump;
		Events.SpeedChange += SpeedChange;
		Events.Restart += Restart;
		asset = Instantiate (characterToInstantiate);
		asset.transform.SetParent (transform);
		Restart ();
	}
	float liveSince;
	void Restart()
	{
		liveSince = 0;
		state = states.IDLE;
		asset.transform.localPosition = new Vector3 (1, 4, 0);
		rb = asset.rigidBody;
		rb.velocity = Vector3.zero;
		asset.Init (this);
		state = states.RUNNING;
	}
	void OnDestroy () {
		Events.Jump -= Jump;
		Events.SpeedChange -= SpeedChange;
	}
	public void UpdateZPosition(float _z)
	{
		liveSince += Time.deltaTime;
		Vector3 pos = transform.localPosition;
		pos.z = _z-4;
		transform.localPosition = pos;
	}
	int jumps = 0;
	void Jump () {
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
	}
	void JumpStartEnd()
	{
		if (state == states.STARTJUMPING)
			state = states.JUMPING;
	}
	public void OnFloor()
	{
		jumps = 0;
		state = states.RUNNING;
	}
	void SpeedChange (int multiplier) {
		
	}
	public void HitWithObstacle()
	{
		if (liveSince < 1)
			return;
		print ("liveSince " + liveSince);
		state = states.DEAD;
		Events.OnCharacterDie ();
	}
}
