using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAsset : MonoBehaviour {

	public Rigidbody rigidBody;
	private Character character;
	public Animator anim;
	public GameObject energyParticles;

	void Start()
	{
		Events.OnGetEnergy += OnGetEnergy;
	}
	void OnGetEnergy()
	{
		CancelInvoke ();
		energyParticles.SetActive (true);
		energyParticles.GetComponentInChildren<ParticleSystem> ().Play ();
		Invoke ("ResetParticles", 0.5f);
	}
	void ResetParticles()
	{
		energyParticles.SetActive (false);
	}
	public void Init(Character _character)
	{
		this.character = _character;
	}
	void OnCollisionEnter(Collision  other)
	{
		if (character == null)
			return;
		
		if (other.gameObject.tag == "Tile" && character.state != Character.states.STARTJUMPING) {
			if(!other.gameObject.GetComponent<TileAsset>().isHole)
				character.OnFloor ();
		}
	}
	int aceleration;
	public void Run(int _aceleration)
	{
		if (aceleration == _aceleration)
			return;

		print ("Run " + _aceleration); 
		this.aceleration = _aceleration;
		OnFloor ();
	}
	public void Jump()
	{
		print ("Jump " );
		anim.Play ("jump");
	}
	public void Hit()
	{
		print ("Hit " );
		anim.Play ("hit");
	}
	public void OnFloor()
	{
		print ("OnFloor" );
		switch (aceleration) {
		case 0:
			anim.CrossFade ("runMid", 0.05f);
			break;
		case 1:
			anim.CrossFade ("runHigh", 0.05f);
			break;
		case -1:
			anim.CrossFade ("runLow", 0.05f);
			break;
		}
	}
	public void OnFinalDone()
	{
		anim.CrossFade ("final", 0.1f);
	}
}
