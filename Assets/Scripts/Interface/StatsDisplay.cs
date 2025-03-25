using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public abstract class StatsDisplay : MonoBehaviour
{
    [SerializeField] protected ReDoHealth targetHealth; // The health component
    [SerializeField] protected TextMeshProUGUI statsText; // The textmesh pro object

    protected virtual void Update()
    {
        if(targetHealth == null || statsText == null)
        {
            Debug.Log("Failed to assign stats or target dead");
            return ;
        }

        // Retrieve stats and effects for display
        string stats = GetStats();
        string effects = GetActiveEffects();

        // Update text continuously with stats
        statsText.text = $"{stats}\n{effects}";
    }

    protected string GetStats()
    {
        return $"HP: {targetHealth.CurrentHealth}/{targetHealth.MaxHealth}";
    }

    protected string GetActiveEffects()
    {
        List<StatusEffect> effects = targetHealth.GetActiveEffects();
        if (effects.Count == 0) return "No active effects";

        string effectsText = "Active effects:\n";
        foreach (var effect in effects)
        {
            effectsText += $"- {effect.name} ({effect.duration:F1}s)\n";
        }

        return effectsText;
    }


}
