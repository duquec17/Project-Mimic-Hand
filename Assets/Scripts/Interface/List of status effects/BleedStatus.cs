using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [CreateAssetMenu(fileName = "BleedStatus", menuName = "StatusEffects/BleedStatus")]
public class BleedStatus : StatusEffect
{
    // Bleed Effect Variables
    private float timer = 0f;

    // Bleed status overrides apply effect logic so that it may work
    public override void ApplyEffect(ReDoHealth target)
    {
        Debug.Log(target.name + " is bleeding!");
    }

    public override void TickEffect(ReDoHealth target, float deltaTime)
    {
        timer += deltaTime;
    }

    public override void RemoveEffect(ReDoHealth target)
    {

    }

}
