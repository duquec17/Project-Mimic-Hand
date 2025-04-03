using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackState : IEnemyState
{
    private float attackCooldown = 5f; // Every 5 seconds

    public void EnterState(EnemyAI enemy)
    {
        Debug.Log($"{enemy.name} entered Boss Attack State.");
        enemy.AttackTimer = attackCooldown; // Set initial cooldown
    }

    public void UpdateState(EnemyAI enemy)
    {
        if (!(enemy is BossEnemyAI boss)) return; // Ensure that only boss uses this state

        // Switch to Chase if player moves out of attack range
        if (enemy.DistanceToPlayer > 6f)
        {
            enemy.SetState(new ChaseState());
            return;
        }

        // Countdown and attack when ready
        if (enemy.AttackTimer <= 0f)
        {
            boss.FireProjectile(); // Call Boss's projectile attack function
            enemy.AttackTimer = attackCooldown; // Reset timer
        }
    }

    public void ExitState(EnemyAI enemy)
    {
        Debug.Log($"{enemy.name} exited Boss Attack State.");
    }

    
}
