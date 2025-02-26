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
    public float moveSpeed;
    public float jumpForce;
    
    public LayerMask groundLayer;
    public Transform groundPosition;
    
    private Rigidbody2D rb;
    private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Checks if is on the ground
        isGrounded = Physics2D.OverlapCircle(groundPosition.position, 0.1f, groundLayer);
        
        float moveDirection = Input.GetAxisRaw("Horizontal"); // Get horizontal movement input
        rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y); // Apply horizontal velocity directly

        // Handles jumping
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}
