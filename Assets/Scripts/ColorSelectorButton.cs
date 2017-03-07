using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorSelectorButton : MonoBehaviour {
	public GameObject pane;
	public GameObject targetPlayer;
	public GameObject colorPicker;
	public List<GameObject> colors;
	// Use this for initialization
	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake()
	{
		colorPicker = GameObject.FindGameObjectWithTag("ColorPicker");

        colors = new List<GameObject>();

		for (int i = 0 ;i< colorPicker.transform.childCount ;i++) {
			colors.Add(colorPicker.transform.GetChild(i).gameObject);
		}
	}
	void Start () {
		pane = GameObject.FindGameObjectWithTag("Panel");		
	}

	public void ChooseColor(){
		targetPlayer.GetComponent<SpriteRenderer>().color = gameObject.GetComponent<Image>().color;
		
		pane.SetActive(false);
		

		for (int i = 0 ;i< colorPicker.transform.childCount ;i++) {

			colors[i].gameObject.SetActive(false);
		}
	}
	

}
