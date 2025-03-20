using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [CreateAssetMenu(fileName = "BleedStatus", menuName = "StatusEffects/BleedStatus")]
public class BleedStatus : StatusEffect
{
    // Bleed Effect Variables
    private float timer = 0f;
    public float bleedPercent = 0.05f;
    public float bleedDamage;

    // Bleed status overrides apply effect logic so that it may work
    public override void ApplyEffect(ReDoHealth target)
    {
        Debug.Log(target.name + " is bleeding!");
    }

    public override void TickEffect(ReDoHealth target, float deltaTime)
    {
        // Increase timer 
        timer += deltaTime;

        // Apply bleed damage every 3 seconds, after it has occured 2 times remove the effect
        if (timer >= 3f)
        {
            // Calculate and assign bleed damage based on 5% of current enemy health
            bleedDamage = target.CurrentHealth * bleedPercent; 
            target.Damage(bleedDamage);

        }
    }

    public override void RemoveEffect(ReDoHealth target)
    {

    }

}
