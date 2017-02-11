using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertState : IEnemyState
{

private readonly StatePatternEnemy enemy;
    private float searchTimer;
    
    public AlertState (StatePatternEnemy statePatternEnemy)
    {
        enemy = statePatternEnemy;
    }
    public void UpdateState() { }

    public void OnTriggerEnter2D(Collider2D other) { }

    public void ToPatrolState() { }

    public void ToAlertState() { }

    public void ToChaseState() { }
}

