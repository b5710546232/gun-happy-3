using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatState : IBotState
{

    private readonly StatePatternBot bot;
    private float searchTimer;

    public CombatState(StatePatternBot statePatternBot)
    {
        bot = statePatternBot;
    }
    public void UpdateState()
    {
        GoToMiddle();
        bot.controller.Shoot(bot.controller.getDirection());
        Combat();
    }

    public void OnTriggerEnter2D(Collider2D other) { }

    public void ToPatrolState()
    {
        bot.currentState = bot.patroState;
    }

void GoToMiddle(){
        bool outofState = Mathf.Abs(bot.getTarget().transform.position.x)>=1.2f;
        if (bot.getTarget().transform.position.y - bot.controller.transform.position.y < -2.0f ||outofState ){
            // goto middle
            // Debug.LogError("go to middle");
            if(bot.getTarget().transform.position.x<=0){
                bot.controller.Move(-1);
            }
            else{
                bot.controller.Move(1);
            }
        }
        
    }
   

    public void ToCombatState() { }

    public void ToChaseState()
    {

    }
    public void ToIdleState(){}
    void Combat()
    {

        if (bot.getTarget().transform.position.x - bot.controller.transform.position.x < 0)
        {
            // bot.GetComponent<PlayerController>().Move(1);
            // Debug.Log("enemy is in left alert");
            //  if(bot.getTarget().transform.position.x - bot.controller.transform.position.x <= bot.attackRage){
            //     ToPatrolState();
            //     return;
            // }
            // Debug.Log("enemy in left alert");
            bot.controller.Move(-1);
            ToPatrolState();

        }
        if (bot.getTarget().transform.position.x - bot.controller.transform.position.x > 0)
        {

            // if(bot.getTarget().transform.position.x - bot.controller.transform.position.x >= bot.attackRage){
            //     ToPatrolState();
            //     return;
            // }
            // Debug.Log("enemy is in right alert");
            bot.controller.Move(1);
            ToPatrolState();

        }
    }
}

