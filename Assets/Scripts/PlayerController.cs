using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 3f;
    public GameObject weapon;

    public float padding_x = 0.2f;
    public float padding_y = 0.2f;

    public float knockbackPoint = 0f;

    public bool grounded = false;

    public float weaponPositionZ = 0;

    public KeyCode upButton;

    public KeyCode leftButton;

    public KeyCode rightButton;

    public KeyCode downButton;

    public KeyCode fireButton;

    private PlayerController shooter;

    public float jumpForce = 3.5f;
    private Animator anim;

    Rigidbody2D playerRb;




    void Awake()
    {
        GetComponent<Rigidbody2D>().freezeRotation = true;
        playerRb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
    }
    // Use this for initialization
    void Start()
    {
        // weapon = Instantiate(weapon,transform.position,Quaternion.identity);

        weapon.transform.parent = this.transform;
        weapon.transform.localPosition = new Vector3(padding_x, padding_y, weaponPositionZ);
    }

    public void ChangeWeapon(GameObject newWeapon)
    {
        weapon.transform.parent = null;
        newWeapon.transform.parent = this.transform;

    }

    // Update is called once per frame
    void Update()
    {
        AnimationManage();
    }

    void Shoot(float direction)
    {
        weapon.GetComponent<GunController>().fire(direction, gameObject);

    }



    void Jump()
    {
        // playerRb.velocity = new Vector2(playerRb.velocity.normalized.x, jumpForce);
        // playerRb.velocity = new Vector2(0, jumpForce);
        playerRb.AddForce( new Vector2(playerRb.velocity.x, jumpForce*30));
        // Vector2 jump = Vector2.up * jumpForce*30;
        // playerRb.AddForce(jump);


    }
    void Move(float direction)
    {
        gameObject.transform.Translate(Vector3.right * direction * speed * Time.deltaTime);
        // playerRb.velocity = Vector2.right*direction*speed;
        // playerRb.velocity = new Vector2(direction*speed, playerRb.velocity.y);
        // Vector2 movement = transform.right*direction*speed*5;
        
        // playerRb.AddForce(movement);
        // float maxSpeed = 1.5f;
        // if(playerRb.velocity.x > maxSpeed){
        //     print("oh");
        //     playerRb.velocity = new Vector2(maxSpeed,playerRb.velocity.y);
        // }
        // if(playerRb.velocity.x < -maxSpeed){
        //     playerRb.velocity = new Vector2(-maxSpeed,playerRb.velocity.y);
        // }


        
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
        else if (Input.GetKey(rightButton))
        {
            transform.localScale = new Vector3(1, 1, 1);
            Move(1);
        }
   


        if (Input.GetKey(fireButton))
        {
            Shoot(transform.localScale.x);
        }
    }

    void AnimationManage(){
        anim.SetBool("isWalk",grounded && (playerRb.velocity != Vector2.zero || Input.GetKey(rightButton)|| Input.GetKey(leftButton)));
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
        playerRb.velocity = Vector2.zero;
        playerRb.angularVelocity = 0f;
        knockbackPoint = 0;

    }


    void FixedUpdate()
    {
        Control();
    }
}
