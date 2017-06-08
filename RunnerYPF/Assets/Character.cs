using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

	public CharacterAsset asset;
	private Rigidbody rb;
	private int jumpForce = 5000;
	public states state;
	public enum states
	{
		IDLE,
		RUNNING,
		STARTJUMPING,
		JUMPING
	}

	void Start () {
		Events.Jump += Jump;
		Events.SpeedChange += SpeedChange;

		CharacterAsset newAsset = Instantiate (asset);
		newAsset.transform.SetParent (transform);
		newAsset.transform.localPosition = new Vector3 (1, 5, 0);

		rb = newAsset.rigidBody;
		newAsset.Init (this);

		state = states.RUNNING;
	}
	public void UpdateZPosition(float _z)
	{
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
}
