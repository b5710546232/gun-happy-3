﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

	public GameObject pool;
	public float projectileForce;

	public float shotDelay = 0.2f;
	private float lastBulletShotAt;


    public float padding_x = 0.12f;
    public float padding_y = -0.1f;
	public float pos_z = 1;

	public GameObject effect_shot;


	
	// Use this for initialization
	void Start () {
		lastBulletShotAt = 0;
		transform.localPosition = new Vector3(padding_x, padding_y, pos_z);

		if(effect_shot==null){
			effect_shot = Instantiate(effect_shot);
			effect_shot.SetActive(false);
		}else{
			effect_shot = Instantiate(effect_shot);
			effect_shot.SetActive(false);
		}
		effect_shot.transform.parent = transform;
		effect_shot.transform.localPosition = new Vector2(0.203f,-0.012f);
	}


	public void fire(float direction, GameObject shooter){

 		if (Time.time - this.lastBulletShotAt < shotDelay) return;
    	lastBulletShotAt = Time.time;

		// shoot fire the bullet
		// GameObject gameobj = Instantiate(bullet, transform.position, Quaternion.identity) as GameObject;
		GameObject gameobj = pool.GetComponent<BulletPoolController>().init(transform.position);
        Rigidbody2D rbBullet = gameobj.GetComponent<Rigidbody2D>();
		// float shootForce =100f;
		// rbBullet.AddForce(gameobj.transform.forward * shootForce);
		// rbBullet.AddForce(Vector2.right *direction* shootForce);
		
        rbBullet.velocity = new Vector2(projectileForce*direction, rbBullet.velocity.y);
		gameobj.transform.localScale = new Vector3(direction, 1, 1);
		
		BulletController bulletController = gameobj.GetComponent<BulletController>();
		bulletController.SetShooter(shooter);
		bulletController.transform.localPosition = new Vector2(-1.023f,-0.615f);

		// effect_shot.transform.localPosition = new Vector3 (transform.position.x + 0.2f,transform.position.y);
		effect_shot.SetActive(true);
		effect_shot.GetComponent<Animator>().Play("effect_shot", -1, 0f);


	}
	
}
