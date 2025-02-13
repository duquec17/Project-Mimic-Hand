using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Purpose:
 * Represents a card deck and also governs the discard pile and works in concordance with the Hand script.
 * 
 * Writer: Cristian Duque
 * ------------------------------------
 * Scripts borrowing from it:
 * 
 * ------------------------------------
 * Scripts it borrows from:
 * 
 */
public class Deck : MonoBehaviour
{
    // Fields and Properties
    public static Deck Instance { get; private set; } // Singleton

    // Now we need a reference to what a deck is, aka what cards it contains -> CardCollection

    private void Awake()
    {
        // Typical singleton declaration
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
