using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Purpose: A general script for boss enemies, but is also being more tailed for a specific MVP/prototype boss
public class BossEnemyAI : EnemyAI
{
    [SerializeField] private GameObject projectilePrefab; // Variable that holds projectile that is assigned in editor
    [SerializeField] private Transform firePoint; // Position where projectile spawns
    [SerializeField] private float projectileSpeed;
    
    [SerializeField] private GameObject spikePrefab; // Variable that holds the spike
   

    // Override start to ensure boss is in correct state
    protected override void Start()
    {
        base.Start();
        SetState(new BossAttackState());
    }

    // Boss attack functions
    public void FireProjectile()
    {
        // Checks for assigned materials
        if (projectilePrefab == null || firePoint == null)
        {
            Debug.LogError("Projectile prefab or fire point not assigned!");
            return;
        }

        // Spawn projectiles at Bosses fire point (hands)
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            Vector3 direction = (Player.position - firePoint.position).normalized;
            rb.velocity = direction * projectileSpeed;
        }
    }

    public void SummonSpikes()
    {
        // Checks for assigned materials
        if (spikePrefab == null || Player == null)
        {
            Debug.LogError("Spike prefab or spike point not assigned!");
            return;
        }

        // Spawns spike at the player location (feet of the player) when they are on the ground, but it is in standby phase
        Vector3 spikePoint = new Vector3(Player.position.x, Player.position.y, Player.position.z);
        GameObject spike = Instantiate(spikePrefab, spikePoint, Quaternion.identity);
    }
}
