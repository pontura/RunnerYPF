using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneEvents : MonoBehaviour {

	public void OnCutsceneComplete(){
		Events.OnCutsceneComplete ();
	}

	public void OnCutsceneLastScene(){
		Events.OnCutsceneFinal ();
	}
}
