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
        if (enemy.DistanceToPlayer >= 3f && enemy.DistanceToPlayer <= 6f)
        {
            enemy.SetState(new ChaseState());
        }

        // Switch to idle state if player has left range
        if (enemy.DistanceToPlayer > 6f)
        {
            enemy.SetState(new IdleState());
        }
    }

    public void ExitState(EnemyAI enemy)
    {
        Debug.Log($"{enemy.name} exited Attack State.");
    }

}
