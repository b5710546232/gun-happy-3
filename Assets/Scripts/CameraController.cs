using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using System.Linq;

public class CameraController : MonoBehaviour
{

    // Use this for initialization
    private new Transform transform;
    private Vector3 DesiredPos;
    public List<Transform> Players;
    public float camSpeed;
    private Camera cam;
    void Awake()
    {
        transform = GetComponent<Transform>();
        cam = GetComponent<Camera>();
    }
    void Start()
    {
        var p = GameObject.FindGameObjectsWithTag("Player");
        Players = new List<Transform>();
        for (int i = 0; i < p.Length; i++)
        {
            Players.Add(p[i].GetComponent<Transform>());
        }
    }

    // Update is called once per frame
     void Update()
         {
             if (Players.Count <= 0)//early out if no players have been found
                 return;
             DesiredPos = Vector3.zero;
             float distance = 0f;
             var hSort = Players.OrderByDescending(p => p.position.y);
             var wSort = Players.OrderByDescending(p => p.position.x);
             var mHeight = hSort.First().position.y - hSort.Last().position.y;
             var mWidth = wSort.First().position.x - wSort.Last().position.x;
             var distanceH = -(mHeight + 5f) * 0.5f / Mathf.Tan(cam.fieldOfView * 0.5f * Mathf.Deg2Rad);
             var distanceW = -(mWidth / cam.aspect + 5f) * 0.5f / Mathf.Tan(cam.fieldOfView * 0.5f * Mathf.Deg2Rad);
             distance = distanceH < distanceW ? distanceH : distanceW;
 
             for (int i = 0; i < Players.Count; i++)
             {
                 DesiredPos += Players[i].position;
             }
             if (distance > -10f) distance = -10f;
             DesiredPos /= Players.Count;
             DesiredPos.z = distance;
         }
		  void LateUpdate()
         {
             transform.position = Vector3.MoveTowards(transform.position, DesiredPos, camSpeed);
         }
}
