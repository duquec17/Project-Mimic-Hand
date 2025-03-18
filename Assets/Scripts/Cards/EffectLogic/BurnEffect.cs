using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BurnEffect", menuName = "CardEffects/BurnEffect")]
public class BurnEffect : CardEffects
{
    // Effect Variables
    public float damagePercent = 0.01f; // 1% of max HP every second
    public float duration = 5f; // Burn duration in seconds
    public override void ApplyEffect(GameObject target)
    {
        // Checks to see if hit target has the script enemy health
        EnemyHealth enemyHealth = target.GetComponent<EnemyHealth>();
        DummyHealth dummyHealth = target.GetComponent<DummyHealth>(); // TODO: Remove after checking that this works for dummy / adjust dummy to use enemy health & be a prefab

        if (enemyHealth != null)
        {
            
        }
    }
}
