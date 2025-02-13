using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* Purpose:
 * A generic collection of CardData objects. Can be used as a deck or a bosoter, for example
 * 
 * Writer: Cristian Duque
 * ------------------------------------
 * Scripts borrowing from it:
 * 
 * ------------------------------------
 * Scripts it borrows from:
 * 
 */
[CreateAssetMenu(menuName = "Card Collection")]
public class CardCollection : ScriptableObject
{
    [field: SerializeField] public List<ScriptableCard> CardsInCollection { get; private set; }

    // Optional: If you think you will need to edit the collection at runtime

    // Remove card from deck
    public void RemoveCardFromCollection(ScriptableCard card)
    {
        // Checks to see if selected card is already in collection
        // Removes card if present and does nothing when not present.
        if (CardsInCollection.Contains(card))
        {
            CardsInCollection.Remove(card);
        }
        else
        {
            Debug.LogWarning("Card is not present in collection - cannot remove");
        }
    }

    // Add card to deck
    public void AddCardToCollection(ScriptableCard card)
    {
        // Checks to see if selected card is already in collection
        // Adds card if not already present and prevents adding a duplicate.
        if (CardsInCollection.Contains(card))
        {
            Debug.LogWarning("Card is already in collection - can't add more");
        }
        else
        {
            CardsInCollection.Add(card);
        }
    }
}
