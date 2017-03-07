using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour {

public List<PlayerController> players;

    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<GameManager>();

            }

            return instance;
        }
    }
    // Use this for initialization
    void Start()
    {
        var p = GameObject.FindGameObjectsWithTag("Player");
        players = new List<PlayerController>();
        for (int i = 0; i < p.Length; i++)
        {
            PlayerController player = p[i].GetComponent<PlayerController>();
            players.Add(player);
        }

    }

    // Update is called once per frame
	public void GameOver(){
		SceneManager.LoadScene(0);
	}	
}
