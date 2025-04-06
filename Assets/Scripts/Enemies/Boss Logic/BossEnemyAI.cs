using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyAI : EnemyAI
{
    [SerializeField] private GameObject projectilePrefab; // Variable that holds projectile that is assigned in editor
    [SerializeField] private Transform firePoint; // Position where projectile spawns
    [SerializeField] private float projectileSpeed;

    // Override start to ensure boss is in correct state
    protected override void Start()
    {
        base.Start();
        SetState(new BossAttackState());
    }

    // Boss attack functions
    public void FireProjectile()
    {
        if (projectilePrefab == null || firePoint == null)
        {
            Debug.LogError("Projectile prefab or fire point not assigned!");
            return;
        }

        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            Vector3 direction = (Player.position - firePoint.position).normalized;
            rb.velocity = direction * projectileSpeed;
        }
    }

    public void summonHazard()
    {

    }


}
