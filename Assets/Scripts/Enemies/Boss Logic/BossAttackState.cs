using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackState : IEnemyState
{
    private float attackCooldown = 5f; // Every 5 seconds
    private float spikeCooldown = 3f; // Cooldown for spike attack
    private float spikeTimer = 0f; // Timer to track spike cooldown

    public void EnterState(EnemyAI enemy)
    {
        Debug.Log($"{enemy.name} entered Boss Attack State.");

        if (enemy is BossEnemyAI boss)
        {
            boss.FireProjectile(); // Call Boss's projectile attack function to fire immediately
            boss.SummonSpikes(); // Call Boss's spike attack function to begin
            enemy.AttackTimer = attackCooldown; // Set initial cooldown
            enemy.stateLockTimer = 2.5f;
        }
    }

    public void UpdateState(EnemyAI enemy)
    {
        if (!(enemy is BossEnemyAI boss)) return; // Ensure that only boss uses this state
            

        // Ensure player is not null before checking distance
        if (enemy.Player == null)
        {
            Debug.LogWarning("Player reference is missing in BossAttackState.");
            return;
        }

        // Switch to Chase if player moves out of attack range
        if (enemy.canChangeState && enemy.DistanceToPlayer > 6f)
        {
            enemy.SetState(new ChaseState());
            return;
        }

        // Countdown and attack when ready
        if (enemy.AttackTimer <= 0f && enemy.canChangeState)
        {
            boss.FireProjectile(); // Call Boss's projectile attack function
            boss.SummonSpikes();
            enemy.AttackTimer = attackCooldown; // Reset timer
            //enemy.stateLockTimer = 2.5f;
        }
    }

    public void ExitState(EnemyAI enemy)
    {
        Debug.Log($"{enemy.name} exited Boss Attack State.");
    }
}
