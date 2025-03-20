using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatusEffect : ScriptableObject
{
    public float duration; // Effect duration in seconds

    public abstract void ApplyEffect(ReDoHealth target);
    public abstract void TickEffect(ReDoHealth target, float deltaTime);
    public abstract void RemoveEffect(ReDoHealth target);
}
