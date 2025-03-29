using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EoaStatus", menuName = "StatusEffects/EoaStatus")]
public class EoaStatus : StatusEffect
{
    // End of all Effect Variables
    //private float timer = 0f;
    private float eoaPercent = 0.10f;
    private float eoaDamage;

    public override void ApplyEffect(ReDoHealth target)
    {
        eoaDamage = target.MaxHealth * eoaPercent; // Set damage to be 10 percent of max health

        // Apply damage to current health immediately
        target.Damage(eoaDamage);

        // Reduce target max health by damage amount
        target.maxHealth -= eoaDamage;
    }

    public override void TickEffect(ReDoHealth target, float deltaTime)
    {
        duration -= deltaTime;
        
        // Check if the effect has expired
        if (duration <= 0)
        {
            duration = 0; // Ensure it is marked for removal
        }
    }

    public override void RemoveEffect(ReDoHealth target)
    {
        // Reduce target max health by damage amount
        target.maxHealth += eoaDamage;
        Debug.Log($"{target.name} has stopped EOA.");
    }

}
