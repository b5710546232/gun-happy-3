using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : IBotState
{
 private readonly StatePatternBot bot;

    
    public ChaseState (StatePatternBot statePatternBot)
    {
        bot = statePatternBot;
    }

    public void UpdateState() {
        Vector3 targetPos = bot.getTarget().transform.position;
        Vector3 Pos = bot.controller.transform.position;
        float rectHeight = bot.controller.GetComponent<BoxCollider2D>().bounds.center.y /2;

        if(!bot.eye2.GetComponent<EyeController>().isHit){
            ToPatrolState();
            return;
        }
        if(!bot.eye.GetComponent<EyeController>().isHit){
            ToPatrolState();
            return;
        }

        if(Mathf.Abs(targetPos.y - Pos.y) <= .02f){
            
            if(targetPos.y - Pos.y <0 ){
                bot.controller.Move(-1);
            }else{
                bot.controller.Move(1);
            }
            ToCombatState();

        }

        if(targetPos.x - Pos.x >.2f){
            bot.controller.Move(1);
        }
        if(targetPos.x - Pos.x < -.2f){
            bot.controller.Move(-1);   
        }
        if(targetPos.y - Pos.y > .3f){
            bot.controller.Jump();
        }
        if(targetPos.y - Pos.y < -rectHeight){
            bot.controller.Drop();
        }
     }

    public void OnTriggerEnter2D(Collider2D other) { }

    public void ToPatrolState() { 
        bot.currentState = bot.patroState;
    }

    public void ToCombatState() { 
        bot.currentState = bot.combatState;
    }

    public void ToChaseState() { }

    public void ToIdleState(){}
}

