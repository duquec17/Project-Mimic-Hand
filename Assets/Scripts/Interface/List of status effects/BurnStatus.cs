using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BurnStatus", menuName = "StatusEffects/BurnStatus")]
public class BurnStatus : StatusEffect
{
    // Burn variables
    private float timer = 0f; // Tracks time of damage intervals
    private float burnInterval = 1f; // Damage will be applied every 1 second
    private float burnDamage; // 5% max hp removed ever second

    // Burn status overrides apply effect logic so that it may work
    public override void ApplyEffect(ReDoHealth target)
    {
        Debug.Log(target.name + " is burning!");
        timer = 0f; // Reset interval timer
    }
    public override void TickEffect(ReDoHealth target, float deltaTime)
    {
        // Accumulate time for interval check
        timer += deltaTime;

        // Apply burn damage at each interval
        if (timer >= burnInterval)
        {
            timer -= burnInterval; // Reset the interval timer

            // Calculate burn damage as 10% of max health
            burnDamage = target.MaxHealth * 0.10f;

            // Apply damage
            target.Damage(burnDamage);

            // Log for debugging
            Debug.Log($"Burn applied to {target.name}: {burnDamage} damage this second.");
        }
        else
        {
            Debug.Log("Failed to burn " + duration);
        }

        // Reduce remaining duration
        duration -= deltaTime;

        // Check if the effect has expired
        if (duration <= 0)
        {
            duration = 0; // Ensure it is marked for removal
        }
    }

    public override void RemoveEffect(ReDoHealth target)
    {
        Debug.Log($"{target.name} has stopped burning.");
    }

   

}
