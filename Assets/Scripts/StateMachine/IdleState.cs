using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IBotState
{
    private readonly StatePatternBot bot;


    public IdleState(StatePatternBot statePatternBot)
    {
        bot = statePatternBot;
    }

    public void UpdateState()
    {
        float midle_pos_x = 0;
        float del_X = bot.getTarget().transform.position.x - bot.controller.transform.position.x;
        float del_Y = bot.getTarget().transform.position.y - bot.controller.transform.position.y;
        if (bot.controller.transform.position.x < 0f)
        {
            bot.controller.Move(1);
        }
        else if(bot.controller.transform.position.x >=1.2f)
        {
            bot.controller.Move(-1);
			//    if (del_X <= bot.attackRage && Mathf.Abs(del_Y) <= 2f)
            // {
            //     ToPatrolState();
            // }
            // if (del_X > -bot.attackRage)
            // {
            //     ToPatrolState();
            // }
        }
		// else{

		// }
		 if (Mathf.Abs(del_X) <= bot.attackRage && Mathf.Abs(del_Y) <= 2f)
            {
                ToPatrolState();
            }
        // else if (del_X > -bot.attackRage)
        //     {
        //         // ToPatrolState();
        //     }
			else{}



    }

    public void OnTriggerEnter2D(Collider2D other)
    {

    }



    public void ToPatrolState()
    {
        bot.currentState = bot.patroState;
    }

    public void ToCombatState() { }

    public void ToChaseState() { }

    public void ToIdleState() { }
}

