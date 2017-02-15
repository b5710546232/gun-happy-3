using UnityEngine;
using System.Collections;

public interface IBotState {

   void UpdateState();


    void ToPatrolState();

    void ToCombatState();

    void ToChaseState();
    void ToIdleState();
}
