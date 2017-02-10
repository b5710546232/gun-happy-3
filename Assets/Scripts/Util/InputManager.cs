using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

	// Use this for initialization

    public KeyCode upButton;

    public KeyCode leftButton;

    public KeyCode rightButton;

    public KeyCode downButton;

    public KeyCode fireButton;

	public KeyCode getUpButton(){
		return upButton;
	}

	public KeyCode getLeftButton(){
		return leftButton;
	}

	public KeyCode getRightButton(){
		return rightButton;
	}

	public KeyCode getDownButton(){
		return downButton;
	}

	public KeyCode getFireButton(){
		return fireButton;
	}


}
