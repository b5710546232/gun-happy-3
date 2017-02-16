using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

	public float damage = 100f;
	private GameObject shooter;
	// Use this for initialization


	void Start () {
		
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

	public void Hit(){
		// method of Hit
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
