using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 3f;
    public GameObject weapon;

    public float knockbackPoint = 0f;

    public bool grounded = false;

    public int live = 5;

    public float weaponPositionZ = 0;

    public KeyCode upButton;

    public KeyCode leftButton;

    public KeyCode rightButton;

    public KeyCode downButton;

    public KeyCode fireButton;

    public InputManager input;

    private PlayerController shooter;

    public float jumpForce = 3.6f;
    private Animator anim;

    Rigidbody2D playerRb;

    public GameObject foot;

    public bool isDown;
    public    float duration = 0.5f;
    public float magnitude = 0.1f;

    private GameObject canvas;

    public string name = "player";

    public GameObject PlayerInfo;

    public int ID;
    void Awake()
    {
        GetComponent<Rigidbody2D>().freezeRotation = true;
        playerRb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        weapon.transform.parent = this.transform;
        canvas = gameObject.transform.GetChild(1).gameObject;
        canvas.transform.GetChild(0).gameObject.GetComponent<Text>().text = name;
        input = transform.GetChild(2).gameObject.GetComponent<InputManager>();


    }
    // Use this for initialization
    void Start()
    {
        foot = gameObject.transform.GetChild(0).gameObject;
        print(gameObject.transform);
        // weapon = Instantiate(weapon,transform.position,Quaternion.identity);

        PlayerInfoInit();

    }

    void PlayerInfoInit(){
        PlayerInfo = Instantiate(PlayerInfo,transform.position,Quaternion.identity).gameObject;
        if(ID==1)
            PlayerInfo.transform.position = new Vector2(1.58f,0);
        if(ID==2)
            PlayerInfo.transform.position = new Vector2(3.24f,0);

        GameObject PlayerInfoText = PlayerInfo.transform.GetChild(2).gameObject;

        GameObject PlayerName = PlayerInfoText.transform.GetChild(0).gameObject;
        PlayerName.GetComponent<Text>().text = name;

        GameObject LiveText = PlayerInfoText.transform.GetChild(1).gameObject;
        LiveText.GetComponent<Text>().text = "live : "+live;

        GameObject armmoText = PlayerInfoText.transform.GetChild(2).gameObject;
        armmoText.GetComponent<Text>().text = "armmo : "+0;
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
        // playerRb.velocity = new Vector2(playerRb.velocity.x, jumpForce);
        // playerRb.velocity = new Vector2(0, jumpForce);
        // playerRb.AddForce( new Vector2(playerRb.velocity.x, jumpForce*30));
        // gameObject.transform.Translate(Vector3.up* jumpForce * Time.deltaTime);
        // Vector2 jump = Vector2.up * jumpForce * 30;
        // playerRb.AddForce(jump);
        // gameObject.transform.Translate(Vector3.up * jumpForce*30 * Time.deltaTime);
        float sp = playerRb.velocity.y;
        sp += jumpForce;
        // if(sp>jumpForce){
        //     // sp = jumpForce;
        // }
    
        // float diriection = 1f;
        // if(playerRb.velocity.x<0)
            // diriection = -1;

        // float sp_x =  playerRb.velocity.normalized.x * sp * diriection;
        

        Vector2 horizontal = new Vector2(playerRb.velocity.x, 0);
        Vector2 vertical = new Vector2(0, jumpForce);
        Vector2 final = horizontal + vertical;
        final = new Vector2 (final.normalized.x,1);

        // Vector2 moveDir = Vector2( Vector3.up).normalized;
        Vector3 moveDir = Vector3.Cross(playerRb.velocity, Vector3.up).normalized;
        playerRb.velocity = final * jumpForce;
        print(moveDir);

    }
    void Move(float direction)
    {

        Vector2 horizontal = Vector2.right;
        Vector2 vertical = new Vector2(0,playerRb.velocity.y);

        Vector2 finalMovement = vertical.normalized + horizontal;


        gameObject.transform.Translate(Vector2.right * direction * speed * Time.deltaTime);
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
        if (Input.GetKey(upButton) || Input.GetKey(input.getUpButton() ))
        {
            if (grounded)
            {
                Jump();
                return;
            }

        }
        if (Input.GetKey(leftButton) || Input.GetKey(input.getLeftButton())  )
        {
            transform.localScale = new Vector3(-1, 1, 1);
            canvas.transform.localScale = new Vector3(-1, 1, 1);
            Move(-1);
            AnimationManage();
        }
        if (Input.GetKey(rightButton) || Input.GetKey(input.getRightButton()))
        {
            transform.localScale = new Vector3(1, 1, 1);
            canvas.transform.localScale = new Vector3(1, 1, 1);
            print(canvas);
            Move(1);
            AnimationManage();
        }




        if (Input.GetKey(fireButton) || Input.GetKey(input.getFireButton()))
        {
            Shoot(transform.localScale.x);
        }
    }

    void AnimationManage()
    {
        anim.SetBool("isWalk", grounded && (playerRb.velocity != Vector2.zero ));
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
            // PlayShake();
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

    void PlayShake(){
        StopAllCoroutines();
        StartCoroutine("Shake");
    }
    IEnumerator Shake() {
        
    print("shake");
    float elapsed = 0f;
    
    Vector3 originalCamPos = Camera.main.transform.position;

    while (elapsed < duration) {
        
        elapsed += Time.deltaTime;          
        
        float percentComplete = elapsed / duration;         
        float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);
        
        // map value to [-1, 1]
        float x = Random.value * 2.0f - 1.0f;
        float y = Random.value * 2.0f - 1.0f;
        x *= magnitude * damper;
        y *= magnitude * damper;
        
        Camera.main.transform.position = new Vector3(x, y, originalCamPos.z);
            
        yield return null;
    }
    
    Camera.main.transform.position = originalCamPos;
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
         bool drop = Input.GetKey(downButton) || Input.GetKey(input.getDownButton());
         drop = drop && foot.GetComponent<PlayerFootController>().drop;
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
