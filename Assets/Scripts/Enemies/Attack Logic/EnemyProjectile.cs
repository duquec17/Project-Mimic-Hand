using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private float damage = 10f;
    [SerializeField] private float lifeTime = 5f;

    void Start()
    {
        Destroy(gameObject, lifeTime); // Prevent infinite projectiles
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent<ReDoHealth>(out ReDoHealth playerHealth))
            {
                playerHealth.Damage(damage);
            }
            Destroy(gameObject); // Destroy on impact
        }
    }
}
