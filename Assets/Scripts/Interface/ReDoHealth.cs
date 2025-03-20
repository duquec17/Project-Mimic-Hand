using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReDoHealth : MonoBehaviour, IDamageable
{
    // Character stat variables
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;
    //private float armor = 1f;
    private float damageReduction = 1f; // Default: no damage reduction

    // List of Status effects
    private List<StatusEffect> activeStatusEffects = new List<StatusEffect>();

    void Start()
    {
        currentHealth = maxHealth;
    }

    public float MaxHealth => maxHealth; // Public getter for maxHealth
    public float CurrentHealth => currentHealth; // Public getter for currentHealth

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

    public void ApplyStatusEffect(StatusEffect statusEffect)
    {
        // Add status effect to the list and initialize it
        var instance = Instantiate(statusEffect);
        activeStatusEffects.Add(instance);
        instance.ApplyEffect(this);
    }

    public void RemoveStatusEffect(StatusEffect statusEffect)
    {
        // Remove status effect and clean up if necessary
        activeStatusEffects.Remove(statusEffect);
        statusEffect.RemoveEffect(this);
    }

    public void Update()
    {
        for (int i = activeStatusEffects.Count - 1; i >= 0; i--)
        {
            var effect = activeStatusEffects[i];
            effect.TickEffect(this, Time.deltaTime);

            if (effect.duration <= 0)
            {
                RemoveStatusEffect(effect);
            }
        }
    }

    private void Die()
    {
        Debug.Log(gameObject.name + " has died.");
        Destroy(gameObject);
    }
}
