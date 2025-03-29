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
        // Check to see if player has left range
        if (Vector3.Distance(enemy.transform.position, enemy.Player.position) >= 6f)
        {
            enemy.SetState(new ChaseState()); // Switch back to idle state
        }
    }

    public void ExitState(EnemyAI enemy)
    {
        Debug.Log($"{enemy.name} exited Chase State.");
    }

}
