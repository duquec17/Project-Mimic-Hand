using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    [SerializeField] private TextMeshProUGUI deckCounterText; // References the counter in the deck builder scene

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
        UpdateDeckCounter(); // This tells how many cards are currently in the deck
        InstantiateDeck(); // This fills the player's deck (will need to be an empty one that can be filled)
        PopulateDeckUI(); // This displays all selectable cards for the player

        actionButtonsPanel.SetActive(false); // Make buttons invisible until click a card

        addButton.onClick.AddListener(AddCardToCurrentDeck); // Assigns function to add card button
        removeButton.onClick.AddListener(RemoveCardFromCurrentDeck); // Assigns function to add card button
        startLevelButton.onClick.AddListener(ChangeScene); // Assigns start level button the ability to change scene
    }

    private void UpdateDeckCounter()
    {
        // Deck Counter variables
        int deckSize = currentDeck.CardsInCollection.Count;
        string message = $"Click on cards to add them until 15 non-duplicates have been selected\n Deck: {deckSize}";

        deckCounterText.text = message; // Update the text to show the current deck size
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
        // If it's in the gameplay phase, do nothing when a card is clicked
        if (isGameplayPhase)
        {
            return; // Exit early
        }

        // Deselect the previous card
        if (selectedCard != null && selectedCard != card)
        {
            // Clear previous selection (optional visual indication logic can go here)
            ClearSelection();
        }

        // Show action buttons next to the selected card
        selectedCard = card;
        // TBR: Debug.Log($"Card selected: {card.name}");
        actionButtonsPanel.SetActive(true);

        // Position the panel near the card
        RectTransform cardRect = card.GetComponent<RectTransform>();
        RectTransform panelRect = actionButtonsPanel.GetComponent<RectTransform>();

        // Calculate offset (adjust values as needed for your UI design)
        Vector3 offset = new Vector3(0, -100, 0);

        // Update panel position
        panelRect.position = cardRect.position + offset;

        // TBR: Debug.Log($"Panel position updated to: {panelRect.position}");
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
            UpdateDeckCounter();
            //Debug.Log($"Added {selectedCardData.name} to the deck. Current deck size: {currentDeck.CardsInCollection.Count}");

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
            UpdateDeckCounter();
            //Debug.Log($"Removed {selectedCardData.name} from deck. Current deck size: {currentDeck.CardsInCollection.Count}");
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
    [SerializeField] private GameObject handContainer; // The container holding the hand of cards
    [SerializeField] private HorizontalLayoutGroup handLayoutGroup; // The layout group for the cards
    public void DrawHand(int amount = 5)
    {
        // Populate the deckPile from currentDeck if it is not already populated
        if (deckPile.Count == 0 && currentDeck.CardsInCollection.Count > 0)
        {
            foreach (var cardData in currentDeck.CardsInCollection)
            {
                Card card = Instantiate(cardPrefab, cardCanvas.transform);
                card.SetUp(cardData);
                card.gameObject.SetActive(false); // Cards start inactive until drawn
                deckPile.Add(card);
            }

            ShuffleDeck();
        }

        // Clear the current hand before drawing new cards
        HandCards.Clear();

        for (int i = 0; i < amount; i++)
        {
            if (deckPile.Count <= 0)
            {
                if (discardPile.Count > 0)
                {
                    deckPile.AddRange(discardPile);
                    discardPile.Clear();
                    ShuffleDeck();
                }
                else
                {
                    Debug.LogWarning("No cards left to draw!");
                    break;
                }
            }

            if (deckPile.Count > 0)
            {
                Card drawnCard = deckPile[0];
                HandCards.Add(drawnCard);
                drawnCard.gameObject.SetActive(true);
                drawnCard.transform.SetParent(handContainer.transform, false); // Set the card's parent to the hand container
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
        }
        else
        {
            Debug.Log("Hand is empty.");
        }

        // Position the hand container at the bottom right of the screen
        RectTransform rectTransform = handContainer.GetComponent<RectTransform>();
    }

    // Needed when inside of the level
    // We will assume no cards can be discarded directly from the deck to the discard pile
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
