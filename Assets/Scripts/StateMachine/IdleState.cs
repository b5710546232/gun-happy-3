using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IBotState
{
 private readonly StatePatternBot bot;

    
    public IdleState (StatePatternBot statePatternBot)
    {
        bot = statePatternBot;
    }

    public void UpdateState() { }

    public void OnTriggerEnter2D(Collider2D other) { }

    public void ToPatrolState() { }

    public void ToCombatState() { }

    public void ToChaseState() { }

    public void ToIdleState(){}
}

