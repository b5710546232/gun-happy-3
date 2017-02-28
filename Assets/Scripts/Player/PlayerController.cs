using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = 1f;

    private float acceleration=.03f;
    private float maxSpeed= 1.5f;
    private GameObject currenWeapon;

    public List<AudioClip> sfx_hurts;

    public int index_hurt;

    public float knockbackPoint = 0f;

    public bool grounded = false;
    bool isJump = false;

    public int live = 5;



    public KeyCode upButton;

    public GameObject body;

    public KeyCode leftButton;

    public KeyCode rightButton;

    public KeyCode downButton;

    public KeyCode fireButton;

    public InputManager input;

    public PlayerController shooter;

    public PlayerController beater;

    public float jumpForce = 3.6f;
    private Animator anim;

    public Rigidbody2D playerRb;

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

    private float noMovementThreshold = 0.0001f;
    private const int noMovementFrames = 3;
    Vector3[] previousLocations = new Vector3[noMovementFrames];
    private bool isMoving;
    public bool IsMoving
 {
     get{ return isMoving; }
 }
 

    public Color color;
    void Awake()
    {

          for(int i = 0; i < previousLocations.Length; i++)
     {
         previousLocations[i] = Vector3.zero;
     }


        GetComponent<SpriteRenderer>().color = color;
        GetComponent<Rigidbody2D>().freezeRotation = true;
        playerRb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        canvas = gameObject.transform.GetChild(1).gameObject;
        canvas.transform.GetChild(0).gameObject.GetComponent<Text>().text = name;
        input = transform.GetChild(2).gameObject.GetComponent<InputManager>();


        if (currenWeapon == null)
        {
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

        body.GetComponent<PlayerBodyController>().init(this);
        

    }

    public void SetPlayerInfo(GameObject PlayerInfo)
    {
        this.PlayerInfo = PlayerInfo;

        GameObject PlayerIcon = PlayerInfo.transform.GetChild(0).gameObject;
        PlayerIcon.GetComponent<SpriteRenderer>().color = color;

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
        newWeapon.transform.localScale = new Vector3(getDirection(), 1, 1);
        currenWeapon.SetActive(false);
        newWeapon.transform.parent = this.transform;
        currenWeapon.SetActive(false);
        newWeapon.gameObject.tag = "Untagged";
        currenWeapon = newWeapon;
        currenWeapon.GetComponent<GunController>().Setup(this);

    }

    private void ChangeToDefaultWeapon()
    {
        if (currenWeapon.GetComponent<GunController>().OutOfBullets() || isDeath)
        {
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


            Vector2 totalForce = playerRb.velocity + Vector2.right * -direction;
            totalForce.Normalize();
            totalForce.x = totalForce.x * recoil;
            playerRb.AddForce( totalForce );

        // playerRb.AddForce(Vector2.right * (-direction) * recoil);
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
                // playerRb.AddForce(Vector2.up * jumpForce * 50);

                playerRb.velocity += (final * jumpForce);
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
                // playerRb.AddForce(final * jumpForce * 50);
                playerRb.velocity += (final * jumpForce);

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
     
        // gameObject.transform.Translate(Vector2.right * direction*speed  * Time.deltaTime);
        // if(Mathf.Abs(playerRb.velocity.x )< maxSpeed )
            // playerRb.velocity += Vector2.right * direction*speed;


        // speed += acceleration/10f;
        //  if (speed > maxSpeed)
            // speed = maxSpeed;

        print(speed+"spepd");
        transform.localScale = new Vector3(direction, 1, 1);
        canvas.transform.localScale = new Vector3(direction, 1, 1);
        this.direction = direction;


        
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
        bool drop = ControlFreak2.CF2Input.GetKey(downButton) || ControlFreak2.CF2Input.GetKey(input.getDownButton());
        if (grounded)
        {
            jumpCounter = 2;
        }

        if (ControlFreak2.CF2Input.GetKey(upButton) || ControlFreak2.CF2Input.GetKey(input.getUpButton()))
        {
            if(!drop)
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
        if (ControlFreak2.CF2Input.GetKey(leftButton) || ControlFreak2.CF2Input.GetKey(input.getLeftButton()))
        {
            Move(-1);
        }
        if (ControlFreak2.CF2Input.GetKey(rightButton) || ControlFreak2.CF2Input.GetKey(input.getRightButton()))
        {
            Move(1);
        }




        if (ControlFreak2.CF2Input.GetKey(fireButton) || ControlFreak2.CF2Input.GetKey(input.getFireButton()))
        {
            Shoot(transform.localScale.x);
        }


    
        if (drop)
        {
                Drop();
        }
    }

    void AnimationManage()
    {
        bool check = ControlFreak2.CF2Input.GetKey(rightButton) || ControlFreak2.CF2Input.GetKey(input.getRightButton());
        check = check || ControlFreak2.CF2Input.GetKey(leftButton) || ControlFreak2.CF2Input.GetKey(input.getLeftButton());

        bool isWalk = grounded && (Mathf.Abs(playerRb.velocity.x) != 0f) || check;
        bool isJump = !grounded && (Mathf.Abs(playerRb.velocity.y) > 0.0f);

        currenWeapon.GetComponent<GunController>().setWalk(isWalk);
        currenWeapon.GetComponent<GunController>().setJump(isJump);
        anim.SetBool("isWalk", isWalk || isMoving);
        anim.SetBool("isJump", isJump);



    }





    private void BulletHitHandler(Collider2D other)
    {
        // use in PlayerBodyController
        // return;
        // if (other.gameObject.tag == "Bullet")
        // {
        //     GameObject gameobj = other.gameObject;
        //     BulletController bulletController = gameobj.GetComponent<BulletController>();
        //     float direction = gameobj.transform.localScale.x;

        //     shooter = bulletController.GetShooter();
        //     if (shooter.Equals(this))
        //     {
        //         return;
        //     }
        //     else
        //     {
        //         beater = bulletController.GetShooter();
        //     }
        //     // PlayShake();
        //     //bullethit
        //     bulletController.Hit();
        //     CombatTextManager.Instance.CreateText(transform.position, "HIT!", new Color(251 / 255.0f, 252 / 255.0f, 170 / 255.0f, 1), true);
        //     //
        //     knockbackPoint = bulletController.GetDamage() * 5.5f;
        //     //addforece
        //     // Vector2 force = (Vector2.right * knockbackPoint * direction);
        //     // print( (playerRb.velocity + Vector2.right * direction ).ToString() );
        //     Vector2 totalForce = playerRb.velocity + Vector2.right * direction;
        //     totalForce.Normalize();
        //     totalForce.x = totalForce.x * knockbackPoint;
        //     playerRb.AddForce( totalForce );
            //check who is shooter

            // print("hitted"+bullet.damage);
        // }
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

  
    private void Reset()
    {
        PlayShake();
        PlaySoundHurt();
        transform.position = new Vector2(0, 2f);
        playerRb.velocity = Vector2.zero;
        playerRb.angularVelocity = 0f;
        knockbackPoint = 0;
        // print("reset");
        if (isDeath)
        {
            ChangeToDefaultWeapon();
            live--;
            if(beater!=null){
                CombatTextManager.Instance.CreateText(beater.transform.position, "GG!", new Color(161 / 255.0f, 239 / 255.0f, 121 / 255.0f, 1), true, 40);
            }
        }
        isDeath = false;

        if(live<=0){
            GameManager.Instance.GameOver();
        }

    }

    private IEnumerator DropProcess()
    {

        float process = 0.0f;
        float limit = .1f;
        while (process < limit)
        {
            foot.GetComponent<Collider2D>().isTrigger = true;
            isDrop = true;
            process += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        isDrop = false;

    }

    public void Drop()
    {

        if (foot.GetComponent<PlayerFootController>().drop)
        {
            // Jump();
            jumpCounter = 0;
            StartCoroutine(DropProcess());
        }
    }

    void PassPlatform()
    {



        if (playerRb.velocity.y > 0.0f)
        {
            foot.GetComponent<Collider2D>().isTrigger = true;

        }
        else
        {
            if (!isDrop)
            {
                foot.GetComponent<Collider2D>().isTrigger = false;
            }

        }
    }


    void FixedUpdate()
    {
        // ===

         for(int i = 0; i < previousLocations.Length - 1; i++)
     {
         previousLocations[i] = previousLocations[i+1];
     }
     previousLocations[previousLocations.Length - 1] = gameObject.transform.position;
 
  
     for(int i = 0; i < previousLocations.Length - 1; i++)
     {
         if(Vector3.Distance(previousLocations[i], previousLocations[i + 1]) >= noMovementThreshold)
         {
             //The minimum movement has been detected between frames
             isMoving = true;
             break;
         }
         else
         {
             isMoving = false;
         }
     }
    //  =======
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

    public float getDirection()
    {
        return direction;
    }

    public void SetDirction(float val){
        direction = val;
    }

    private void PlaySoundHurt(){
           int index = (index_hurt++) % sfx_hurts.Count;
        AudioSource.PlayClipAtPoint(sfx_hurts[index],transform.position);
    }

}
