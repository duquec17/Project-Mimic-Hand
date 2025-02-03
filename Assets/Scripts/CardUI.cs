using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/* Purpose:
 * Will Update the UI-visuals of each card, depending on it's data
 * 
 * Writer: Cristian Duque
 * 
 * Scripts borrowing from it:
 * 
 * Scripts it borrows from:
 * Card.cs 
 */

public class CardUI : MonoBehaviour
{
    // Fields & Properties
    private Card card;

    [Header("Prefab Elements")] // References to objects in the card prefab
    [SerializeField] private Image cardImage;
    [SerializeField] private Image cardBackground;

    [SerializeField] private TextMeshProUGUI cardName;
    [SerializeField] private TextMeshProUGUI cardDesc;
    [SerializeField] private TextMeshProUGUI cardType;

    [Header("Sprite Assets")] // References to the art folder in assets
    [SerializeField] private Sprite baseCard;
    [SerializeField] private Sprite buffCard;
    [SerializeField] private Sprite debuffCard;
    [SerializeField] private Sprite wildCard;

    // Methods
}
