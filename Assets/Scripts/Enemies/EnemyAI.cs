using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    Idle,
    Chase,
    Rage,
    Attack
}

public class EnemyAI : MonoBehaviour
{
    // Enemy Ai Variable List
    public IEnemyState currentState;
    public float enemySpeed;
    public float enemyJump;
    
    [SerializeField] private float enemyDamage = 1f;
    [SerializeField] private float enemyDamageMultiplier = 1f;
    
    private Transform player;
    public Transform Player => player;
    private ReDoHealth healthComponent;

    // Initialize variables for enemy to then later use
    void Start()
    {
        // Assign player variable to have interactions with enemy
        player = GameObject.FindGameObjectWithTag("Player").transform;
        healthComponent = GetComponent<ReDoHealth>(); // Possibly add player. to GC to make sure it gets the right version of RDH
        
        SetState(new IdleState());
    }

    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SetState(IEnemyState newState)
    {

        if (currentState != null)
        {
            currentState.ExitState(this);
        }
        currentState = newState;
        currentState.EnterState(this);
    }

}
