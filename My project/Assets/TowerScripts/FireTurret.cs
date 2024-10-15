using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTurret : MonoBehaviour
{
    public Transform turret;
    public Transform rotatingGunHolder;
    public float rotationSmooth;
    public Transform enemy;
    public FindEnemy find;
    public GameObject fireAreaPrefab; // Fire area prefab that deals damage
    public Transform firePoint; // The position where the fire area spawns
    public float fireCooldown = 2f; // Time between fire spawns
    private float nextFireTime = 0f; // Time until the next fire spawn is allowed
    public float fireDuration = 5f; // How long the fire lasts before disappearing
    public float damagePerSecond = 10f; // Damage dealt to enemies in the fire

    // New fields for particle and sound
    public ParticleSystem fireSpawnEffect; // Particle system for fire spawning
    public AudioSource fireSound; // Audio source for the fire sound

    // Update is called once per frame
    void Update()
    {
        enemy = find.FindEnem();

        if (enemy != null)
        {
            RotateGunHolder();

            // Spawn fire if cooldown allows
            if (Time.time >= nextFireTime)
            {
                SpawnFire();
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

    void SpawnFire()
    {
        // Instantiate the fire area at the firePoint's position and rotation
        GameObject fireArea = Instantiate(fireAreaPrefab, firePoint.position, firePoint.rotation);

        // Optionally destroy the fire area after a certain time (fireDuration)
        Destroy(fireArea, fireDuration);

        // Play the fire spawn particle effect
        if (fireSpawnEffect != null)
        {
            fireSpawnEffect.Play(); // Play the fire particle system at the fire point
        }

        // Play the fire spawning sound
        if (fireSound != null)
        {
            fireSound.Play(); // Trigger the fire spawn sound effect
        }
    }
}
