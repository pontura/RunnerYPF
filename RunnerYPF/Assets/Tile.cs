using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : SceneObject {

	public bool isPath;
	public TileAsset asset;
	public TileData tileData;
	public Animation anim;
	GroundTilesLine line;
	public Transform container;

	public void Init(GroundTilesLine line)
	{
		this.line = line;
		bool isHole = false;
		if (isPath) {
			
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
					SceneObject so = Data.Instance.pool.AddObjectTo ("Energy", transform);
					so.GetComponent<Energy>().Init(tileData.sceneObjectData.height);
				}
			}
		} else {
			if (Random.Range (0, 100) < 10 && this.transform.position.x<0 && !Game.Instance.gameManager.dontAddGenericObjects) {
				SceneObject so = Data.Instance.pool.AddObjectTo ("GenericObject", container);
			}
		}
		asset.Init (this, isHole);
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
