using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Purpose:
 * Define what a card is and can be, while connecting all data and behaviours.
 * 
 * Writer: Cristian Duque
 * 
 * Scripts borrowing from it:
 * Deck Builder.cs
 * 
 * Scripts it borrows from:
 * 
 */

public class Card : MonoBehaviour
{
    // Fields and Properties
    [field: SerializeField] public ScriptableCard CardData { get; private set; }

    // Methods

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
