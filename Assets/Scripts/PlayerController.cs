using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 3f;
    public GameObject weapon;

    public float padding = 0.2f;

    public float health = 100f;

    public bool grounded = false;

    public float weaponPositionZ = -5;

    public KeyCode upButton;

    public KeyCode leftButton;

    public KeyCode rightButton;

    public KeyCode downButton;

    public KeyCode fireButton;

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
        weapon.GetComponent<GunController>().fire(direction);


        // use gun to fire
        // gun.fire(direction);

    }



    void Jump()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(rb.velocity.x, 5f);

    }
    void Move()
    {
        gameObject.transform.Translate(Input.GetAxis("Horizontal") * Vector3.right * speed * Time.deltaTime);
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
			Move();
        }
        if (Input.GetKey(rightButton))
        {
            transform.localScale = new Vector3(1, 1, 1);
			Move();
        }
        

        if (Input.GetKeyDown(fireButton))
        {
            Shoot(transform.localScale.x);
        }
    }

    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            grounded = true;
            print("hi");
        }
        else
        {
            grounded = false;
            print("false");
        }
    }

    void FixedUpdate()
    {
        Control();
    }
}
