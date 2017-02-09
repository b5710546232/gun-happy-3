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
    bool isJump = false;

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

    private int jumpCounter = 2;

    public bool isDown;
    public    float duration = 0.5f;
    public float magnitude = 0.1f;

    private GameObject canvas;

    public string name = "player";

    public GameObject PlayerInfo;

    public int ID;

    private float direction;

    private float jumpDelay = .4f;
    private float lastJumpAt = 0f;
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
        direction = 1;

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
        TakeRecoil(5f);



    }

    void TakeRecoil(float recoil){

        playerRb.AddForce(Vector2.right*( -direction )*recoil);
        print(Vector2.right*( -direction )*recoil);
        print("x"+playerRb.velocity.x);
    }



    void Jump()
    {
  
        Vector2 horizontal = new Vector2(playerRb.velocity.x, 0);
        Vector2 vertical = new Vector2(0, jumpForce*50 + playerRb.velocity.y );
        Vector2 final = horizontal + vertical;
        final = new Vector2 (final.normalized.x,final.normalized.y);
        // playerRb.velocity = final * jumpForce;
        playerRb.AddForce(final * jumpForce*50);
        print(playerRb.velocity);


    }
    void Move(float direction)
    {
        this.direction = direction;
        float F = speed*4f;
        float v = (F/playerRb.mass)*Time.fixedDeltaTime;
        // gameObject.transform.Translate(Vector2.right * direction * speed * Time.deltaTime);
        Vector2 horizontal = new Vector2(10, 0);
        Vector2 vertical = new Vector2(0, jumpForce*50 + playerRb.velocity.y );
        Vector2 final = horizontal + vertical;
        float maxSpeed = 4f;
        	if(direction * GetComponent<Rigidbody2D>().velocity.x < maxSpeed)
			// ... add a force to the player.
			GetComponent<Rigidbody2D>().AddForce(Vector2.right * direction * speed*4f);
		// If the player's horizontal velocity is greater than the maxSpeed...
		// if(Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > maxSpeed)
			// ... set the player's velocity to the maxSpeed in the x axis.
			// GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(GetComponent<Rigidbody2D>().velocity.x) * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

        print(playerRb.velocity.x);

    }

    void Control()
    {
        print(jumpCounter);

        grounded = foot.GetComponent<PlayerFootController>().isGrounded() && playerRb.velocity.y<=0;
        if(grounded){
            jumpCounter = 2;
        }
        
        if (Input.GetKey(upButton) || Input.GetKey(input.getUpButton() ))
        {
            if(!grounded && jumpCounter>0){
                if (!(Time.time - this.lastJumpAt < jumpDelay)){
                        lastJumpAt = Time.time;
                        Jump();
                        isJump = true;
                        jumpCounter = 0;
                }

            }
            else if (jumpCounter>0 || grounded){
                if (!(Time.time - this.lastJumpAt < jumpDelay)){
                    lastJumpAt = Time.time;
                    Jump();
                    isJump = true;
                    jumpCounter--;
                }
            }
            

        }
        if (Input.GetKey(leftButton) || Input.GetKey(input.getLeftButton())  )
        {
            transform.localScale = new Vector3(-1, 1, 1);
            canvas.transform.localScale = new Vector3(-1, 1, 1);
            Move(-1);
        }
        if (Input.GetKey(rightButton) || Input.GetKey(input.getRightButton()))
        {
            transform.localScale = new Vector3(1, 1, 1);
            canvas.transform.localScale = new Vector3(1, 1, 1);
            Move(1);
        }




        if (Input.GetKey(fireButton) || Input.GetKey(input.getFireButton()))
        {
            Shoot(transform.localScale.x);
        }
    }

    void AnimationManage()
    {
        bool check = Input.GetKey(rightButton) || Input.GetKey(input.getRightButton());
        check = check || Input.GetKey(leftButton) || Input.GetKey(input.getLeftButton());
        anim.SetBool("isWalk", grounded && (Mathf.Abs(playerRb.velocity.x) != 0f)|| check);
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

            foot.GetComponent<Collider2D>().isTrigger = true;

        }
        else{
            foot.GetComponent<Collider2D>().isTrigger = false;
        }
    
      
    }


    void FixedUpdate()
    {
        Control();
        Drop(); 
        AnimationManage();
    }

}
