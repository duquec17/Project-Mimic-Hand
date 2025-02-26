using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Purpose:
 * Handles basic player Controls. Examples 8 direction movement (Up & down for passing through planes).
 * The key binds that determine which card is used from the hand. 
 * 
 * Writer: Cristian Duque
 * ------------------------------------
 * Scripts borrowing from it:
 * 
 * ------------------------------------
 * Scripts it borrows from:
 * 
 */
public class PlayerControls : MonoBehaviour
{
    // Variable List
    public float MoveSpeed;
    float speedX, speedY;
    Rigidbody2D rb;

    /* Algorithm
     * 1. Assign speed to player
     * 2. Assign keys to directions
     *  a. AWSD for direction buttons
     *  b. UIOPJ for attack buttons
     * 3. 
     */


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        speedX = Input.GetAxisRaw("Horizontal") * MoveSpeed;
        speedY = Input.GetAxisRaw("Vertical") * MoveSpeed;
        rb.velocity = new Vector2(speedX, speedY);
    }
}
