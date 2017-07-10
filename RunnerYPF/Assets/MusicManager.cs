using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

	public AudioClip ingame, cutscene,cutsceneComplete;

	AudioSource music;

	// Use this for initialization
	void Start () {
		music = GetComponent<AudioSource> ();

		Events.OnFinal += OnFinal;
		Events.RestartAllOver += RestartAllOver;
		Events.OnCutsceneFinal += OnCutsceneFinal;
		//Events.OnCutsceneComplete += OnCutsceneComplete;
	}

	void OnDestroy () {
		Events.RestartAllOver -= RestartAllOver;
		Events.OnFinal -= OnFinal;
		//Events.OnCutsceneComplete -= OnCutsceneComplete;
		Events.OnCutsceneFinal -= OnCutsceneFinal;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnFinal(){
		music.clip = cutscene;
		music.Play ();
	}

	void RestartAllOver(){
		music.clip = ingame;
		music.Play ();
	}

	void OnCutsceneFinal(){
		music.Stop ();
		//PlaySoundWithCallback(cutsceneComplete,RestartAllOver);
		music.PlayOneShot(cutsceneComplete);
	}

	public delegate void AudioCallback();
	public void PlaySoundWithCallback(AudioClip clip, AudioCallback callback)
	{
		music.PlayOneShot(clip);
		StartCoroutine(DelayedCallback(clip.length, callback));
	}
	private IEnumerator DelayedCallback(float time, AudioCallback callback)
	{
		yield return new WaitForSeconds(time);
		callback();
	}
}
