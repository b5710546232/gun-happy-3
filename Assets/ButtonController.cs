using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ButtonController : MonoBehaviour {

	public void OnButtonIsClicked(){
		SceneManager.LoadScene(1);
	}
}
