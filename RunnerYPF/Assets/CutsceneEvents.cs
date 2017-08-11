using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CutsceneEvents : MonoBehaviour {

	public bool newGameOnEnd;
	AudioSource source;
	VideoPlayer vplayer;
	
	void Start(){
		
	}

	void OnEnable(){
		vplayer = GetComponent<VideoPlayer> ();
		vplayer.loopPointReached += EndReached;
		source = GetComponent<AudioSource> ();
		vplayer.Play ();
		//source.Play ();
	}

	public void OnCutsceneComplete(int newGame){
		Events.OnCutsceneComplete (newGame==1);
	}

	public void OnCutsceneLastScene(){
		Events.OnCutsceneFinal ();
	}

	void EndReached(UnityEngine.Video.VideoPlayer vp)
	{
		Events.OnCutsceneFinal ();
		Invoke ("CutsceneClose", 3f);
	}

	void CutsceneClose(){
		Events.OnCutsceneComplete (newGameOnEnd);
	}
}
