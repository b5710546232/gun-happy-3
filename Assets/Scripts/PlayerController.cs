using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public float speed = 3f;
	public bool grounded = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Control();
	}

	void Jump(){
		Rigidbody2D rb = GetComponent<Rigidbody2D> ();
		rb.velocity = new Vector2 (rb.velocity.x, 5f);
	}
	void Move(){
		gameObject.transform.Translate(Input.GetAxis("Horizontal")*Vector3.right*speed* Time.deltaTime);
	}

	void Control(){
		if( Input.GetAxis("Vertical")!=0  ){
			if(grounded){
				 Jump();
				 grounded = false;
			}
		}
		if( Input.GetAxis("Horizontal")!=0  ){
			Move();
		}
	}

	/// <summary>
	/// Sent when an incoming collider makes contact with this object's
	/// collider (2D physics only).
	/// </summary>
	/// <param name="other">The Collision2D data associated with this collision.</param>
	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Ground"){
			grounded = true;
			print("hi");
		}
		else{
			grounded = false;
			print("false");
		}
	}
}
