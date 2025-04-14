using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : IEnemyState
{
    public void EnterState(EnemyAI enemy)
    {
        Debug.Log($"{ enemy.name} entered Chase State.");
    }

    public void UpdateState(EnemyAI enemy)
    {
        // Switch to attack state if close enough
        if (enemy.DistanceToPlayer <= enemy.AttackRange)
        {
            if (enemy is BossEnemyAI)
                enemy.SetState(new BossAttackState());
            else
                enemy.SetState(new AttackState());
            return;
        }

        // Switch to idle state if player has left range
        if (enemy.DistanceToPlayer >= enemy.IdleRange)
        {
            enemy.SetState(new IdleState());
        }

        // Move toward the player
        enemy.transform.position = Vector3.MoveTowards(
            enemy.transform.position,
            enemy.Player.position,
            enemy.enemySpeed * Time.deltaTime
        );
    }

    public void ExitState(EnemyAI enemy)
    {
        Debug.Log($"{enemy.name} exited Chase State.");
    }

}
