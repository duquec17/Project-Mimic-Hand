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
    [SerializeField] private float attackCooldown = 1.5f; // Centralized cooldown
    private float attackTimer = 0f; // Centralized timer

    private Transform player;
    public Transform Player => player;
    private ReDoHealth healthComponent;

    // Public property to access the distance to the player
    public float DistanceToPlayer => Vector3.Distance(transform.position, Player.position);
    public float EnemyDamage => enemyDamage; // Getter for enemy damage
    public float AttackCooldown => attackCooldown; // Getter for cooldown
    public float AttackTimer
    {
        get => attackTimer;
        set => attackTimer = value; // Getter/Setter for timer
    }

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
        attackTimer -= Time.deltaTime; // Update timer globally
        currentState.UpdateState(this); // Update the current state 
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
