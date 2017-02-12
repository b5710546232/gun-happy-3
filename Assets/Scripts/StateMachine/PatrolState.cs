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
        GoToMiddle();
        Search();
    }

    public void OnTriggerEnter2D(Collider2D other) {

     }

    public void ToPatrolState() { }

    public void ToCombatState()
    {
        bot.currentState = bot.combatState;
    }

    public void ToChaseState()
    {
        bot.currentState = bot.chaseState;
    }
    public void ToIdleState(){
        bot.currentState = bot.idleState;
    }

    void GoToMiddle(){
        bool outofState = Mathf.Abs(bot.getTarget().transform.position.x)>=1.5f;
        if (bot.getTarget().transform.position.y - bot.controller.transform.position.y < -2.0f ||outofState ){
            // goto middle
            Debug.LogError("go to middle");
            ToIdleState();
        }
        
    }

    void Search()
    {

            /***
        enemy is higher
        so should jump
        ***/
        float rectHeight = bot.controller.GetComponent<BoxCollider2D>().bounds.center.y /2;
        // Debug.LogError(rectHeight+"height");
        if (bot.getTarget().transform.position.y - bot.controller.transform.position.y > 0.2f ){
            // Debug.Log("should jump");
            bot.controller.Jump();
        }
     
          if (bot.getTarget().transform.position.y - bot.controller.transform.position.y < rectHeight){
            bot.controller.Drop();
            Debug.Log("down");
        }

        //    string s = "position y "+(bot.getTarget().transform.position.y - bot.controller.transform.position.y);
        // Debug.Log(s);

        if (bot.getTarget().transform.position.x - bot.controller.transform.position.x <= 0)
        {
            if (bot.getTarget().transform.position.x - bot.controller.transform.position.x > -bot.attackRage)
            {
                // Debug.Log("in rage attack left");
                bot.controller.Move(-1);
                ToCombatState();
                return;
            }
            bot.controller.Move(-1);
            Debug.Log("enemy is in left patron");

        }
        if (bot.getTarget().transform.position.x - bot.controller.transform.position.x > 0)
        {

            if (bot.getTarget().transform.position.x - bot.controller.transform.position.x <= bot.attackRage)
            {
                bot.controller.Move(1);
                Debug.Log("in rage attack right");
                ToCombatState();
                return;
            }
            bot.controller.Move(1);
            Debug.Log("enemy is in right patron");

        }
    
    }
}

