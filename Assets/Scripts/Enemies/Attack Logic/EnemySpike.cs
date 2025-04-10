using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpike : MonoBehaviour
{
    // Spike variable list
    [SerializeField] private float warningDuration = 1f; // Time before spike activates
    [SerializeField] private float activeDuration = 1f; // Time spike is active
    [SerializeField] private float damage = 15f;

    [SerializeField] private Sprite warningSprite; // For color change might remove
    [SerializeField] private Sprite activeSprite;
    private SpriteRenderer spriteRenderer;

    private bool isActive = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpikeBehavior: Missing SpriteRenderer component!");
            return;
        }
        StartCoroutine(ActivateSpike());
    }

    private IEnumerator ActivateSpike()
    {
        // Set warning sprite
        spriteRenderer.sprite = warningSprite;

        // Wait for warning duration
        yield return new WaitForSeconds(warningDuration);

        // Activate spike and change sprite
        isActive = true;
        spriteRenderer.sprite = activeSprite;

        // Wait for active duration
        yield return new WaitForSeconds(activeDuration);

        // Destory spike after it is active
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isActive && other.CompareTag("Player"))
        {
            if (other.TryGetComponent<ReDoHealth> (out var playerHealth))
            {
                playerHealth.Damage(damage);
            }
        }
    }
}
