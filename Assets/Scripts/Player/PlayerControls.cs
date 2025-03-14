using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Purpose:
 * Handles basic player Controls. Examples 8 direction movement (Up & down for passing through planes).
 * The key binds that determine which card is used from the hand. Checks for when specific key is pressed
 * then should play connected attack animation with card effect. 
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
    // Player Variable List
    public float moveSpeed;
    public float jumpForce;
    
    public LayerMask groundLayer;
    public Transform groundPosition;
    
    private Rigidbody2D rb;
    private bool isGrounded;

    [SerializeField] private Transform attackTransform;
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private LayerMask attackableLayer;
    [SerializeField] private float damageAmount = 1f;
    private RaycastHit2D[] hits;

    [SerializeField] private Deck deck;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (deck == null)
        {
            Debug.LogError("No assigned deck");
        }
        else
        {
            Debug.Log("Deck name: " + deck.name);
        }
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

        AttackControls();
    }

    private void AttackControls()
    {
        // Loop iterating to see each attack input key UIOPJ
        foreach (KeyCode key in new[] { KeyCode.U, KeyCode.I, KeyCode.O, KeyCode.P, KeyCode.J })
        {
            if (Input.GetKeyDown(key))
            {
                int cardIndex = GetCardIndexFromKey(key);
                if (cardIndex != -1)
                {
                    Attack(cardIndex);
                }
            }
        }

    }

    private int GetCardIndexFromKey(KeyCode key)
    {
        switch (key)
        {
            case KeyCode.U:
                return 0; // Leftmost card
            case KeyCode.I:
                return 1; // 2nd Leftmost card
            case KeyCode.O:
                return 2; // Middle card
            case KeyCode.P:
                return 3; // 2nd Rightmost card
            case KeyCode.J:
                return 4; // Rightmost card
            default:
                return -1; // Invald attack key press
        }
    }

    private void Attack(int cardIndex)
    {
        // Creates a circle hit box that checks from enemies inside of it
        hits = Physics2D.CircleCastAll(attackTransform.position, attackRange, transform.right, 0f, attackableLayer);
        
        for (int i = 0; i < hits.Length; i++)
        {
            IDamageable iDamageable = hits[i].collider.gameObject.GetComponent<IDamageable>();

            if (iDamageable != null)
            {
                // Apply damage
                iDamageable.Damage(damageAmount);
            }
        }


    }

    // uses the 2nd card in the hand which is tied to pressing the J key when in the level
    private void JAttack()
    {
        // 
        hits = Physics2D.CircleCastAll(attackTransform.position, attackRange, transform.right, 0f, attackableLayer);

        for (int i = 0; i < hits.Length; i++)
        {
            IDamageable iDamageable = hits[i].collider.gameObject.GetComponent<IDamageable>();

            if (iDamageable != null)
            {
                // Apply damage
                iDamageable.Damage(damageAmount);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackTransform.position, attackRange);
    }
}
