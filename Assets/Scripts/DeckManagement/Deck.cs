using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* Purpose:
 * Represents a card deck and also governs the discard pile and works in concordance with the Hand script.
 * 
 * Writer: Cristian Duque
 * ------------------------------------
 * Scripts borrowing from it:
 * 
 * ------------------------------------
 * Scripts it borrows from:
 */

public class Deck : MonoBehaviour
{
    // Fields and Properties
    public static Deck Instance { get; private set; } // Singleton

    // Now we need a reference to what a deck is, aka what cards it contains -> CardCollection
    // We will work with one deck for now, but you can easily add several choices for the player to pick from
    [SerializeField] private CardCollection playerDeck; // Holds the cards available for selection (code wise)
    [SerializeField] private CardCollection currentDeck; // The deck being built by the user (code wise)
    [SerializeField] private Card cardPrefab; // Our cardPrefab, of which we will make copies with the different CardData
    
    [SerializeField] private GameObject DeckUI; // Physically holds all cards in collection (UI)
    [SerializeField] private GameObject actionButtonsPanel; // Panel with add and remove buttons
    [SerializeField] private Button addButton;
    [SerializeField] private Button removeButton; 

    [SerializeField] private Canvas cardCanvas;

    private Card selectedCard;

    // Now to represent the instantiated Cards
    public List<Card> deckPile = new();
    public List<Card> discardPile = new();

    public List<Card> HandCards { get; private set; } = new();

    // Methods and/or Functions
    public void PopulateDeckUI()
    {
        foreach (var cardData in playerDeck.CardsInCollection)
        {
            Card card = Instantiate(cardPrefab, DeckUI.transform); // Instantiate card as a child of the panel
            card.SetUp(cardData); // Use the SetUp method to assign the card data
        }
    }

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
        InstantiateDeck(); // This fills the player's deck (will need to be an empty one that can be filled)
        PopulateDeckUI(); // This displays all selectable cards for the player

        actionButtonsPanel.SetActive(false); // Make buttons invisible until click a card

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

    // Allows cards to be selected
    public void OnCardSelected(Card card)
    {
        // Show action buttons next to the selected card
        selectedCard = card;
        actionButtonsPanel.SetActive(true);

        // Position the panel near the card
        RectTransform cardRect = card.GetComponent<RectTransform>();
        RectTransform panelRect = actionButtonsPanel.GetComponent<RectTransform>();
        panelRect.position = cardRect.position + new Vector3(0, -100, 0); // Adjust offset as needed
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

            if (deckPile.Count > 0)
            {
                HandCards.Add(deckPile[0]);
                deckPile[0].gameObject.SetActive(true);
                deckPile.RemoveAt(0);
            }
            
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
