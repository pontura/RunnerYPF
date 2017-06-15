using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneObject : MonoBehaviour {

	public void Poolme()
	{
		Data.Instance.pool.PoolObject (this);
	}
}
