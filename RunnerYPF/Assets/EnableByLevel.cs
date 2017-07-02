using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnableByLevel : MonoBehaviour {

	public List<ElementByLevel> elements;

	[Serializable]
	public class ElementByLevel
	{
		public int levelId;
		public GameObject element;
	}

	// Use this for initialization
	void Start () {
		Events.OnLevelComplete += OnLevelComplete;
		SetElements ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnDestroy(){
		Events.OnLevelComplete -= OnLevelComplete;
	}

	void SetElements(){
		foreach (ElementByLevel e in elements)
			e.element.SetActive (Data.Instance.playerData.level == e.levelId);
	}

	void OnLevelComplete(){
		SetElements ();
	}
}
