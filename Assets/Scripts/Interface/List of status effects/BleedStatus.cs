using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BleedStatus", menuName = "StatusEffects/BleedStatus")]
public class BleedStatus : StatusEffect
{
    // Bleed Effect Variables
    private float timer = 0f;
    private int tickCount = 0;

    public int maxTicks = 2;
    public float tickInterval = 3f;
    public float bleedPercent = 0.05f;
    public float bleedDamage;

    // Bleed status overrides apply effect logic so that it may work
    public override void ApplyEffect(ReDoHealth target)
    {
        //Debug.Log(target.name + " is bleeding!");
        tickCount = 0; // Reset tick count when effect is applied
        duration = maxTicks * tickInterval;
    }

    public override void TickEffect(ReDoHealth target, float deltaTime)
    {
        // Increase timer 
        timer += deltaTime;

        // Apply bleed damage every 3 seconds
        if (timer >= tickInterval)
        {
            // Reset timer
            timer = 0f;

            // Calculate and assign bleed damage based on 5% of current enemy health
            bleedDamage = target.CurrentHealth * bleedPercent;
            
            // Apply damage to target
            target.Damage(bleedDamage);

            // Log the effect for debugging
            Debug.Log($"Bleed tick {tickCount + 1} applied to {target.name}: {bleedDamage} damage.");

            // Increase tick count
            tickCount++;

            // Check if effect has reached maximum number of ticks
            if (tickCount >= maxTicks)
            {
                duration = 0; // Mark for removal effect
            }
        }
    }

    public override void RemoveEffect(ReDoHealth target)
    {
        Debug.Log($"{target.name} has stopped bleeding.");
    }

}
