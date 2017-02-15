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


}
