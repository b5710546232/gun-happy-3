using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 3f;
    public GameObject weapon;

    public float padding = 0.2f;

    public float knockbackPoint = 0f;

    public bool grounded = false;

    public float weaponPositionZ = -5;

    public KeyCode upButton;

    public KeyCode leftButton;

    public KeyCode rightButton;

    public KeyCode downButton;

    public KeyCode fireButton;

    private PlayerController shooter;



    void Awake()
    {
        GetComponent<Rigidbody2D>().freezeRotation = true;
    }
    // Use this for initialization
    void Start()
    {
        // weapon = Instantiate(weapon,transform.position,Quaternion.identity);

        weapon.transform.parent = this.transform;
        weapon.transform.localPosition = new Vector3(padding, 0, weaponPositionZ);
    }

    public void ChangeWeapon(GameObject newWeapon)
    {
        weapon.transform.parent = null;
        newWeapon.transform.parent = this.transform;

    }

    // Update is called once per frame
    void Update()
    {

    }

    void Shoot(float direction)
    {
        weapon.GetComponent<GunController>().fire(direction, gameObject);

    }



    void Jump()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(rb.velocity.normalized.x, 5f);

    }
    void Move(float direction)
    {
        gameObject.transform.Translate(Vector3.right * direction * speed * Time.deltaTime);
    }

    void Control()
    {
        if (Input.GetKey(upButton))
        {
            if (grounded)
            {
                Jump();
                grounded = false;
            }
        }
        if (Input.GetKey(leftButton))
        {
            transform.localScale = new Vector3(-1, 1, 1);
            Move(-1);
        }
        if (Input.GetKey(rightButton))
        {
            transform.localScale = new Vector3(1, 1, 1);
            Move(1);
        }


        if (Input.GetKey(fireButton))
        {
            Shoot(transform.localScale.x);
        }
    }


    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {

            grounded = true;
        }

    }


    void OnTriggerEnter2D(Collider2D other)
    {
        BulletHitHandler(other);
        DeadZoneHitHandler(other);
        
    }

    /// <summary>
    /// Sent when a collider on another object stops touching this
    /// object's collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionExit2D(Collision2D other)
    {
        
           if (other.gameObject.tag == "Ground")
        {

            grounded = false;
        }
    }


    private void BulletHitHandler(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            GameObject gameobj = other.gameObject;
            BulletController bulletController = gameobj.GetComponent<BulletController>();
            Rigidbody2D playerRb = gameObject.GetComponent<Rigidbody2D>();
            float direction = gameobj.transform.localScale.x;

            shooter = bulletController.GetShooter();
            if (shooter.Equals(this))
            {
                return;
            }

            //bullethit
            bulletController.Hit();

            //
            knockbackPoint += bulletController.GetDamage();
            //addforece
            playerRb.AddForce(Vector2.right * knockbackPoint * direction);
            //check who is shooter

            // print("hitted"+bullet.damage);
        }
    }

    private void DeadZoneHitHandler(Collider2D other)
    {
        if (other.gameObject.tag == "DeadZone")
        {
            // go to spawn @ spawn point.
            Reset();

            //add score to shooter who shot this player.
        }
    }

    private void Reset()
    {
        transform.position = Vector3.zero;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().angularVelocity = 0f;
        knockbackPoint = 0;

    }


    void FixedUpdate()
    {
        Control();
    }
}
