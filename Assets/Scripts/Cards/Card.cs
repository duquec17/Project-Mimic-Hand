using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/* Purpose:
 * Define what a card is and can be, while connecting all data and behaviours.
 * 
 * Writer: Cristian Duque
 * ------------------------------------
 * Scripts borrowing from it:
 * Deck Builder.cs
 * ------------------------------------
 * Scripts it borrows from:
 * 
 */

[RequireComponent(typeof(CardUI))] // Will automatically attach the CardUI script to every object that is a card
[RequireComponent(typeof(CardMovement))] // Will handle everything to do with perceived card movement

public class Card : MonoBehaviour, IPointerClickHandler
{
    // Fields and Properties
    [field: SerializeField] public ScriptableCard CardData { get; private set; }

    // Methods & Functions

    public void PlayCard(GameObject target)
    {
        if (CardData.cardEffect != null)
        {
            CardData.cardEffect.ApplyEffect(target);
        }
    }

    // Set the relevant card data at runtime and update the card's UI
    public void SetUp(ScriptableCard data)
    {
        CardData = data;
        GetComponent<CardUI>().SetCardUI();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Deck.Instance.OnCardSelected(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
