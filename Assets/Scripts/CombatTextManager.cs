using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatTextManager : MonoBehaviour {

	private static CombatTextManager instance;
	public float speed;
	public GameObject textPrefab;
	public RectTransform canvasTransform;
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
		sct.GetComponent<RectTransform>().localScale = new Vector3(0.02f,0.02f,1);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
