using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Purpose:
 * Designed to maintain variable management of the various health systems for enemies
 * 
 * Writer: Cristian Duque
 * ------------------------------------
 * Scripts borrowing from it:
 * 
 * ------------------------------------
 * Scripts it borrows from:
 * 
 */
public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth = 3f;

    private float currentHealth;
    
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }
    
    // Changes health based on damage dealt
    public void Damage(float damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Die();
        }
     }

    
    private void Die()
    {
        Destroy(gameObject);
    }
}
