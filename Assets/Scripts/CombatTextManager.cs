using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatTextManager : MonoBehaviour {

	private CombatTextManager instance;
	public float speed;
	public GameObject textPrefab;
	public RectTransform canvasTransform;
	public float fadeTime;
	// Use this for initialization
	void Start () {
		
	}

	public CombatTextManager GetInstance(){
		if(instance==null){
			return new CombatTextManager();
		}
		return instance;
	}

	public void CreateText(Vector3 position, string text, Color color,bool critical){

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
