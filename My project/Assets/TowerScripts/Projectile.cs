using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the object we collided with has the "Enemy" tag
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Print "hit" to the console when the projectile hits an enemy
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.turretHit(); // Pass the damage amount to the OnHit method
            }
            // Optional: You can destroy the projectile on hit if you want
            Destroy(gameObject);
        }
    }
}
