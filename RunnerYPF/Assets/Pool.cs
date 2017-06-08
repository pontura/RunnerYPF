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

	public List<SceneObject>[] pooledObjects;

	private int totalEntries;

	void Start()
	{
		DontDestroyOnLoad(this);

		for (int i = 0; i < Entries.Length; i++)
		{
			totalEntries += Entries[i].Count;
		}

		pooledObjects = new List<SceneObject>[totalEntries];

		int id = 0;
		for (int i = 0; i < Entries.Length; i++)
		{
			var objectPrefab = Entries[i];
			//create the repository

			pooledObjects[i] = new List<SceneObject>();
			//fill it

			for (int n = 0; n < objectPrefab.Count; n++)
			{
				SceneObject newObj = Instantiate(objectPrefab.Prefab) as SceneObject;
				newObj.name = objectPrefab.Prefab.name;
				newObj.transform.SetParent (pooled);
				SceneObject ro = newObj.GetComponent<SceneObject>();
				PoolObject(ro);
				id++;

			}
		}
	}

	public SceneObject AddObjectTo(string objectType, Transform container)
	{
		for (int i = 0; i < Entries.Length; i++)
		{
			var prefab = Entries[i].Prefab;
			if (prefab.name != objectType)
				continue;

			if (pooledObjects[i].Count > 0)
			{
				SceneObject pooledObject = pooledObjects[i][0];
				pooledObjects[i].RemoveAt(0);
				if (pooledObject)
				{
					pooledObject.transform.SetParent (container);
					pooledObject.transform.SetParent (container);
					pooledObject.transform.localPosition = Vector3.zero;
					pooledObject.transform.localScale = Vector3.one;
					pooledObject.gameObject.SetActive (true);
					return pooledObject;
				}
				else
				{
					Debug.Log("ObjectPool no encontro el objeto: " + objectType + "  bool ");
					AddObjectTo(objectType, container);
				}
			}
		}
		return null;
	}




	public void PoolObject(SceneObject obj)
	{
		for (int i = 0; i < Entries.Length; i++)
		{
			if (Entries[i].Prefab.name == obj.name || Entries[i].Prefab.name + "(Clone)" == obj.name)
			{
				obj.gameObject.SetActive(false);
				obj.transform.SetParent (pooled);
				pooledObjects[i].Add(obj);
				return;
			}
		}
	}


}