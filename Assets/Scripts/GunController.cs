using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

	public GameObject pool;
	public float projectileForce;

	public float shotDelay = 0.2f;
	private float lastBulletShotAt;

	
	// Use this for initialization
	void Start () {
		lastBulletShotAt = 0;
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


	}
	
}
