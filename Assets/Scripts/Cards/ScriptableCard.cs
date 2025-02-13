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
    [field: SerializeField] public CardBackgrounds CardBackgrounds { get; private set; }

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