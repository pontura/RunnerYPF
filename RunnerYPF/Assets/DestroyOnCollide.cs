using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollide : MonoBehaviour {
	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "rail") {
			//print ("destroy on collission");
			Destroy (gameObject);
			//gameObject.SetActive(false);
		}
	}
}
