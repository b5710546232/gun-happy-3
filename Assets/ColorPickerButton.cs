using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPickerButton : MonoBehaviour {

	public GameObject colorPicker;
	public GameObject panel;
	// Use this for initialization
	public bool toggle;
	void Start () {
		toggle = false;

		// set to not active
		colorPicker.SetActive(false);
		panel.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ShowColorPicker(){
		toggle = !toggle;
		colorPicker.SetActive(toggle);
		panel.SetActive(toggle);
	}
}
