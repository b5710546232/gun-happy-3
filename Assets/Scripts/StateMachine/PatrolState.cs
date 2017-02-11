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

    public void ToAlertState() { }

    public void ToChaseState() { }

    void Search(){
        if(bot.getTarget().transform.position.x <= bot.transform.position.x){
            bot.GetComponent<PlayerController>().Move(-1);
        }
        
        else{
            bot.GetComponent<PlayerController>().Move(1);
        }
    }
}

