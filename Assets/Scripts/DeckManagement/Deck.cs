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
    // We will work with one deck for now, but you can easily add several choices for the player to pick from
    [SerializeField] private CardCollection playerDeck;
    [SerializeField] private Card cardPrefab; // Our cardPrefab, of which we will make copies with the different CardData

    [SerializeField] private Canvas cardCanvas;

    // Now to represent the instantiated Cards
    private List<Card> deckPile;
    private List<Card> discardPile = new();

    public List<Card> HandCards { get; private set; }

    // Methods and/or Functions
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

    private void Start()
    {
        // we will instantiate the deck once, at the start of the game/level
        InstantiateDeck();
    }

    private void InstantiateDeck()
    {
        for (int i = 0; i < playerDeck.CardsInCollection.Count; i++)
        {
            Card card = Instantiate(cardPrefab, cardCanvas.transform); // Instantiates the Card Prefab as a child of the card canvas
            card.SetUp(playerDeck.CardsInCollection[i]);
            deckPile.Add(card); // At the start, all cards in the deck, none in hand, none in discard
            card.gameObject.SetActive(false); // We will later activate the cards when we draw them, for now we just want to build the pool
        }

        ShuffleDeck();
    }

    // Call once at start and whenever deck count hits zero
    private void ShuffleDeck()
    {
        for (int i = deckPile.Count -1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            var temp = deckPile[i];
            deckPile[i] = deckPile[j];
            deckPile[j] = temp;
        }
    }

    public void DrawHand(int amount = 5)
    {
        for (int i = 0; i < amount; i++)
        {
            if (deckPile.Count <= 0)
            {
                discardPile = deckPile;
                discardPile.Clear();
                ShuffleDeck();
            }

            HandCards.Add(deckPile[0]);
            deckPile[0].gameObject.SetActive(true);
            discardPile.RemoveAt(0);
        }
    }

    // We will assume no cards can be discarded directly from the deck to the discard pile
    // otherwise mate two methods, one to discard from hand, one from deck
    // TODO: Adjust from generalized design to specifics
    public void DiscardCard(Card card)
    {
        if (HandCards.Contains(card))
        {
            HandCards.Remove(card);
            discardPile.Add(card);
            card.gameObject.SetActive(false);
        }
    }
}
