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

        if (bot.getTarget().transform.position.x - bot.transform.position.x < 0)
        {
            if (bot.getTarget().transform.position.x - bot.transform.position.x > -bot.attackRage)
            {
                Debug.Log("in rage attack left");
                ToAlertState();
                return;
            }
            bot.GetComponent<PlayerController>().Move(-1);
            Debug.Log("enemy is in left patron");

        }
        if (bot.getTarget().transform.position.x - bot.transform.position.x > 0)
        {

            if (bot.getTarget().transform.position.x - bot.transform.position.x <= bot.attackRage)
            {
                Debug.Log("in rage attack right");
                ToAlertState();
                return;
            }
            bot.GetComponent<PlayerController>().Move(1);
            Debug.Log("enemy is in right patron");

        }
    }
}

