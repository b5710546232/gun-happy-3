using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public float speed = 3f;
	public GameObject projectile;
	public float padding = 1f;
	public float projecttileSpeed = 10f;

	public float health = 100f;

	public bool grounded = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Shoot(){
		GameObject bullet = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
		Rigidbody2D rbBullet = bullet.GetComponent<Rigidbody2D> ();
		float bulletSpeed = 10f;
		rbBullet.velocity = new Vector2(bulletSpeed, rbBullet.velocity.y);
		// rbBullet.velocity = new Vector2(rbBullet.velocity.x, 5f);
		// use gun to fire
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

		if(Input.GetKeyDown(KeyCode.Space)){
			Shoot();
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

	void FixedUpdate()
	{
		Control();
	}
}
