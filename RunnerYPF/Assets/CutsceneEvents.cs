using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneEvents : MonoBehaviour {

	public void OnCutsceneComplete(int newGame){
		Events.OnCutsceneComplete (newGame==1);
	}

	public void OnCutsceneLastScene(){
		Events.OnCutsceneFinal ();
	}
}
