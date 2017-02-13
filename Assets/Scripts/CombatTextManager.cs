using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CombatTextManager : MonoBehaviour {

	private static CombatTextManager instance;
	public float speed;
	public GameObject textPrefab;
	public RectTransform canvasTransform;
	public Vector3 direction;
	public float fadeTime;
	// Use this for initialization

	public static CombatTextManager Instance{
		get{
			if(instance == null){
				instance = GameObject.FindObjectOfType<CombatTextManager>();

			}

			return instance;
		}
	}
	void Start () {
		
	}


	public void CreateText(Vector3 position, string text, Color color,bool critical){
		GameObject sct = Instantiate(textPrefab,position,Quaternion.identity);
		sct.transform.SetParent(canvasTransform);
		sct.GetComponent<RectTransform>().localScale = new Vector3(0.015f,0.015f,1);
		sct.GetComponent<CombatText>().Init(speed,direction,fadeTime,critical);
		sct.GetComponent<Text>().text = text;
		sct.GetComponent<Text>().color = color;
	}

	public void CreateText(Vector3 position, string text, Color color,bool critical,int size){
		GameObject sct = Instantiate(textPrefab,position,Quaternion.identity);
		sct.transform.SetParent(canvasTransform);
		sct.GetComponent<RectTransform>().localScale = new Vector3(0.015f,0.015f,1);
		sct.GetComponent<CombatText>().Init(speed,direction,fadeTime,critical);
		sct.GetComponent<Text>().text = text;
		sct.GetComponent<Text>().fontSize = size;
		sct.GetComponent<Text>().color = color;
	}
	
	
	// Update is called once per frame
	void Update () {
		
	}
}
