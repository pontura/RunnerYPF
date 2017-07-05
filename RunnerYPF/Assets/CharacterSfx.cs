using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSfx : MonoBehaviour {

	public AudioClip[] footsteps;
	public AudioClip jump;
	public AudioClip jump2;
	public AudioClip stop;
	public AudioClip dead;
	public AudioClip win;
	public AudioClip restart;
	public AudioClip fall;

	AudioSource source;

	public Character.states state;

	// Use this for initialization
	void Start () {
		source = GetComponent<AudioSource> ();
		Events.PoolAllObjects += Fall;
	}

	void OnDestroy(){
		Events.PoolAllObjects -= Fall;
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void Fall (){
		if (Game.Instance.gameManager.state == GameManager.states.DEAD) {
			source.loop = false;
			source.clip = fall;
			source.pitch = 1;
			source.Play ();
		}
	}

	public void Restart (){
		if(source==null)
			source = GetComponent<AudioSource> ();
		source.loop = false;
		source.clip = restart;
		source.pitch = 1;
		PlaySoundWithCallback(restart,() => Run(0));
	}

	public void Run(int mult){
		if (state != Character.states.RUNNING)
			return;
		source.clip = footsteps[0];
		source.pitch = 1f + (mult -2) * -0.25f;
		source.loop = true;
		source.Play();
	}

	public void Jump(bool first){
		source.loop = false;
		source.clip = jump;
		if (first)
			source.pitch = 1;
		else
			source.pitch = 1.122462f;
		//source.Play ();
		//AudioClip c = first?jump:jump2;
		PlaySoundWithCallback(jump,() => Run(0));
	}

	public void Stop(){
		source.loop = false;
		source.clip = stop;
		source.pitch = 1;
		source.Play ();
	}

	public void Die(){
		source.loop = false;
		source.clip = dead;
		source.pitch = 1;
		source.Play ();
	}

	public void Win(){
		source.loop = false;
		source.clip = win;
		source.pitch = 1;
		source.Play ();
	}

	public delegate void AudioCallback();
	public void PlaySoundWithCallback(AudioClip clip, AudioCallback callback)
	{
		source.PlayOneShot(clip);
		StartCoroutine(DelayedCallback(clip.length, callback));
	}
	private IEnumerator DelayedCallback(float time, AudioCallback callback)
	{
		yield return new WaitForSeconds(time);
		callback();
	}
}

