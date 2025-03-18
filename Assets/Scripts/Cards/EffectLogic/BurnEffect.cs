using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BurnEffect", menuName = "CardEffects/BurnEffect")]
public class BurnEffect : CardEffects
{
    // Effect Variables
    public float burnDamagePercent = 0.01f; // 1% of max HP every second
    public float burnDuration = 5f; // Burn duration in seconds
    public override void ApplyEffect(GameObject target)
    {
        // Checks to see if hit target has the script enemy health
        EnemyHealth enemyHealth = target.GetComponent<EnemyHealth>();
        DummyHealth dummyHealth = target.GetComponent<DummyHealth>(); // TODO: Remove after checking that this works for dummy / adjust dummy to use enemy health & be a prefab

        if (enemyHealth != null)
        {
            enemyHealth.StartCoroutine(ApplyBurn(enemyHealth));
        }
    }

    private IEnumerator ApplyBurn(EnemyHealth enemyHealth)
    {
        float timeElapsed = 0f;

        while (timeElapsed < burnDuration)
        {
            enemyHealth.Damage(burnDamagePercent); // Apply burn damage
            timeElapsed += 1f; // Wait for 1 second
            yield return new WaitForSeconds(1f);
        }
    }
}
