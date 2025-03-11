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
    [SerializeField] private Button startLevelButton; // References existing button in scene


    [SerializeField] private Canvas cardCanvas;

    private Card selectedCard;

    // Now to represent the instantiated Cards
    public List<Card> deckPile = new();
    public List<Card> discardPile = new();

    // Hand variables
    public List<Card> HandCards { get; private set; } = new();
    private bool isGameplayPhase = false;

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
        // If scene is not the deck builder scene than cut short everything
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "DeckBuilder")
        {
            isGameplayPhase = true; // Mark that we are transitioning to gameplay

            // Deactivate UI for deck-building
            DeckUI.SetActive(false);
            actionButtonsPanel.SetActive(false);
            
            // Draw cards for current hand
            DrawHand();

            return ;
        }
        // we will instantiate the deck once, at the start of the game/level
        ClearCurrentDeck();
        InstantiateDeck(); // This fills the player's deck (will need to be an empty one that can be filled)
        PopulateDeckUI(); // This displays all selectable cards for the player

        actionButtonsPanel.SetActive(false); // Make buttons invisible until click a card

        addButton.onClick.AddListener(AddCardToCurrentDeck); // Assigns function to add card button
        removeButton.onClick.AddListener(RemoveCardFromCurrentDeck); // Assigns function to add card button
        startLevelButton.onClick.AddListener(ChangeScene); // Assigns start level button the ability to change scene
    }

    private void ClearCurrentDeck()
    {
        if (currentDeck.CardsInCollection.Count > 0)
        {
            currentDeck.CardsInCollection.Clear();
            Debug.LogWarning("Reset deck");
        }
    }

    // Populates deck with full card collection
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

    // Allows cards to be selected and display the button
    public void OnCardSelected(Card card)
    {
        // Deselect the previous card
        if (selectedCard != null && selectedCard != card)
        {
            // Clear previous selection (optional visual indication logic can go here)
            ClearSelection();
        }

        // Show action buttons next to the selected card
        selectedCard = card;
        Debug.Log($"Card selected: {card.name}");
        actionButtonsPanel.SetActive(true);

        // Position the panel near the card
        RectTransform cardRect = card.GetComponent<RectTransform>();
        RectTransform panelRect = actionButtonsPanel.GetComponent<RectTransform>();

        // Calculate offset (adjust values as needed for your UI design)
        Vector3 offset = new Vector3(0, -100, 0);

        // Update panel position
        panelRect.position = cardRect.position + offset;

        Debug.Log($"Panel position updated to: {panelRect.position}");
    }

    // Hide action buttons
    public void ClearSelection()
    {
        selectedCard = null;
        actionButtonsPanel.SetActive(false);
    }

    // Will add card to empty deck
    public void AddCardToCurrentDeck()
    {
        if (selectedCard == null)
        {
            Debug.LogWarning("No card selected");
            return;
        }

        // Check if deck is full
        if (currentDeck.CardsInCollection.Count >= 15)
        {
            Debug.LogWarning("Full deck, remove card");
            return;
        }
        else
        {
            ScriptableCard selectedCardData = selectedCard.CardData; // Grabs card data from selected card 
            currentDeck.AddCardToCollection(selectedCardData); // Adds card to empty deck

            Debug.Log($"Added {selectedCardData.name} to the deck. Current deck size: {currentDeck.CardsInCollection.Count}");

            if (currentDeck.CardsInCollection.Count == 15)
            {
                EnableStartLevelButton();
            }
        }
    }

    // Function that determins when able to change scenes
    private void EnableStartLevelButton()
    {
        startLevelButton.gameObject.SetActive(true); // Makes button visible
        startLevelButton.interactable = true; // Makes button interactable
        Debug.Log("Scene change ready");
    }

    // Function to change scenes (needs to adjusted for multiple scenes)
    public void ChangeScene()
    {
        isGameplayPhase = true; // Mark that we are transitioning to gameplay

        // Deactivate UI for deck-building
        DeckUI.SetActive(false);
        actionButtonsPanel.SetActive(false);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Training ground");
    }

    // Will add card to empty deck
    public void RemoveCardFromCurrentDeck()
    {
        if (selectedCard == null)
        {
            Debug.LogWarning("No card selected");
            return;
        }

        // Check if deck is empty
        if (currentDeck.CardsInCollection.Count <= 0)
        {
            Debug.LogWarning("Empty deck, add a card");
            return;
        }
        else
        {
            ScriptableCard selectedCardData = selectedCard.CardData; // Grabs card data from selected card 
            currentDeck.RemoveCardFromCollection(selectedCardData); // Removes card to empty deck

            Debug.Log($"Removed {selectedCardData.name} from deck. Current deck size: {currentDeck.CardsInCollection.Count}");
        }
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

    // Needed when insde of the level
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

        // Log the current hand
        if (HandCards.Count > 0)
        {
            string handContents = "Current hand: ";
            foreach (Card card in HandCards)
            {
                handContents += card.name + ", "; // Replace `card.name` with the relevant property of your Card class
            }
            Debug.Log(handContents.TrimEnd(',', ' '));
        }
        else
        {
            Debug.Log("Hand is empty.");
        }
    }

    // Needed when inside of the level
    // We will assume no cards can be discarded directly from the deck to the discard pile
    // otherwise make two methods, one to discard from hand, one from deck
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
