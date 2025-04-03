using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IEnemyState
{

    public void EnterState(EnemyAI enemy)
    {
        Debug.Log($"{ enemy.name} entered Attack State.");
    }

    public void UpdateState(EnemyAI enemy)
    {
        // Switch to chase state if player is in range of it, but not attack range
        if (enemy.DistanceToPlayer <= enemy.ChaseRange)
        {
            enemy.SetState(new ChaseState());
        }

        // Switch to idle state if player has left range
        if (enemy.DistanceToPlayer >= enemy.IdleRange)
        {
            enemy.SetState(new IdleState());
        }

        // Attack logic
        if (enemy.AttackTimer <= 0f)
        {
            PerformAttack(enemy);
            enemy.AttackTimer = enemy.AttackCooldown; // Reset timer
        }
    }

    public void ExitState(EnemyAI enemy)
    {
        Debug.Log($"{enemy.name} exited Attack State.");
    }

    private void PerformAttack(EnemyAI enemy)
    {
        Debug.Log($"{enemy.name} attacks!");
        if (enemy.Player.TryGetComponent<ReDoHealth>(out ReDoHealth playerHealth))
        {
            playerHealth.Damage(enemy.EnemyDamage * enemy.EnemyDamageMultiplier);
        }
    }
}
