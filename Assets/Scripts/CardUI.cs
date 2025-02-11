using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/* Purpose:
 * Will Update the UI-visuals of each card, depending on it's data
 * 
 * Writer: Cristian Duque
 * --------------------------------
 * Scripts borrowing from it:
 * 
 * --------------------------------
 * Scripts it borrows from:
 * Card.cs 
 */

public class CardUI : MonoBehaviour
{
    // Fields & Properties //
    private Card card;

    [Header("Prefab Elements")] // References to objects in the card prefab
    [SerializeField] private Image cardImage;
    [SerializeField] private Image cardBackground;

    [SerializeField] private TextMeshProUGUI cardName;
    [SerializeField] private TextMeshProUGUI cardDescription;
    [SerializeField] private TextMeshProUGUI cardType;

    [Header("Sprite Assets")] // Card Types
                              // References to the art folder in assets
    [SerializeField] private Sprite baseCard;
    [SerializeField] private Sprite buffCard;
    [SerializeField] private Sprite debuffCard;
    [SerializeField] private Sprite wildCard;

    private readonly string base_CD = "DMG";
    private readonly string debuff_CD = "DEBUFF";
    private readonly string buff_CD = "BUFF";
    private readonly string wild_CD = "WILD";

    // End of Fields & Properties. //


    // Methods //

    // Calls Awake every time the inspector/editor gets refreshed
    // lets you see changes also in editor (no need to start game)
    private void Awake()
    {
        card = GetComponent<Card>();
        SetCardUI();
    }

    private void OnValidate()
    {
        Awake();
    }

    private void SetCardUI()
    {
        if (card != null && card.CardData != null)
        {
            SetCardTexts();
            SetCardBackground();
            SetCardImage();
        }
    }

    private void SetCardTexts()
    {
        // We need to know the card name, description, and type
        SetCardEffectType();

        cardName.text = card.CardData.CardName;
        cardDescription.text = card.CardData.CardDescription;
        cardType.text = card.CardData.CardType;
    }

    private void SetCardEffectType()
    {
        // Adjust card type text to match with 1 of 4 options
        switch (card.CardData.EffectType)
        {
            case CardEffectType.Base:
                cardType.text = base_CD;
                break;
            
            case CardEffectType.Buff:
                cardType.text = buff_CD;
                break;
            
            case CardEffectType.Debuff:
                cardType.text = debuff_CD;
                break;
            
            case CardEffectType.Wild:
                cardType.text = wild_CD;
                break;
        }
    }

    private void SetCardBackground ()
    {
        switch (card.CardData.CardBackgrounds)
        {
            case CardBackgrounds.Base:
                cardBackground.sprite = baseCard;
            break;

            case CardBackgrounds.Buff:
                cardBackground.sprite = buffCard;
            break;

            case CardBackgrounds.Debuff:
                cardBackground.sprite = debuffCard;
                break;

            case CardBackgrounds.Wild:
                cardBackground.sprite = wildCard;
                break;
        }
    }

    private void SetCardImage()
    {
        cardImage.sprite = card.CardData.Image;
    }

    // End of Methods //
}
