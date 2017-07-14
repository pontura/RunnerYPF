using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAsset : MonoBehaviour {

	public Rigidbody rigidBody;
	private Character character;
	public Animator anim;
	public GameObject energyParticles;
	public GameObject powerupFX;

	AudioSource source;

	void Start()
	{
		source = GetComponent<AudioSource> ();
		Events.OnGetEnergy += OnGetEnergy;
		Events.OnLevelComplete += TurnOff;
		Events.OnPowerUp += OnPowerUp;
	}

	void OnDestroy () {
		Events.OnGetEnergy -= OnGetEnergy;
		Events.OnLevelComplete -= TurnOff;
		Events.OnPowerUp -= OnPowerUp;
	}
	void OnPowerUp(bool isOn)
	{
		powerupFX.SetActive (isOn);
	}
	void OnGetEnergy()
	{
		CancelInvoke ();
		source.pitch = 0.8f + UnityEngine.Random.value * 0.4f;
		source.PlayOneShot (source.clip);
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
		OnPowerUp (false);
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

		//print ("Run " + _aceleration); 
		this.aceleration = _aceleration;
		OnFloor ();
	}
	public void Jump()
	{
		//print ("Jump " );
		anim.Play ("jump");
	}
	public void Hit()
	{
		//print ("Hit " );
		anim.Play ("hit");
	}
	public void OnFloor()
	{
		//print ("OnFloor" );
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
		Run (0);
		anim.CrossFade ("stop", 0.01f);
		Invoke ("SetKinematic", 1f);
		//Invoke ("TurnOff", 2f);
	}

	void SetKinematic(){
		rigidBody.isKinematic = true;
	}

	void TurnOff(){
		transform.localPosition = new Vector3 (0, 1000, transform.localPosition.z); 
	}
}
