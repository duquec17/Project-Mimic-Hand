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
        if (Vector3.Distance(enemy.transform.position, enemy.Player.position) <= 3f)
        {
            enemy.SetState(new ChaseState()); // Switch to Chase
        }
    }

    public void ExitState(EnemyAI enemy)
    {
        Debug.Log($"{enemy.name} exited Idle State.");
    }


}
