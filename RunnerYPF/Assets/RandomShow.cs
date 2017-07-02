using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomShow : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnEnable(){
		int r = (int)(Random.value * transform.childCount);
		for (int i = 0; i < transform.childCount; i++)
			transform.GetChild (i).gameObject.SetActive (i == r);		
	}
}
