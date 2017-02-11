using UnityEngine;
using System.Collections;

public interface IBotState {

   void UpdateState();

    void OnTriggerEnter2D (Collider2D other);

    void ToPatrolState();

    void ToAlertState();

    void ToChaseState();
}
