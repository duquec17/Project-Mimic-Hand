using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardEffects : ScriptableObject
{
    public abstract void ApplyEffect(GameObject target); // Target can be anything (Player, enemy, etc.)
}
