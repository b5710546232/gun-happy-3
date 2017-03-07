using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeController : MonoBehaviour
{
    public List<string> targets;

    public bool isHit = true;

    public GameObject temp;

    // Use this for initialization
    void Start()
    {
        gameObject.tag = "Eye";
        isHit = false;

    }

    // Update is called once per frame
    void Update()
    {


    }

    void OnTriggerEnter2D(Collider2D other)
    {
        foreach (string target in targets)
        {
           
            if (other.gameObject.tag == target)
                isHit = true;
                temp = other.gameObject;
        }
        // if (other.gameObject.tag == "Ground" || other.gameObject.tag == "AirFloor")
        //     {
        //         // print("hesasdf");
        //         isHit = true;
        //     }

    }
    void OnTriggerStay2D(Collider2D other)
    {
        // if (other.gameObject.tag == "Ground" || other.gameObject.tag == "AirFloor")
        // {
        //     // print("hesasdf");
        //     isHit = true;
        // }
        foreach (string target in targets)
        {
            if (other.gameObject.tag == target)
                isHit = true;
        }
    }



    void OnTriggerExit2D(Collider2D other)
    {
        foreach (string target in targets)
        {
            if (other.gameObject.tag == target)
                isHit = false;
                // temp = null;
        }


        // if (other.gameObject.tag == "Ground" || other.gameObject.tag == "AirFloor")
        // {
        //     // print("exit");
        //     isHit = false;
        // }
    }

    // /// <summary>
    // /// Callback to draw gizmos that are pickable and always drawn.
    // /// </summary>
    // void OnDrawGizmos()
    // {
    //     Gizmos.DrawWireSphere(transform.position,1);
    // }
}
