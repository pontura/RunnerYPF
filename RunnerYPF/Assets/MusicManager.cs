using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

	public AudioClip ingame, cutsceneWin, cutsceneLose,cutsceneComplete,splash;

	AudioSource music;

	// Use this for initialization
	void Start () {
		music = GetComponent<AudioSource> ();

		Events.OnFinal += OnFinal;
		Events.RestartAllOver += RestartAllOver;
		Events.StartGame += StartGame;
		Events.OnCutsceneFinal += OnCutsceneFinal;
		Events.OnCutsceneComplete += OnCutsceneComplete;
		Events.GameOver += GameOver;
	}

	void OnDestroy () {
		Events.RestartAllOver -= RestartAllOver;
		Events.OnFinal -= OnFinal;
		Events.OnCutsceneComplete -= OnCutsceneComplete;
		Events.OnCutsceneFinal -= OnCutsceneFinal;
		Events.GameOver -= GameOver;
		Events.StartGame -= StartGame;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnFinal(){
		music.clip = cutsceneWin;
		music.Play ();
	}

	void RestartAllOver(bool newGame){
		print ("aca");
		if (newGame) {
			music.clip = splash;
			music.Play ();
		}else
			StartGame ();
	}

	void StartGame(){
		music.clip = ingame;
		music.Play ();
	}

	void OnCutsceneFinal(){
		music.Stop ();
		//PlaySoundWithCallback(cutsceneComplete,RestartAllOver);
		music.PlayOneShot(cutsceneComplete);
	}

	void OnCutsceneComplete(bool newGame){
		//if(newGame)music.Stop ();
	}

	void GameOver(){
		music.clip = cutsceneLose;
		music.Play ();
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
