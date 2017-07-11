using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneSfx : MonoBehaviour {

	public AudioClip[] clips;
	AudioSource source;

	// Use this for initialization
	void Start () {
		source = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Play(int i){
		source.PlayOneShot (clips [i]);
	}
}
