﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootController : MonoBehaviour {
	private bool grounded;
	// Use this for initialization
	void Start () {
		grounded = false;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public bool isGrounded(){
		return grounded;
	}

    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground" || other.gameObject.tag == "AirFloor")
        {

            grounded = true;
        }

    }

    void OnCollisionExit2D(Collision2D other)
    {
        
           if (other.gameObject.tag == "Ground"|| other.gameObject.tag == "AirFloor")
        {

            grounded = false;
        }
    }

}