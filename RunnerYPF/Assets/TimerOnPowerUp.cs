using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerOnPowerUp : MonoBehaviour {

	Image image;
	Color defaultColor;

	// Use this for initialization
	void Start () {
		image = GetComponent<Image> ();
		defaultColor = new Color (image.color.r, image.color.g, image.color.b);
		Events.OnPowerUp += OnPowerUp;
	}
	void OnDestroy()
	{
		Events.OnPowerUp -= OnPowerUp;
	}
		
	// Update is called once per frame
	void OnPowerUp (bool isOn) {
		if (isOn) {
			image.color = Color.white;
			Invoke ("SetDefaultColor", 0.2f);
		}
	}

	void SetDefaultColor(){
		image.color = new Color (defaultColor.r, defaultColor.g, defaultColor.b);
	}
}
