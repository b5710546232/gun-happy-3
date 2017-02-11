using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePatternEnemy : MonoBehaviour {

	public float searchingTurnSpeed = 120f;
	public float searchingDuration = 4f;
	public float sightRage = 20f;
	public Transform[] wayPoints;
	public Transform eyes;
	public Vector3 offset = new Vector3 (0,0.5f,0);

	[HideInInspector] public Transform chaseTarget;
	[HideInInspector] public IEnemyState currentState;
	[HideInInspector] public ChaseState chaseState;
	[HideInInspector] public AlertState alertState;
	[HideInInspector] public PatrolState patroState;


	private void Awake(){
		chaseState = new ChaseState(this);
		alertState = new AlertState(this);
		patroState = new PatrolState(this);


	}
	
	// Use this for initialization
	void Start () {
		currentState = patroState;
		
	}
	
	// Update is called once per frame
	void Update () {
		currentState.UpdateState();
	}


	void OnTriggerEnter2D(Collider2D other)
	{
		currentState.OnTriggerEnter2D(other);
	}
}
