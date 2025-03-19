using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BurnEffect", menuName = "CardEffects/BurnEffect")]
public class BurnEffect : CardEffects
{
    // Effect Variables
    public float burnDamagePercent = 0.05f; // 5% of max HP every second
    public float burnDuration = 5f; // Burn duration in seconds

    public override void ApplyEffect(GameObject target)
    {
        Debug.Log("Target passed to BurnEffect: " + target.name);

        // Check for enemy health component
        EnemyHealth enemyHealth = target.GetComponent<EnemyHealth>();

        if (enemyHealth != null)
        {
            Debug.Log("BurnEffect applied to: " + target.name);
            enemyHealth.ApplyBurn(burnDamagePercent, burnDuration); // Call the ApplyBurn method on EnemyHealth
        } else
        {
            Debug.Log("Failed to Burn");
        }
    }
}
