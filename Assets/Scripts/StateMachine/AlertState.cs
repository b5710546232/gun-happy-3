using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertState : IBotState
{

private readonly StatePatternBot bot;
    private float searchTimer;
    
    public AlertState (StatePatternBot statePatternBot)
    {
        bot = statePatternBot;
    }
    public void UpdateState() { 
        bot.GetComponent<PlayerController>().Shoot(bot.GetComponent<PlayerController>().getDirection());
        Search();
    }

    public void OnTriggerEnter2D(Collider2D other) { }

    public void ToPatrolState() {
        bot.currentState = bot.patroState;
     }

    public void ToAlertState() { }

    public void ToChaseState() {

     }
         void Search(){
     
        if(bot.getTarget().transform.position.x - bot.transform.position.x < 0){
            // bot.GetComponent<PlayerController>().Move(1);
            Debug.Log("enemy is in left");
            
        }
        if(bot.getTarget().transform.position.x - bot.transform.position.x > 0){

            if(bot.getTarget().transform.position.x - bot.transform.position.x >= 3f){
                Debug.Log("in rage attack");    
                ToPatrolState();
                return;
            }
            Debug.Log("enemy is in right");
            
        }
    }
}

