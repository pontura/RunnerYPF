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
	public Transform container;

	CharacterSfx sfx;

	public enum states
	{
		IDLE,
		RUNNING,
		STARTJUMPING,
		JUMPING,
		DEAD,
		DONE
	}

	void Start () {

		sfx = GetComponent<CharacterSfx> ();

		Events.Jump += Jump;
		Events.SpeedChange += SpeedChange;
		Events.Restart += Restart;
		Events.RestartAllOver += RestartAllOver;

		asset = Instantiate (characterToInstantiate);
		asset.transform.SetParent (container);
		asset.transform.localPosition = Vector3.zero;
		asset.transform.localEulerAngles = Vector3.zero;

		Restart ();
	}
	void RestartAllOver()
	{
		Restart ();
	}
	void Restart()
	{
		asset.enabled = true;
		asset.gameObject.SetActive (true);
		liveSince = 0;
		state = states.IDLE;
		sfx.state = state;
		asset.transform.localPosition = new Vector3 (0.7f, 4, 0);
		rb = asset.rigidBody;
		rb.velocity = Vector3.zero;
		asset.Init (this);
		Invoke ("DejarCaer", 0.25f);
		sfx.Restart ();
		OnFloor ();
	}

	void DejarCaer(){
		rb.isKinematic = false;
	}

	void OnDestroy () {
		Events.Jump -= Jump;
		Events.SpeedChange -= SpeedChange;
	}
	public void UpdateZPosition(float _z)
	{
		if (asset.transform.position.y < 0)
			Die ();
		
		liveSince += Time.deltaTime;
		Vector3 pos = transform.localPosition;
		pos.z = _z-4;
		transform.localPosition = pos;
	}
	int jumps = 0;
	void Jump () {
		if (state == states.DONE)
			return;
		if (Game.Instance.gameManager.state != GameManager.states.PLAYING)
			return;
		
		if (state == states.JUMPING) {
			if(jumps > 1) return;
		} else if (state != states.RUNNING)
			return;
		if (jumps == 0) {
			state = states.STARTJUMPING;
			sfx.state = state;
			Invoke ("JumpStartEnd", 0.1f);
		} else {
			state = states.JUMPING;
			sfx.state = state;
			sfx.Jump (false);
		}
		jumps++;
		rb.velocity = Vector3.zero;
		rb.AddForce (0, jumpForce, 0);
		asset.Jump ();
	}
	void JumpStartEnd()
	{
		if (state == states.DONE)
			return;
		if (state == states.STARTJUMPING) {
			state = states.JUMPING;
			sfx.state = state;
			sfx.Jump (true);
		}
	}
	public void OnFloor()
	{
		if (state == states.DONE)
			return;
		if (state == states.RUNNING || state == states.DEAD)
			return;
		jumps = 0;
		state = states.RUNNING;
		sfx.state = state;
		asset.OnFloor ();
	}
	int realSpeed = 0;
	void SpeedChange (int multiplier) {
		if (state == states.DONE)
			return;
		this.realSpeed = multiplier;
		if (Game.Instance.gameManager.state != GameManager.states.PLAYING)
			return;
		asset.Run (realSpeed);
		sfx.Run (multiplier);

	}
	public void HitWithObstacle()
	{
		if (liveSince < 1)
			return;
		Die ();
		asset.Hit();
	}
	public void FinalDone()
	{
		if (state == states.DONE)
			return;
		state = states.DONE;
		sfx.state = state;
		sfx.Stop ();
		asset.OnFinalDone ();
		Invoke ("WinSfx", 1.5f);
	}

	void WinSfx(){
		sfx.Win ();
	}

	void Die()
	{
		if (state == states.DEAD)
			return;
		sfx.Die ();
		state = states.DEAD;
		sfx.state = state;
		Events.OnCharacterDie ();
	}


}
