using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Purpose:
 * Holds all data for each individual card
 * 
 * Writer: Cristian Duque
 * ---------------------------------------
 * Scripts borrowing from it:
 * CardUI.cs
 * ---------------------------------------
 * Scripts it borrows from:
 * Card.cs
 */

[CreateAssetMenu(menuName = "CardData")] // Lets you create a new CardData Object with the right-click menu in the editor
public class ScriptableCard : ScriptableObject
{
    // field: SerializeFiekd lets you reveal properties in the inspetor like they were public fields
    [field: SerializeField] public string CardName { get; private set; } 
    [field: SerializeField, TextArea] public string CardDescription { get; private set; } // TextArea makes an input field in the inspector to write long in
    [field: SerializeField] public string CardType { get; private set; }
    [field: SerializeField] public Sprite Image { get; private set; }
    [field: SerializeField] public CardEffectType EffectType { get; private set; }
    [field: SerializeField] public CardEffects cardEffect { get; private set; } // Refrences CardEffects.cs
    [field: SerializeField] public StatusEffect statusEffect { get; private set; } // Refrences statusEffect.cs
    [field: SerializeField] public CardBackgrounds CardBackgrounds { get; private set; }

    public void PlayCard(GameObject target)
    {
        // Apply the effect (such as damage multiplier) to the player or enemy
        if (cardEffect != null)
        {
            cardEffect.ApplyEffect(target); // Apply the effect when the card is played
        }


        if (statusEffect != null)
        {
            ReDoHealth targetHealth = target.GetComponent<ReDoHealth>();
            if (targetHealth != null)
            {
                targetHealth.ApplyStatusEffect(statusEffect);
            }
            else
            {
                Debug.LogWarning($"Target {target.name} does not have a ReDoHealth component to apply status effects.");
            }
        }
    }
}



public enum CardEffectType
{
    Base,
    Buff,
    Debuff,
    Wild
}

public enum CardBackgrounds
{
    Base,
    Buff,
    Debuff,
    Wild
}