using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasTank : MonoBehaviour
{
    public float explosionRadius = 10f;  // Explosion radius
    public float explosionDamage = 100f; // Damage caused by the explosion
    public ParticleSystem explosionEffect; // Explosion effect
    public AudioClip explosionSound; // Explosion sound

    private bool isExploded = false; // To prevent multiple explosions

    // This method will be called when the gas tank is shot
    public void Explode()
    {
        if (!isExploded)
        {
            isExploded = true;

            // Play explosion effect and sound
            if (explosionEffect != null)
            {
                ParticleSystem effect = Instantiate(explosionEffect, transform.position, Quaternion.identity);
                effect.Play();  // Ensure the particle system starts playing if not set to "Play on Awake"
            }
            if (explosionSound != null)
            {
                AudioSource.PlayClipAtPoint(explosionSound, transform.position);
            }

            // Detect all enemies within the explosion radius
            Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
            foreach (Collider nearbyObject in colliders)
            {
                // Check if the object is an enemy
                Enemy enemy = nearbyObject.GetComponent<Enemy>();
                if (enemy != null)
                {
                    // Apply explosion damage to the enemy
                    enemy.Explosion();
                }
            }

            // Destroy the gas tank object after the explosion
            Destroy(gameObject, 0.1f);  // Optionally delay destruction by 1 second for effect
        }
    }

    // Draw the explosion radius in the scene editor for visualization
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
