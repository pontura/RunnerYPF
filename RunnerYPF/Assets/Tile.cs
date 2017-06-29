using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : SceneObject {

	public bool isRiver;
	public bool isPath;

	public MeshRenderer top;
	public MeshRenderer bottom;

	public TileAsset asset;
	public TileData tileData;
	public Animation anim;
	GroundTilesLine line;
	public Transform container;

	public void Init(GroundTilesLine line, Settings.LevelSettings levelSettings)
	{		
		if (isPath) {
			top.material.color = levelSettings.tile;
			bottom.material.color = levelSettings.tile;
			if (tileData.obstaclesInLane == TileData.ObstaclesInLane.CAR) {
				SceneObject so = Data.Instance.pool.AddObjectTo ("CarForward", container);
				so.GetComponent<MoveSceneObject> ().Init ();
			}
		} else {
			Colorize (levelSettings);
		}
		this.line = line;
		bool isHole = false;

		if (tileData.isRiver) {
			isRiver = true;
		}
		else if (isPath) {
			
			if (tileData.final == true) {
				SceneObject so = Data.Instance.pool.AddObjectTo ("Final", transform);
				Events.OnFinal ();
			}
			int height= tileData.height;
			if (height == 0) {
				isHole = true;
			}
			else {
				asset.gameObject.SetActive (true);
				asset.transform.localPosition = new Vector3 (0, height-1, 0);
				if (tileData.sceneObjectData != null) {
					SceneObject so = Data.Instance.pool.AddObjectTo ("Energy", container);
					so.GetComponent<Energy>().Init(tileData.sceneObjectData.height);
				}
			}
		} else {


			/////////////level 1
			if (!Game.Instance.gameManager.dontAddGenericObjects) {
				if (transform.position.x == 2)
					AddSceneObjectsEvery (3); // faroles
				else if (transform.position.x == -3)
					AddSceneObjectsEvery (14); //estacion
				else if (transform.position.x == -4)
					AddSceneObjectsEvery (9); //panel o molino
				else
					AddSceneObjectsInRandom (40, 3); //autos
			}
			/////////////level 1


		}
		asset.Init (this, isHole, isRiver);
	}
	void AddSceneObjectsEvery(int num)
	{
		if (num == 1) {			
			AddObject ();
		} else
		if (transform.position.z % num == 0) {			
			AddObject ();
		}
	}
	void AddEveryTile()
	{
		AddObject ();
	}
	void AddSceneObjectsInRandom(int posibilityIn100, int tilesSeparation)
	{
		if (Random.Range (0, 100) < posibilityIn100 && (int)transform.position.z % tilesSeparation == 0)
			AddObject ();
	}
	void AddObject()
	{
		SceneObject so = null;
		int tileID = (int)transform.position.x;
		so = Data.Instance.pool.AddObjectTo ("GenericObject", container);
		so.GetComponent<GenericObject> ().Init (1, tileID);
	}
	void Colorize(Settings.LevelSettings levelSettings)
	{
		bool tile1 = false;
		if (Mathf.Floor (transform.position.x) % 2 == 0 && Mathf.Floor (transform.position.z) % 2 == 0)
			tile1 = true;
		else if (Mathf.Floor (transform.position.x) % 2 !=0 && Mathf.Floor (transform.position.z) % 2 != 0)
			tile1 = true;

		if(tile1)
		{
			top.material.color = levelSettings.topGeneric [0];
			bottom.material.color = levelSettings.bottomGeneric [1];
		} else {
			top.material.color = levelSettings.topGeneric[1];
			bottom.material.color = levelSettings.bottomGeneric[0];
		}
	}
	public void AnimateIn()
	{
		anim.Play ("in");
	}
	public void AnimateOut()
	{
		Invoke ("Reset", 0.2f);
		anim.Play ("out");
	}
	public void Reset()
	{
		if(asset.GetComponent<Collider>() != null)
			asset.GetComponent<Collider>().enabled = false;
		
		Poolme ();

		if (container != null && container.childCount > 0) {
			SceneObject so = container.GetComponentInChildren<SceneObject> ();
			so.Poolme();
		}
	}


}
