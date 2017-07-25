using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Energy : SceneObject {

	bool catched;
	bool catchedByPowerUp;

	AudioSource source;

	Character character;
	[Serializable]
	public class EnergyAsset
	{
		public GameObject active;
		public GameObject itemToBar;
		public bool isPowerUp;
	}
	public EnergyAsset[] level1;
	public EnergyAsset[] level2;
	public EnergyAsset[] level3;

	public bool isPowerUP;

	EnergyAsset activeAsset;

	public Vector3 colliderScale;

	void Start()
	{
		Events.OnPowerUp += OnPowerUp;
	}
	void OnDestroy()
	{
		Events.OnPowerUp -= OnPowerUp;
	}
	void OnPowerUp(bool isOn)
	{
		if (isOn && !isPowerUP) {
			Vector3 scale = colliderScale;
			scale.y *= 6;
			scale.z *= 3;
			GetComponent<BoxCollider> ().size = scale;
		} else {
			GetComponent<BoxCollider> ().size = colliderScale;
		}
	}

	public void Init(int _y, bool _isPowerUP)
	{
		CancelInvoke ();
		OnPowerUp (Data.Instance.isPowerUpOn);
		isPowerUP = _isPowerUP;
		source = GetComponent<AudioSource> ();

		foreach (EnergyAsset ea in level1) {
			ea.active.SetActive (false);
			ea.itemToBar.SetActive (false);
		}

		foreach (EnergyAsset ea in level2) {
			ea.active.SetActive (false);
			ea.itemToBar.SetActive (false);
		}

		foreach (EnergyAsset ea in level3) {
			ea.active.SetActive (false);
			ea.itemToBar.SetActive (false);
		}

		EnergyAsset[] actualLevel;
		switch (Data.Instance.playerData.level) {
		case 1:
			actualLevel = level1;
			break;
		case 2:
			actualLevel = level2;
			break;
		default:
			actualLevel = level3;
			break;
		}
		if(isPowerUP)
			activeAsset = GetPowerUp(actualLevel);
		else
			activeAsset = GetRayo(actualLevel);
		
		activeAsset.active.SetActive (true);

		catchedByPowerUp = false;
		catched = false;

		Vector3 pos = transform.localPosition;
		pos.y = _y;
		transform.localPosition = pos;
		transform.localScale = Vector3.one;
	}
	EnergyAsset GetRayo(EnergyAsset[] actualLevel)
	{
		foreach (EnergyAsset ea in actualLevel) {
			if (!ea.isPowerUp)
				return ea;
		}
		return null;
	}

	EnergyAsset GetPowerUp(EnergyAsset[] actualLevel)
	{
		EnergyAsset ea = actualLevel [UnityEngine.Random.Range (0, level1.Length - 1)];
		if (ea.isPowerUp)
			return ea;
		else  return GetPowerUp(actualLevel);
	}

	void OnTriggerEnter(Collider other)
	{
		character = other.gameObject.GetComponentInParent<Character> ();
		if (character != null) {
			source.pitch = 0.7937f * Mathf.Pow(1.122462f,(int)(UnityEngine.Random.value * 3));
			source.PlayOneShot (source.clip);

			activeAsset.active.SetActive (false);
			activeAsset.itemToBar.SetActive (true);

			if (isPowerUP)
				Events.OnPowerUp (true);

			if (!isPowerUP && character.inPowerUp) {
				catchedByPowerUp = true;
				Invoke ("ResetPowerUpCatched", 0.3f);
			} else {
				catched = true;
				Events.OnGetEnergy ();
				Invoke ("Done", 2f);
			}

		}
	}
	void ResetPowerUpCatched()
	{
		catchedByPowerUp = false;
		catched = true;
		Events.OnGetEnergy ();
		Invoke ("Done", 2f);
	}
	Vector3 dest;
	void Update()
	{	
		if (catchedByPowerUp) {
			dest = character.transform.position;
			dest.y -= 2;
			dest.z += 2;
			transform.position = Vector3.Lerp (transform.position, dest, Time.deltaTime*4f);
		}	
		else if (catched) {
			transform.SetParent (Game.Instance.gameManager.camera.transform);
			dest = transform.position;
			dest.z += 18;
			dest.y = 10;
			transform.position = Vector3.Lerp (transform.position, dest, Time.deltaTime*3);
		}
	}
	void Done()
	{
		isPowerUP = false;
		catchedByPowerUp = false;
		catched = false;
		Poolme ();
		CancelInvoke ();
	}
}
