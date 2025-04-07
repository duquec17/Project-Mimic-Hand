using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpike : MonoBehaviour
{
    // Spike variable list
    [SerializeField] private float warningDuration = 1f; // Time before spike activates
    [SerializeField] private float activeDuration = 1f; // Time spike is active
    [SerializeField] private float damage = 15f;
    [SerializeField] private SpriteRenderer spriteRenderer; // For color change might remove

    private bool isActive = false;
}
