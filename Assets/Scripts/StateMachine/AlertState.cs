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

    }

    public void OnTriggerEnter2D(Collider2D other) { }

    public void ToPatrolState() { }

    public void ToAlertState() { }

    public void ToChaseState() {
        
     }
}

