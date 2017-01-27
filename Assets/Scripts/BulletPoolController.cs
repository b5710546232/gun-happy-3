using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPoolController : MonoBehaviour {

	public int SIZE = 0;
	public List<GameObject> pools;

	public GameObject bullet;

	// Use this for initialization
	void Start () {
		print("hello");
		pools.Clear();
		GameObject _bullet = Instantiate(bullet, transform.position, Quaternion.identity) as GameObject;
		_bullet.SetActive(false);
		pools.Add(_bullet);
		for(int i = 0 ; i<SIZE; i++){
			// GameObject _bullet = Instantiate(bullet, transform.position, Quaternion.identity) as GameObject;
			// _bullet.SetActive(false);
			// pools.Add(_bullet);
		}
	}

	public GameObject init(Vector3 position){

		foreach(GameObject item in pools){
			if(!item.activeInHierarchy){
				item.transform.position = position;
				item.SetActive(true);
				return item;
			}
		}

		GameObject _bullet = Instantiate(bullet,position, Quaternion.identity);
		pools.Add(_bullet);

		return  _bullet;


	}
	
	

}
