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
    public float stateLockTimer = 0f;
    public bool canChangeState => stateLockTimer <= 0f;
    public float enemySpeed;
    public float enemyJump;
    
    [SerializeField] private float enemyDamage = 1f;
    [SerializeField] private float enemyDamageMultiplier = 1f;
    [SerializeField] private float attackCooldown = 1.5f; // Centralized cooldown
    [SerializeField] private float attackRange = 6f;
    [SerializeField] private float chaseRange = 10f;
    [SerializeField] private float idleRange = 12f;
    private float attackTimer = 0f; // Centralized timer

    private Transform player; // Track what is considered player and used for certain logic
    public Transform Player => player;
    private ReDoHealth healthComponent;

    // Public property to access the distance to the player
    public float DistanceToPlayer => player != null ? Vector3.Distance(transform.position, Player.position) : Mathf.Infinity;
    public float EnemyDamage => enemyDamage; // Getter for enemy damage
    public float EnemyDamageMultiplier => enemyDamageMultiplier;
    public float AttackCooldown => attackCooldown; 
    public float AttackRange => attackRange;
    public float ChaseRange => chaseRange;
    public float IdleRange => idleRange;
    public float AttackTimer
    {
        get => attackTimer;
        set => attackTimer = value; // Getter/Setter for timer
    }

    // Initialize variables for enemy to then later use
    protected virtual void Start()
    {
        // Assign player variable safely
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
            Debug.Log($" Player successfully assigned to {gameObject.name}.");
        }
        else
        {
            Debug.LogError("Player not found! Ensure Player has the correct tag.");
        }

        healthComponent = GetComponent<ReDoHealth>(); // Possibly add player. to GC to make sure it gets the right version of RDH
        
        SetState(new IdleState());
    }

    void Update()
    {
        attackTimer -= Time.deltaTime; // Update timer globally
        stateLockTimer -= Time.deltaTime; // Update state timer globally
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
