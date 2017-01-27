using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

	public float damage = 100f;
	// Use this for initialization
	void Start () {
		
	}
	public float GetDamage(){
		return damage;
	}
	
	public void Hit(){
		// method of Hit
	}
	void OnBecameInvisible() {
		gameObject.SetActive(false);
        //  Destroy(gameObject);
     }
}
