using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAsset : MonoBehaviour {

	public Rigidbody rigidBody;
	private Character character;

	public void Init(Character _character)
	{
		this.character = _character;
	}
	void OnCollisionEnter(Collision  other)
	{
		if (character == null)
			return;
		
		if (other.gameObject.tag == "Tile" && character.state != Character.states.STARTJUMPING) {
			if(!other.gameObject.GetComponent<TileAsset>().isHole)
				character.OnFloor ();
		}
	}
	public void Jump()
	{
		//collider.enabled = false;
	}
}
