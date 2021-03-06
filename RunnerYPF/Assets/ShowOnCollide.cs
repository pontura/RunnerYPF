﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowOnCollide : MonoBehaviour {

	public GameObject[] assets;

	bool _hide;

	void OnEnable(){
		Invoke ("TestShow", 1.0f);
	}

	void TestShow(){
		if (!_hide) {
			foreach (GameObject go in assets)
				go.SetActive (true);
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "rail") {
			print ("destroy on collission");
			//Destroy (gameObject);
			//gameObject.SetActive(false);
			_hide = true;
		}
	}
}
