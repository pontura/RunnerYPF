using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pool : MonoBehaviour
{
	#region member
	[Serializable]
	public class ObjectPoolEntry
	{
		[SerializeField]
		public SceneObject Prefab;

		[SerializeField]
		public int Count;
	}
	#endregion
	public ObjectPoolEntry[] Entries;
	public Transform pooled;

	public List<SceneObject> pooledObjects;
	public List<SceneObject> ObjectsOnScene;

	private int totalEntries;

	void Awake()
	{
		Events.PoolAllObjects += PoolAllObjects;
		DontDestroyOnLoad(this);

		for (int i = 0; i < Entries.Length; i++)
		{
			totalEntries += Entries[i].Count;
		}

		int id = 0;
		for (int i = 0; i < Entries.Length; i++)
		{
			var objectPrefab = Entries[i];
			//create the repository
			//fill it

			for (int n = 0; n < objectPrefab.Count; n++)
			{
				SceneObject newObj = Instantiate(objectPrefab.Prefab) as SceneObject;
				newObj.name = objectPrefab.Prefab.name;
				newObj.transform.SetParent (pooled);
				newObj.gameObject.SetActive (false);
				pooledObjects.Add (newObj);
				//SceneObject ro = newObj.GetComponent<SceneObject>();
				//PoolObject(ro);
				id++;

			}
		}
	}

	public SceneObject AddObjectTo(string objectType, Transform container)
	{

		SceneObject pooledObjectToAdd = null;
		foreach (SceneObject pooledObject in pooledObjects) {
			if (pooledObject.name == objectType)
				pooledObjectToAdd = pooledObject;
		}
		if (pooledObjectToAdd != null)
		{
			
			pooledObjectToAdd.transform.SetParent (container);
			pooledObjectToAdd.transform.localPosition = Vector3.zero;
			pooledObjectToAdd.transform.localScale = Vector3.one;
			pooledObjectToAdd.gameObject.SetActive (true);

			ObjectsOnScene.Add (pooledObjectToAdd);
			pooledObjects.Remove (pooledObjectToAdd);

			return pooledObjectToAdd;
		}
		else
		{
			Debug.Log("ObjectPool no encontro el objeto: " + objectType + "  bool ");
		}
		return null;
	}



	void PoolAllObjects()
	{
		for (int a=0; a<ObjectsOnScene.Count; a++) {
			SceneObject so = ObjectsOnScene[a];
			PoolthisObject (so);
		}
		ObjectsOnScene.Clear ();
	}
	public void PoolObject(SceneObject obj)
	{
		SceneObject objectToPool = null;
		foreach (SceneObject ooscene in ObjectsOnScene) {
			if (ooscene == obj)
				objectToPool = ooscene;
		}
		if (objectToPool != null) {
			PoolthisObject (objectToPool);
			ObjectsOnScene.Remove (objectToPool);
		}
	}
	void PoolthisObject(SceneObject so)
	{
		so.gameObject.SetActive (false);
		so.transform.SetParent (pooled);
		so.transform.position = new Vector3 (0, 0, 0);
		pooledObjects.Add (so);
	}

}