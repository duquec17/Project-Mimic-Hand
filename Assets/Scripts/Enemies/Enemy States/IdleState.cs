using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IEnemyState
{
    public void EnterState(EnemyAI enemy)
    {
        Debug.Log($"{ enemy.name} entered Idle State.");
    }

    public void UpdateState(EnemyAI enemy)
    {
        // Checks to see if distance between player is enough to enter chase them, but not attack
        if (enemy.DistanceToPlayer <= enemy.ChaseRange && enemy.DistanceToPlayer > enemy.AttackRange)
        {
            enemy.SetState(new ChaseState()); // Switch to Chase
        }

        if (enemy.DistanceToPlayer <= enemy.AttackRange)
        {
            enemy.SetState(new BossAttackState()); // Or regular AttackState
        }
    }

    public void ExitState(EnemyAI enemy)
    {
        Debug.Log($"{enemy.name} exited Idle State.");
    }


}
