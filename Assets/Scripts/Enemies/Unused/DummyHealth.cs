using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth = 100f;

    private float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Changes health based on damage dealt
    public void Damage(float damageAmount)
    {
        // Decreases HP basses on damage dealt
        currentHealth -= damageAmount;
        Debug.Log("HP is: " + currentHealth);

        // Destroy enemy when its health is all gone
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
