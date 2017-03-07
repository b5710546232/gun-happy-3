using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{

    public List<GameObject> spawnerPoints;
	public List<GameObject> itemList = new List<GameObject>(1);
	public int randPointIndex;
    // Use this for initialization

	void Awake()
	{
		spawnerPoints = new List<GameObject>();
		itemList = new List<GameObject>();
	}
    void Start()
    {
        foreach (Transform child in transform)
        {
			spawnerPoints.Add(child.gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
