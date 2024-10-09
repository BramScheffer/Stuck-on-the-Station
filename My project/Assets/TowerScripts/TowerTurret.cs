using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerTurret : MonoBehaviour
{
    public Transform turret;
    public Transform rotatingGunHolder;
    public float rotationSmooth;
    public Transform enemy;
    public FindEnemy find;
    public GameObject projectilePrefab; // Projectile to shoot
    public Transform firePoint; // The position where the projectile is fired from
    public float fireCooldown = 2f; // Time between shots
    public float projectileSpeed = 20f; // Speed of the projectile
    private float nextFireTime = 0f; // Time until the next shot is allowed

    // New fields for particle and sound
    public ParticleSystem muzzleFlash; // Particle system for firing
    public AudioSource fireSound; // Audio source for the firing sound

    // Update is called once per frame
    void Update()
    {
        enemy = find.FindEnem();

        if (enemy != null)
        {
            RotateGunHolder();

            // Shoot at the enemy if cooldown allows
            if (Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + fireCooldown; // Reset the cooldown
            }
        }
    }

    void RotateGunHolder()
    {
        Vector3 direction = enemy.position - turret.position;
        direction.y = 0;
        Quaternion rotation = Quaternion.LookRotation(direction);
        rotatingGunHolder.rotation = Quaternion.Slerp(rotatingGunHolder.rotation, rotation, rotationSmooth);
    }

    void Shoot()
    {
        // Instantiate the projectile at the firePoint's position and rotation
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        // Get the Rigidbody component of the projectile to apply force
        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        // If the projectile has a Rigidbody, add force to make it move towards the enemy
        if (rb != null)
        {
            // Calculate the direction towards the enemy
            Vector3 shootDirection = (enemy.position - firePoint.position).normalized;
            rb.velocity = shootDirection * projectileSpeed; // Apply velocity to the projectile
        }

        // Optionally, you can destroy the projectile after a certain time
        Destroy(projectile, 7f); // Destroy the projectile after 7 seconds to clean up

        // Play the muzzle flash particle effect
        if (muzzleFlash != null)
        {
            muzzleFlash.Play(); // Play the muzzle flash particle system at the fire point
        }

        // Play the firing sound
        if (fireSound != null)
        {
            fireSound.Play(); // Trigger the sound effect
        }
    }
}
