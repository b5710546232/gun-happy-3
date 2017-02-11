using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IBotState
{

  private readonly StatePatternBot bot;
    private int nextWayPoint;

    public PatrolState (StatePatternBot statePatternBot)
    {
        bot = statePatternBot;
    }

    public void UpdateState() { 
        Search();
    }

    public void OnTriggerEnter2D(Collider2D other) { }

    public void ToPatrolState() { }

    public void ToAlertState() { 
        bot.currentState = bot.alertState;
    }

    public void ToChaseState() { 
        bot.currentState = bot.chaseState;
    }

    void Search(){
     
        if(bot.getTarget().transform.position.x - bot.transform.position.x < 0){
            // bot.GetComponent<PlayerController>().Move(1);
            Debug.Log("enemy is in left");
            
        }
        if(bot.getTarget().transform.position.x - bot.transform.position.x > 0){

            if(bot.getTarget().transform.position.x - bot.transform.position.x <= 3f){
                Debug.Log("in rage attack");    
                ToAlertState();
                return;
            }
            bot.GetComponent<PlayerController>().Move(1);
            Debug.Log("enemy is in right");
            
        }
    }
}

