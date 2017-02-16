using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBodyController : MonoBehaviour {
	public PlayerController player;
	
	
	// Use this for initialization
	void Start () {
		
	}

	public void init(PlayerController p){
		player = p;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/// <summary>
	/// Sent when another object enters a trigger collider attached to this
	/// object (2D physics only).
	/// </summary>
	/// <param name="other">The other Collider2D involved in this collision.</param>
	void OnTriggerEnter2D(Collider2D other)
	{
		 if (other.gameObject.tag == "Bullet")
        {
            GameObject gameobj = other.gameObject;
            BulletController bulletController = gameobj.GetComponent<BulletController>();
            float direction = gameobj.transform.localScale.x;

            player.shooter = bulletController.GetShooter();
            if (player.shooter.body.GetComponent<PlayerBodyController>().Equals(this))
            {
                return;
            }
            else
            {
                player.beater = bulletController.GetShooter();
            }
            // PlayShake();
            //bullethit
            bulletController.Hit();
            CombatTextManager.Instance.CreateText(player.transform.position, "HIT!", new Color(251 / 255.0f, 252 / 255.0f, 170 / 255.0f, 1), true);
            //
            player.knockbackPoint = bulletController.GetDamage() * 5.5f;
            //addforece
            // Vector2 force = (Vector2.right * knockbackPoint * direction);
            // print( (playerRb.velocity + Vector2.right * direction ).ToString() );
            Vector2 totalForce = player.playerRb.velocity + Vector2.right * direction;
            totalForce.Normalize();
            totalForce.x = totalForce.x * player.knockbackPoint;
            player.playerRb.AddForce( totalForce );
            //check who is shooter

            // print("hitted"+bullet.damage);
        }
		
	}
}
