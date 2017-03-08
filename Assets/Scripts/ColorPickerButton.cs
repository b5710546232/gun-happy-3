using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorPickerButton : MonoBehaviour {

	public GameObject colorPicker;
	
	public GameObject player;
	public GameObject panel;
	public List<GameObject> colors;
	// Use this for initialization
	public bool toggle;
	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake()
	{
			toggle = false;

		// set to not active
	
		gameObject.GetComponent<Image>().color = player.GetComponent<SpriteRenderer>().color;
		// colorPicker = GameObject.FindGameObjectWithTag("ColorPicker");

        colors = new List<GameObject>();

		for (int i = 0 ;i< colorPicker.transform.childCount ;i++) {
			colors.Add(colorPicker.transform.GetChild(i).gameObject);
		}
     
	 	colorPicker.SetActive(false);
		panel.SetActive(false);
	}
	void Start () {
	

	}


	public void ShowColorPicker(){

		colorPicker.SetActive(true);
		panel.SetActive(true);

		for (int i = 0 ;i< colorPicker.transform.childCount ;i++){
			colors[i].GetComponent<ColorSelectorButton>().targetPlayer = player;
			colors[i].GetComponent<ColorSelectorButton>().colorPickerButton = gameObject;

		}
		
		for (int i = 0 ;i< colorPicker.transform.childCount ;i++) {

			colors[i].gameObject.SetActive(true);
		}
	}
}
