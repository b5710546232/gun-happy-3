using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBodyController : MonoBehaviour
{
    public PlayerController player;


    // Use this for initialization
    void Start()
    {

    }

    public void init(PlayerController p)
    {
        player = p;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            GameObject gameobj = other.gameObject;
            BulletController bulletController = gameobj.GetComponent<BulletController>();

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
            float knockbackPoint = bulletController.GetDamage();

            float dir = knockbackPoint/Mathf.Abs(knockbackPoint);

            float val =knockbackPoint;
            
            Vector2 force = new Vector2(player.playerRb.velocity.x+val,player.playerRb.velocity.y);
            
            // force.Set(player.playerRb.velocity.x+dir,0);
            float x = Mathf.Abs(player.playerRb.velocity.x+val);
            float y = Mathf.Abs(player.playerRb.velocity.y);
            float m = Mathf.Sqrt(( (x*x)+(y*y) ));

            force = force.normalized * knockbackPoint * dir * Time.deltaTime;
            // force = Vector2.right * Mathf.Abs( (2*x-m)/m) * knockbackPoint * dir * Time.deltaTime;
            
            //addforece
            
            // force = ( Vector2.right*m * knockbackPoint *dir) *Time.deltaTime;
            // print( (playerRb.velocity + Vector2.right * direction ).ToString() );

            // rigidbody.position + (delta * speed)
            // player.playerRb.AddForce(force,ForceMode2D.Impulse);
            // Vector2 temp = new Vector2 ( force.x, y).normalized;
            // force = temp.x*force.x * Vector2.right;

            Vector2 final = new Vector2(force.x - (dir*player.playerRb.velocity.normalized.y), 0);
            player.playerRb.AddForce(final*.7f,ForceMode2D.Impulse);
            // print("player.playerRb.velocity.y"+player.playerRb.velocity.y);
            // player.playerRb.velocity += final;
            print("final_mag"+final.magnitude);
            print("magforce"+force.magnitude);
            print("magp"+player.playerRb.velocity.magnitude);
            print("vel"+player.playerRb.velocity);
            // player.playerRb.AddForce(Vector2.right*knockbackPoint*0.8f);
            // player.playerRb.velocity += Vector2.right*knockbackPoint/50f;
            // player.playerRb.velocity += force
            //check who is shooter

            // print("hitted"+bullet.damage);
            // Debug.LogError( "Velocity : " + player.playerRb.velocity.ToString());
        }


        if (other.gameObject.tag == "Weapon")
        {

            player.ChangeWeapon(other.gameObject);
        }

    }
}
