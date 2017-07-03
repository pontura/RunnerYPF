using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

    void Update()
    {
		if (Input.GetKeyDown(KeyCode.Space))
			Events.Jump();

		/*if (Input.GetButton("Fire1"))
			Events.Jump();*/
		
		if (Input.GetKeyDown(KeyCode.RightArrow))
			Events.SpeedChange(1);
		else if (Input.GetKeyDown(KeyCode.LeftArrow))
			Events.SpeedChange(-1);
		else if (Input.GetKeyUp(KeyCode.RightArrow))
			Events.SpeedChange(0);
		else if (Input.GetKeyUp(KeyCode.LeftArrow))
			Events.SpeedChange(0);
    }
}
