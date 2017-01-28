using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

	public GameObject pool;

	// Use this for initialization
	void Start () {
	}

	public void fire(float direction){
		// shoot fire the bullet
		// GameObject gameobj = Instantiate(bullet, transform.position, Quaternion.identity) as GameObject;
		GameObject gameobj = pool.GetComponent<BulletPoolController>().init(transform.position);
        Rigidbody2D rbBullet = gameobj.GetComponent<Rigidbody2D>();
		float bulletSpeed = 3f;
        rbBullet.velocity = new Vector2(bulletSpeed*direction, rbBullet.velocity.y);
		gameobj.transform.localScale = new Vector3(direction, 1, 1);

	}
	
}
