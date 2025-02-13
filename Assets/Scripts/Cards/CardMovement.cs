using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/* Purpose:
 * Handles their movement when zoomed out, etc.
 * NOTE: Can be used for drag & drop of cards, but tying this to buttons is first design choice to make it easier
 * 
 * Writer: Cristian Duque
 * ------------------------------------
 * Scripts borrowing from it:
 * 
 * ------------------------------------
 * Scripts it borrows from:
 * 
 */

public class CardMovement : MonoBehaviour
{
    // Fields and Properties
    private Canvas cardCanvas; // We need to get this at runtime, assigning in the inspector won't work
    private RectTransform rectTransform;

    private readonly string CANVAS_TAG = "CardCanvas";

    // Methods and/or Functions
    private void Start()
    {
        cardCanvas = GameObject.FindGameObjectWithTag(CANVAS_TAG).GetComponent<Canvas>();
        rectTransform = GetComponent<RectTransform>();
    }
}
