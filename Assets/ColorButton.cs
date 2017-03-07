using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorButton : MonoBehaviour {
	public GameObject pane;
	public GameObject targetPlayer;
	// Use this for initialization
	void Start () {
		pane = GameObject.FindGameObjectWithTag("Panel");


		
	}

	public void ChooseColor(){
		targetPlayer.GetComponent<SpriteRenderer>().color = gameObject.GetComponent<Image>().color;
		pane.SetActive(false);
		gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
