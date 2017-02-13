using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 3f;
    private GameObject currenWeapon;

    public float knockbackPoint = 0f;

    public bool grounded = false;
    bool isJump = false;

    public int live = 5;



    public KeyCode upButton;

    public KeyCode leftButton;

    public KeyCode rightButton;

    public KeyCode downButton;

    public KeyCode fireButton;

    public InputManager input;

    public PlayerController shooter;

    public PlayerController beater;

    public float jumpForce = 3.6f;
    private Animator anim;

    Rigidbody2D playerRb;

    public GameObject foot;

    private int jumpCounter = 2;

    public bool isDown;
    private float duration = 0.1f;
    public float magnitude = 0.1f;

    private GameObject canvas;

    public GameObject defaultWeapon;

    public string name = "player";

    public GameObject PlayerInfo;

    public int PID;

    private bool isDeath = false;

    private float direction;

    private float jumpDelay = .4f;
    private float lastJumpAt = 0f;

    private bool isDrop = false;

    public Color color;
    void Awake()
    {
        GetComponent<SpriteRenderer>().color = color;
        GetComponent<Rigidbody2D>().freezeRotation = true;
        playerRb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        canvas = gameObject.transform.GetChild(1).gameObject;
        canvas.transform.GetChild(0).gameObject.GetComponent<Text>().text = name;
        input = transform.GetChild(2).gameObject.GetComponent<InputManager>();
        
        
        if(currenWeapon==null){
            currenWeapon = defaultWeapon;
        }
        currenWeapon.transform.parent = this.transform;


    }
    // Use this for initialization
    void Start()
    {
        foot = gameObject.transform.GetChild(0).gameObject;
        // print(gameObject.transform);
        // currenWeapon = Instantiate(currenWeapon,transform.position,Quaternion.identity);
        direction = 1;
        if (currenWeapon != null)
        {
            anim.Play("player_anim_idle", 0, 0);
            currenWeapon.GetComponent<GunController>().GetComponent<Animator>().Play("gun_anim_idle", 0, 0);
        }
        currenWeapon.GetComponent<GunController>().Setup(this);

    }

    public void SetPlayerInfo(GameObject PlayerInfo)
    {
        this.PlayerInfo = PlayerInfo;


        GameObject PlayerInfoText = PlayerInfo.transform.GetChild(2).gameObject;

        GameObject PlayerName = PlayerInfoText.transform.GetChild(0).gameObject;
        PlayerName.GetComponent<Text>().text = name;

        GameObject LiveText = PlayerInfoText.transform.GetChild(1).gameObject;
        LiveText.GetComponent<Text>().text = "live : " + live;

        GameObject armmoText = PlayerInfoText.transform.GetChild(2).gameObject;
        armmoText.GetComponent<Text>().text = "armmo : " + 0;
    }

    public void UpdatePlayerInfo()
    {
        GameObject PlayerInfoText = PlayerInfo.transform.GetChild(2).gameObject;
        GameObject LiveText = PlayerInfoText.transform.GetChild(1).gameObject;
        LiveText.GetComponent<Text>().text = "live : " + live;

        GameObject armmoText = PlayerInfoText.transform.GetChild(2).gameObject;
        armmoText.GetComponent<Text>().text = "armmo : " + 0;
    }

    public void ChangeWeapon(GameObject newWeapon)
    {
        // currenWeapon.transform.parent = ;
        newWeapon.transform.localScale  = new Vector3(getDirection(), 1, 1);
        currenWeapon.SetActive(false);
        newWeapon.transform.parent = this.transform;
        currenWeapon.SetActive(false);
        newWeapon.gameObject.tag = "Untagged";
        currenWeapon = newWeapon;
        currenWeapon.GetComponent<GunController>().Setup(this);

    }

    private void ChangeToDefaultWeapon(){
        if(currenWeapon.GetComponent<GunController>().OutOfBullets() || isDeath){
            currenWeapon.SetActive(false);
            defaultWeapon.SetActive(true);
            currenWeapon = defaultWeapon;
            currenWeapon.GetComponent<GunController>().Setup(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        ChangeToDefaultWeapon();
    }

    public void Shoot(float direction)
    {
        currenWeapon.GetComponent<GunController>().fire(direction, gameObject);
        

    }

    public void TakeRecoil(float recoil)
    {

        playerRb.AddForce(Vector2.right * (-direction) * recoil);
        // print(Vector2.right * (-direction) * recoil);
    }



    public void Jump()
    {

        grounded = foot.GetComponent<PlayerFootController>().isGrounded() && playerRb.velocity.y <= 0;
        // logic
         if (!grounded && jumpCounter > 0)
            {
                if (!(Time.time - this.lastJumpAt < jumpDelay))
                {
                    lastJumpAt = Time.time;
                    // Jump();
                     // jump method
        Vector2 horizontal = new Vector2(playerRb.velocity.x, 0);
        Vector2 vertical = new Vector2(0, jumpForce * 50 + playerRb.velocity.y);
        Vector2 final = horizontal + vertical;
        final = new Vector2(final.normalized.x, final.normalized.y);
        // playerRb.velocity = final * jumpForce;
        playerRb.AddForce(final * jumpForce * 50);
        print(playerRb.velocity);
                    jumpCounter = 0;
                }

            }
            else if (jumpCounter > 0 || grounded)
            {
                if (!(Time.time - this.lastJumpAt < jumpDelay))
                {
                    lastJumpAt = Time.time;
                    // Jump();
                     // jump method
        Vector2 horizontal = new Vector2(playerRb.velocity.x, 0);
        Vector2 vertical = new Vector2(0, jumpForce * 50 + playerRb.velocity.y);
        Vector2 final = horizontal + vertical;
        final = new Vector2(final.normalized.x, final.normalized.y);
        // playerRb.velocity = final * jumpForce;
        playerRb.AddForce(final * jumpForce * 50);
        print(playerRb.velocity);
                    jumpCounter--;
                }
            }

        // // jump method
        // Vector2 horizontal = new Vector2(playerRb.velocity.x, 0);
        // Vector2 vertical = new Vector2(0, jumpForce * 50 + playerRb.velocity.y);
        // Vector2 final = horizontal + vertical;
        // final = new Vector2(final.normalized.x, final.normalized.y);
        // // playerRb.velocity = final * jumpForce;
        // playerRb.AddForce(final * jumpForce * 50);
        // print(playerRb.velocity);


    }
    public void Move(float direction)
    {
        transform.localScale = new Vector3(direction, 1, 1);
        canvas.transform.localScale = new Vector3(direction, 1, 1);
        this.direction = direction;
        // gameObject.transform.Translate(Vector2.right * direction * speed * Time.deltaTime);
        float maxSpeed = 1.0f;
        if (Mathf.Abs(playerRb.velocity.x) < maxSpeed)
        {
            playerRb.AddForce(Vector2.right * direction * speed * 10f);
            // print(playerRb.velocity.x);
        }

    }

    void Control()
    {


        grounded = foot.GetComponent<PlayerFootController>().isGrounded() && playerRb.velocity.y <= 0;
        if (grounded)
        {
            jumpCounter = 2;
        }

        if (Input.GetKey(upButton) || Input.GetKey(input.getUpButton()))
        {
            Jump();
            // if (!grounded && jumpCounter > 0)
            // {
            //     if (!(Time.time - this.lastJumpAt < jumpDelay))
            //     {
            //         lastJumpAt = Time.time;
            //         Jump();
            //         jumpCounter = 0;
            //     }

            // }
            // else if (jumpCounter > 0 || grounded)
            // {
            //     if (!(Time.time - this.lastJumpAt < jumpDelay))
            //     {
            //         lastJumpAt = Time.time;
            //         Jump();
            //         jumpCounter--;
            //     }
            // }


        }
        if (Input.GetKey(leftButton) || Input.GetKey(input.getLeftButton()))
        {
            Move(-1);
        }
        if (Input.GetKey(rightButton) || Input.GetKey(input.getRightButton()))
        {
            Move(1);
        }




        if (Input.GetKey(fireButton) || Input.GetKey(input.getFireButton()))
        {
            Shoot(transform.localScale.x);
        }

        
        bool drop = Input.GetKey(downButton) || Input.GetKey(input.getDownButton());    
        if (drop)
        {
            Drop();
        }
    }

    void AnimationManage()
    {
        bool check = Input.GetKey(rightButton) || Input.GetKey(input.getRightButton());
        check = check || Input.GetKey(leftButton) || Input.GetKey(input.getLeftButton());

        bool isWalk = grounded && (Mathf.Abs(playerRb.velocity.x) != 0f) || check;
        bool isJump = !grounded && (Mathf.Abs(playerRb.velocity.y) > 0.0f);

        currenWeapon.GetComponent<GunController>().setWalk(isWalk);
        currenWeapon.GetComponent<GunController>().setJump(isJump);
        anim.SetBool("isWalk", isWalk);
        anim.SetBool("isJump", isJump);



    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Weapon"){

            ChangeWeapon(other.gameObject);
        }
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
            else{
                beater = bulletController.GetShooter();
            }
            // PlayShake();
            //bullethit
            bulletController.Hit();
            CombatTextManager.Instance.CreateText(transform.position, "HIT!", new Color(251/255.0f,252/255.0f,170/255.0f,1), true);
            //
            knockbackPoint = bulletController.GetDamage() * 5.5f;
            //addforece
            playerRb.AddForce(Vector2.right * knockbackPoint * direction);
            //check who is shooter

            // print("hitted"+bullet.damage);
        }
    }

    void PlayShake()
    {
        StopAllCoroutines();
        StartCoroutine("Shake");
    }
    IEnumerator Shake()
    {

        float elapsed = 0f;

        Vector3 originalCamPos = Camera.main.transform.position;

        while (elapsed < duration)
        {

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
            // isDeath = true;
            // // Reset();
            return;
            //add score to shooter who shot this player.
        }
    }

    private void Reset()
    {
        StartCoroutine(Shake());
        transform.position = new Vector2(0,2f);
        playerRb.velocity = Vector2.zero;
        playerRb.angularVelocity = 0f;
        knockbackPoint = 0;
        print("reset");
        if (isDeath)
        {
            ChangeToDefaultWeapon();
            live--;
            CombatTextManager.Instance.CreateText(beater.transform.position, "GG!", new Color(161/255.0f,239/255.0f,121/255.0f,1), true,40);
        }
        isDeath = false;

    }

    private IEnumerator DropProcess(){
              
        float process = 0.0f;
        float limit = .1f;
          while(process<limit){
            foot.GetComponent<Collider2D>().isTrigger = true;       
            isDrop = true;
            process += Time.deltaTime;
            yield return new WaitForFixedUpdate();
            }         
            isDrop = false;   
        
    }

    public void Drop()
    {
    
                if( foot.GetComponent<PlayerFootController>().drop){
                    StartCoroutine(DropProcess());
                }  
    }

    void PassPlatform(){
            
         
        
        if ( playerRb.velocity.y > 0.0f)
        {
                foot.GetComponent<Collider2D>().isTrigger = true;       
                
        }
        else
        {
            if(!isDrop){
                foot.GetComponent<Collider2D>().isTrigger = false;
            }
            
        }
    }


    void FixedUpdate()
    {
        PassPlatform();
        Control();

        // StartCoroutine(Drop());
        AnimationManage();
        UpdatePlayerInfo();

        if (Mathf.Abs(transform.position.y) > 10.0f || Mathf.Abs(transform.position.x) > 10.0f)
        {
            isDeath = true;
            Reset();
        }
    }

    public float getDirection(){
        return direction;
    }

}
