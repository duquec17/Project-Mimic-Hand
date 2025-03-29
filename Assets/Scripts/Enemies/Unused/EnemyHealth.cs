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
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;
    private float damageReduction = 1f; // Default: no damage reduction

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void SetDamageReduction(float reductionAmount)
    {
        damageReduction = reductionAmount; // Set the new damage reduction multiplier
    }

    public void Damage(float damageAmount)
    {
        damageAmount *= damageReduction; // Apply damage reduction

        currentHealth -= damageAmount;
        Debug.Log("HP is: " + currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void ApplyBurn(float burnDamagePercent, float burnDuration)
    {
        Debug.Log($"ApplyBurn called on: {gameObject.name} with {burnDamagePercent * 100}% burn for {burnDuration} seconds.");
        StartCoroutine(BurnCoroutine(burnDamagePercent, burnDuration));
    }

    private IEnumerator BurnCoroutine(float burnDamagePercent, float burnDuration)
    {
        Debug.Log("Burn Start on: " + gameObject.name);
        float timeElapsed = 0f;

        while (timeElapsed < burnDuration)
        {
            float burnDamage = maxHealth * burnDamagePercent; // Calculate damage based on max health
            Damage(burnDamage);
            Debug.Log($"Burn tick: {burnDamage} damage applied.");
            timeElapsed += 1f; // Wait for 1 second
            yield return new WaitForSeconds(1f);
        }

        Debug.Log("Burn End" + gameObject.name);
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
