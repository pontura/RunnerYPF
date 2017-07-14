using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSfx : MonoBehaviour {

	AudioSource source;

	// Use this for initialization
	void Start () {
		source = GetComponent<AudioSource> ();
	}

	void OnEnable(){
		source.Play ();
	}

}
