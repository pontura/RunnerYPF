using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneObject : MonoBehaviour {

	void Start () {
		//Events.PoolAllObjects += PoolAllObjects;
	}
	void OnDestroy() {
	//	Events.PoolAllObjects -= PoolAllObjects;
	}
	void PoolAllObjects()
	{
		//Poolme ();
	}
	public void Poolme()
	{
		Data.Instance.pool.PoolObject (this);
	}
}
