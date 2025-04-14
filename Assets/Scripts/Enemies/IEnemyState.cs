using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyState
{
    void EnterState(EnemyAI enemy);
    void UpdateState(EnemyAI enemy);
    void ExitState(EnemyAI enemy);
}
