using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootController : MonoBehaviour {
	public bool grounded;
    public bool drop;


	// Use this for initialization
	void Start () {
		grounded = false;
        drop = false;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public bool isGrounded(){
		return grounded;
	}

    void OnCollisionStay2D(Collision2D other)
    {

        float RectHeightObj = other.collider.bounds.size.y;
        float upObj = other.collider.bounds.center.y + RectHeightObj/2;

        float footHeight = GetComponent<BoxCollider2D>().size.y;
        float downFoot = GetComponent<BoxCollider2D>().bounds.center.y - footHeight/2;
        
      if (other.gameObject.tag == "Ground" || other.gameObject.tag == "AirFloor" )
        {
            grounded = true;
        }
        
        if(other.gameObject.tag == "AirFloor"){
            if(downFoot>=upObj)
            drop = true;
        }
        if(other.gameObject.tag == "Ground"){
            drop = false;
        }

    }


    void OnTriggerStay2D(Collider2D other)
    {
        
         if (other.gameObject.tag == "Ground" || other.gameObject.tag == "AirFloor" )
        {

            grounded = true;
        }
        
        if(other.gameObject.tag == "AirFloor"){
            drop = true;
        }
        if(other.gameObject.tag == "Ground"){
            drop = false;
        }

    }

   
    void OnTriggerEnter2D(Collider2D other)
    {
          
           if (other.gameObject.tag == "Ground")
        {

            grounded = false;
        }
         if(other.gameObject.tag == "AirFloor"){
            grounded = false;
            drop = true;
        }
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
            if (other.gameObject.tag == "Ground")
        {

            grounded = false;
        }
         if(other.gameObject.tag == "AirFloor"){
            grounded = false;
            drop = false;
        }
        
    }

    void OnCollisionExit2D(Collision2D other)
    {
        
           if (other.gameObject.tag == "Ground")
        {

            grounded = false;
        }
         if(other.gameObject.tag == "AirFloor"){
            grounded = false;
            drop = false;
        }
    }

    

}
