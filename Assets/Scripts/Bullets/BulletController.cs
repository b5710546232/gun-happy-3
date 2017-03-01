using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

	private float damage = 0;
	private GameObject shooter;
	// Use this for initialization
	private float hitTimeStart = 0;

	void Start () {
		
	}

	public void SetDamage(float val){
		damage = val;
	}

	public float GetDamage(){
		return damage;
	}
	
	public void SetShooter(GameObject shooter){
		this.shooter = shooter;
	}

	public PlayerController GetShooter(){
		return shooter.GetComponent<PlayerController>();
	}

	public void resetTo(Vector3 pos){
		transform.position = pos;
		gameObject.SetActive(true);
		hitTimeStart = Time.time;
	}

	public void Hit(){
		// method of Hit
		float hitTime = Time.time - hitTimeStart;
		Debug.Log("Time"+hitTime);

		if (hitTime <0.2){
			hitTime = 0.85f;
		}
		else if( hitTime <= 0.6f  && hitTime>=0.2f){
			hitTime = 1;
		}


		else if( hitTime > .6f ){
			hitTime = 1.2f;
		}

		float newDmg = GetDamage() * 1/hitTime;

		SetDamage(newDmg);

		print(newDmg+hitTime);

		gameObject.SetActive(false);
		Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
		rb.velocity = Vector2.zero;
	}
	void OnBecameInvisible() {
		gameObject.SetActive(false);
        //  Destroy(gameObject);
     }

	 /// <summary>
	 /// Sent when another object enters a trigger collider attached to this
	 /// object (2D physics only).
	 /// </summary>
	 /// <param name="other">The other Collider2D involved in this collision.</param>
	 void OnTriggerEnter2D(Collider2D other)
	 {
		//  if (other.gameObject.tag == "Player"){
		// 	 PlayerController player = other.gameObject.GetComponent<PlayerController>();

		// 	 if (player.shooter.Equals(GetShooter()))
        //     {
        //         return;
        //     }
        //     else
        //     {
        //         player.beater = GetShooter();
        //     }

		// 	Hit();

		// 	CombatTextManager.Instance.CreateText(player.transform.position, "HIT!", new Color(251 / 255.0f, 252 / 255.0f, 170 / 255.0f, 1), true);
		// 	player.knockbackPoint = GetDamage() * 5.5f;
		// 	Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
		// 	 Vector2 totalForce = playerRb.velocity + Vector2.right * player.getDirection();
        //     totalForce.Normalize();
        //     totalForce.x = totalForce.x * player.knockbackPoint;
        //     playerRb.AddForce( totalForce );

		//  }
	 }


}
