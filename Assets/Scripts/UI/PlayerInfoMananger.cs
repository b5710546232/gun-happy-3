using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoMananger : MonoBehaviour {

	public List<PlayerController> Players;

	public GameObject PlayerInfo;

	// Use this for initialization
	void Start () {
		var p = GameObject.FindGameObjectsWithTag("Player");
        Players = new List<PlayerController>();
        for (int i = 0; i < p.Length; i++)
        {
            Players.Add(p[i].GetComponent<PlayerController>());
			 if( p[i].GetComponent<PlayerController>().PID == 1 ){
				 GameObject pinfo = Instantiate(PlayerInfo,transform.position,Quaternion.identity).gameObject;
				 pinfo.transform.position = new Vector3(0.7f,-0.56f,-2);
				 p[i].GetComponent<PlayerController>().SetPlayerInfo(pinfo);
			 }
       		 if(  p[i].GetComponent<PlayerController>().PID == 2 ){
				GameObject pinfo = Instantiate(PlayerInfo,transform.position,Quaternion.identity).gameObject;
				pinfo.transform.position = new Vector3(1.92f,-0.56f,-2);
				p[i].GetComponent<PlayerController>().SetPlayerInfo(pinfo);
				}
            	
			

        }



	}

	void setPlayerNameText(){

	}

	void setLiveText(){

	}

	void setArmmoText(){

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
