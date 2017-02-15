using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeController : MonoBehaviour
{
    public List<string> targets;

    public bool isHit = true;

    // Use this for initialization
    void Start()
    {
        isHit = false;

    }

    // Update is called once per frame
    void Update()
    {


    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        foreach (string target in targets)
        {
            if (other.gameObject.tag == target)
                isHit = true;
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
        }


        // if (other.gameObject.tag == "Ground" || other.gameObject.tag == "AirFloor")
        // {
        //     // print("exit");
        //     isHit = false;
        // }
    }
}
