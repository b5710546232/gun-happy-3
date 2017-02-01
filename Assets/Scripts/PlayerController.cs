using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 3f;
    public GameObject weapon;

    public float knockbackPoint = 0f;

    public bool grounded = false;

    public float weaponPositionZ = 0;

    public KeyCode upButton;

    public KeyCode leftButton;

    public KeyCode rightButton;

    public KeyCode downButton;

    public KeyCode fireButton;

    private PlayerController shooter;

    public float jumpForce = 3.6f;
    private Animator anim;

    Rigidbody2D playerRb;

    public GameObject foot;

    public bool isDown;

    void Awake()
    {
        GetComponent<Rigidbody2D>().freezeRotation = true;
        playerRb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        weapon.transform.parent = this.transform;



    }
    // Use this for initialization
    void Start()
    {
        foot = gameObject.transform.GetChild(0).gameObject;
        print(gameObject.transform);
        // weapon = Instantiate(weapon,transform.position,Quaternion.identity);
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
        // playerRb.velocity = new Vector2(playerRb.velocity.x, jumpForce);
        // playerRb.velocity = new Vector2(0, jumpForce);
        // playerRb.AddForce( new Vector2(playerRb.velocity.x, jumpForce*30));
        // gameObject.transform.Translate(Vector3.up* jumpForce * Time.deltaTime);
        // Vector2 jump = Vector2.up * jumpForce * 30;
        // playerRb.AddForce(jump);
        // gameObject.transform.Translate(Vector3.up * jumpForce*30 * Time.deltaTime);
        float sp = playerRb.velocity.y;
        // sp += jumpForce;
        // if(sp>jumpForce){
        //     // sp = jumpForce;
        // }
        sp = jumpForce;

        playerRb.velocity = new Vector2(playerRb.velocity.x,sp);

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
        grounded = foot.GetComponent<PlayerFootController>().isGrounded();
        if (Input.GetKeyDown(upButton))
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

    void AnimationManage()
    {
        anim.SetBool("isWalk", grounded && (playerRb.velocity != Vector2.zero || Input.GetKey(rightButton) || Input.GetKey(leftButton)));
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        BulletHitHandler(other);
        DeadZoneHitHandler(other);

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
    
    void Drop(){
         bool drop = Input.GetKey(downButton);
        if(drop ||  playerRb.velocity.y>0.0f){
        //Vector2 jump = Vector2.up *jumpForce*10;
        //playerRb.AddForce(jump);
        // Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Foot"),
        //                   LayerMask.NameToLayer("AirFloor"),
        //                    drop
        //                   );
        foot.GetComponent<Collider2D>().isTrigger = true;
        // foot.GetComponent<Collider2D>().enabled = false;
        // foot.GetComponent<Collider2D>().enabled = true;
        }
        else{
            foot.GetComponent<Collider2D>().isTrigger = false;
            // foot.GetComponent<Collider2D>().enabled = true;
            //   Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Foot"),
            //               LayerMask.NameToLayer("AirFloor"),
            //                playerRb.velocity.y>0.0f
            //               );
        }
    
      
    }


    void FixedUpdate()
    {
        Drop(); 
        Control();


    }

}
