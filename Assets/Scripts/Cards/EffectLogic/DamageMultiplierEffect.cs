using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DamageMultiplierEffect", menuName = "CardEffects/DamageMultiplierEffect")]
public class DamageMultiplierEffect : CardEffects
{
    public float multiplier = 2f; // Multiplier for next attack
    public override void ApplyEffect(GameObject target)
    {
        // Apply multiplier to the player's attack
        PlayerControls playerControls = target.GetComponent<PlayerControls>();

        if (playerControls != null)
        {
            playerControls.SetDamageMultiplier(multiplier);
        }
        else
        {
            Debug.Log("There is no player controls component on target");
        }
    }
}
