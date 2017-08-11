using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CutsceneEvents : MonoBehaviour {

	public bool newGameOnEnd;
	public bool loseCutscene;
	AudioSource source;
	VideoPlayer vplayer;
	
	void Start(){
		
	}
	void OnEnable(){
		vplayer = GetComponent<VideoPlayer> ();
		source = GetComponent<AudioSource> ();
		if (vplayer != null) {
			vplayer.loopPointReached += EndReached;
			vplayer.Play ();
		}
		if (loseCutscene)
			Invoke ("OnCutsceneDead", 6);
		//source.Play ();
	}

	public void OnCutsceneComplete(int newGame){
		//print ("__________OnCutsceneComplete " + newGame);
		//Events.OnCutsceneComplete (newGame==1);
	}
	public void OnCutsceneDead(){
		print ("________________OnCutsceneDead");
		Events.OnCutsceneComplete (true);
	}

	public void OnCutsceneLastScene(){
		Events.OnCutsceneFinal ();
	}

	void EndReached(UnityEngine.Video.VideoPlayer vp)
	{
		vplayer.loopPointReached -= EndReached;
		vplayer.Stop ();
		print ("____EndReached");
		Events.OnCutsceneFinal ();
		Invoke ("CutsceneClose", 3f);
	}

	void CutsceneClose(){
		Events.OnCutsceneComplete (newGameOnEnd);
	}
}
