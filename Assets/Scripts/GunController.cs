using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

	public GameObject pool;
	public float projectileForce;

	// Use this for initialization
	void Start () {
	}

	public void fire(float direction, GameObject shooter){
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
