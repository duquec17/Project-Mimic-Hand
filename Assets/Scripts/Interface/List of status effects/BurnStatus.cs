using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BurnStatus", menuName = "StatusEffects/BurnStatus")]
public class BurnStatus : StatusEffect
{
    // Burn variables
    private float timer = 0f;
    private float burnDuration = 5f; // A duration of 5 seconds 
    private float burnDamage; // 5% max hp removed ever second

    // Burn status overrides apply effect logic so that it may work
    public override void ApplyEffect(ReDoHealth target)
    {
        Debug.Log(target.name + " is burning!");
    }
    public override void TickEffect(ReDoHealth target, float deltaTime)
    {
        timer += deltaTime;

        if (burnDuration >= timer)
        {
            // Calculate burn damage based off max health of target
            burnDamage = target.MaxHealth * 0.02f;

            // Pass calculated burn damage to target
            target.Damage(burnDamage);

            // Log the effect for debugging
            Debug.Log($"Burn applied to {target.name}: {burnDamage} damage.");

            // Decrease burn duration
            burnDuration -= burnDuration - deltaTime;
        }
        else
        {
            duration = 0; // Mark for removal effect
        }
    }

    public override void RemoveEffect(ReDoHealth target)
    {
        Debug.Log($"{target.name} has stopped burning.");
    }

   

}
