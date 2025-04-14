using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RageState : IEnemyState
{
    public void EnterState(EnemyAI enemy)
    {
        Debug.Log($"{ enemy.name} entered Rage State.");
    }

    public void UpdateState(EnemyAI enemy)
    {
        
    }

    public void ExitState(EnemyAI enemy)
    {
        Debug.Log($"{enemy.name} exited Rage State.");
    }


}
