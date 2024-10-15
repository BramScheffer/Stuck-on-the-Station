using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireArea : MonoBehaviour
{
  

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object is on the "Enemy" layer
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            // Get the enemy's health component and deal initial damage (assuming an enemy health script exists)
            Enemy enemyHealth = other.GetComponent<Enemy>();
            if (enemyHealth != null)
            {
                // Deal instant damage to the enemy on entering the fire area
                enemyHealth.FireDamage();

                // (Optional) If you want to continuously deal damage, you can start a coroutine here
                // StartCoroutine(DamageOverTime(enemyHealth));
            }
        }
    }

    // Optional: If you want to deal damage over time, you can use this coroutine
    /*
    IEnumerator DamageOverTime(EnemyHealth enemyHealth)
    {
        while (enemyHealth != null) // Continue until the enemy dies or leaves
        {
            enemyHealth.TakeDamage(damageAmount * Time.deltaTime); // Deal damage over time
            yield return null; // Wait until the next frame
        }
    }
    */

    private void OnTriggerExit(Collider other)
    {
        // Optional: Handle when the enemy leaves the fire area
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            // You could stop dealing damage, or remove status effects, etc.
        }
    }
}
