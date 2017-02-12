using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IBotState
{

    private readonly StatePatternBot bot;
    private int nextWayPoint;

    public PatrolState(StatePatternBot statePatternBot)
    {
        bot = statePatternBot;
    }

    public void UpdateState()
    {
        Search();
    }

    public void OnTriggerEnter2D(Collider2D other) { }

    public void ToPatrolState() { }

    public void ToAlertState()
    {
        bot.currentState = bot.alertState;
    }

    public void ToChaseState()
    {
        bot.currentState = bot.chaseState;
    }

    void Search()
    {

           string s = "position y "+(bot.getTarget().transform.position.y - bot.transform.position.y);
        Debug.Log(s);

        if (bot.getTarget().transform.position.x - bot.transform.position.x < 0)
        {
            if (bot.getTarget().transform.position.x - bot.transform.position.x > -bot.attackRage)
            {
                // Debug.Log("in rage attack left");
                // bot.GetComponent<PlayerController>().Move(-1);
                // ToAlertState();
                return;
            }
            bot.GetComponent<PlayerController>().Move(-1);
            Debug.Log("enemy is in left patron");

        }
        if (bot.getTarget().transform.position.x - bot.transform.position.x > 0)
        {

            if (bot.getTarget().transform.position.x - bot.transform.position.x <= bot.attackRage)
            {
                // bot.GetComponent<PlayerController>().Move(1);
                Debug.Log("in rage attack right");
                // ToAlertState();
                return;
            }
            bot.GetComponent<PlayerController>().Move(1);
            Debug.Log("enemy is in right patron");

        }
        /***
        enemy is higher
        so should jump
        ***/
        if (bot.getTarget().transform.position.y - bot.transform.position.y > 0.0f){
            // Debug.Log("should jump");
            // bot.controller.Jump();
        }
     
          if (bot.getTarget().transform.position.y - bot.transform.position.y < 0.0f){
            // bot.controller.Drop();
            Debug.Log("down");
        }
    }
}

