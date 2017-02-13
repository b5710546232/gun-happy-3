using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{

    public GameObject pool;
    public float projectileForce;

    public float shotDelay = 0.2f;
    private float lastBulletShotAt;

    public AudioClip shotsfx;
    public List<AudioClip> sfx_shoots;

    public int index_sfx;


    public float padding_x = 0.146f;
    public float padding_y = -0.113f;
    public float pos_z = 1;

    public GameObject effect_shot;

    public float offset_bullet_x;
    public float offset_bullet_y;

    public Animator anim;

    public float fx_padding_x = 0.203f;
    public float fx_padding_y = -0.001f;

    public bool isJump;

    public bool isWalk;

    public bool isInfiniteBullet;

    public int maxBullets;

    public int currentBullets;


    void Awake()
    {
        anim = GetComponent<Animator>();
        if(pool==null){
            pool = GameObject.Find("BulletPool");
        }
    }


    public void setJump(bool val)
    {
        isJump = val;
    }

    public void setWalk(bool val)
    {
        isWalk = val;
    }


    // Use this for initialization
    void Start()
    {
        lastBulletShotAt = 0;

        currentBullets = maxBullets;

        if (effect_shot == null)
        {
            effect_shot = Instantiate(effect_shot);
            effect_shot.SetActive(false);
        }
        else
        {
            effect_shot = Instantiate(effect_shot);
            effect_shot.SetActive(false);
        }
        effect_shot.transform.parent = transform;
        effect_shot.transform.localPosition = new Vector2(fx_padding_x, fx_padding_y);
    }

    public void Setup(PlayerController player)
    {
        transform.localPosition = new Vector3(padding_x, padding_y, pos_z);
        // transform.localScale = new Vector3(player.getDirection(), 1, 1);
    }

    private void PlaySoundSFX(){
        
        // // int index = (index_sfx++) % sfx_shoots.Count;
        // //fix to index 0;
        // int index = 0;
        // AudioSource.PlayClipAtPoint(sfx_shoots[index],transform.position);
    }

    public void Reload(){
         currentBullets = maxBullets;
    }

    public void fire(float direction, GameObject shooter)
    {




        if (Time.time - this.lastBulletShotAt < shotDelay) return;
        lastBulletShotAt = Time.time;


        //check type of gun;
        if (isInfiniteBullet)
        {
            //check number of bullet
            if (currentBullets <= 0) return;
            currentBullets--;
        }

        PlaySoundSFX();
        // AudioSource.PlayClipAtPoint (shotsfx , transform.position ,0.5f);

        // shoot fire the bullet

        GameObject gameobj = pool.GetComponent<BulletPoolController>().init(new Vector3(transform.position.x + offset_bullet_x * shooter.GetComponent<PlayerController>().getDirection(), transform.position.y + offset_bullet_y, transform.position.z));
        Rigidbody2D rbBullet = gameobj.GetComponent<Rigidbody2D>();
        rbBullet.velocity = new Vector2(projectileForce * direction, rbBullet.velocity.y);
        gameobj.transform.localScale = new Vector3(direction, 1, 1);

        BulletController bulletController = gameobj.GetComponent<BulletController>();
        bulletController.SetShooter(shooter);
        shooter.GetComponent<PlayerController>().TakeRecoil(20f);


        // effect shooting
        effect_shot.SetActive(true);
        effect_shot.GetComponent<Animator>().Play("effect_shot", -1, 0f);

        //reduce bullet;



    }

    public bool OutOfBullets(){
        if(isInfiniteBullet){
            return currentBullets <= 0;
        }
        return false;
    }

    void FixedUpdate()
    {
        anim.SetBool("isJump", isJump);
        anim.SetBool("isWalk", isWalk);
    }


}
