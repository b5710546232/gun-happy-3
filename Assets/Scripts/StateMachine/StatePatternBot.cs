﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePatternBot : MonoBehaviour {

	public float searchingTurnSpeed = 120f;
	public float searchingDuration = 4f;
	public float sightRage = 20f;
	public Transform[] wayPoints;
	public GameObject eye;
	public GameObject eye2;
	public GameObject eyeBullet;
	public GameObject eyeEnemy;
	public Vector3 offset = new Vector3 (0,0.5f,0);
	public float saveRage = 1.3f;

	public List<PlayerController>enemies;

	[HideInInspector] public Transform chaseTarget;
	[HideInInspector] public IBotState currentState;
	[HideInInspector] public ChaseState chaseState;
	[HideInInspector] public CombatState combatState;
	[HideInInspector] public PatrolState patroState;
	[HideInInspector] public IdleState idleState;
	

	[HideInInspector] public PlayerController controller;

	private PlayerController target;

	public float attackRage = 1f;


	private void Awake(){

		controller = GetComponent<PlayerController>();
		chaseState = new ChaseState(this);
		combatState = new CombatState(this);
		patroState = new PatrolState(this);
		idleState = new IdleState(this);
		
		var p = GameObject.FindGameObjectsWithTag("Player");
        enemies = new List<PlayerController>();
        for (int i = 0; i < p.Length; i++)
        {	
			PlayerController player = p[i].GetComponent<PlayerController>();
			if(player.PID != controller.PID)
            enemies.Add(player);
        }
		// print(enemies);
		// test enemy
		target = enemies[0];


	}
	
	// Use this for initialization
	void Start () {
		currentState = patroState;

		// eye = Instantiate(eye,transform);
		// eye.transform.localPosition = Vector2.zero;
		
	}


	
	// Update is called once per frame
	void Update () {
		currentState.UpdateState();
	}


	void OnTriggerEnter2D(Collider2D other)
	{
		// currentState.OnTriggerEnter2D(other);
	}

	public PlayerController getTarget(){
		return target;
	}
}
