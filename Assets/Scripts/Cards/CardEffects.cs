using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardEffects : ScriptableObject
{
    public abstract void ApplyEffect(GameObject target); // Target can be anything (Player, enemy, etc.)
}

[CreateAssetMenu (fileName = "DamageMultiplierEffect", menuName = "CardEffects/DamageMultiplierEffect")]
public class DamageMultiplierEffect : CardEffects
{
    public float multiplier = 2f; // Multiplier for next attack
    public override void ApplyEffect(GameObject target)
    {
        // Apply multipler to the player's attack
        PlayerControls playerControls = target.GetComponent<PlayerControls>();

        if (playerControls != null)
        {
            playerControls.SetDamageMultiplier(multiplier);
        }
    }
}
