using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IEnemyState
{

  private readonly StatePatternEnemy enemy;
    private int nextWayPoint;

    public PatrolState (StatePatternEnemy statePatternEnemy)
    {
        enemy = statePatternEnemy;
    }

    public void UpdateState() { 
        enemy.GetComponent<PlayerController>().Move(-1);
    }

    public void OnTriggerEnter2D(Collider2D other) { }

    public void ToPatrolState() { }

    public void ToAlertState() { }

    public void ToChaseState() { }
}

