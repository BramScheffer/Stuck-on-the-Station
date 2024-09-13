
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.GraphicsBuffer;

public class Turret : MonoBehaviour
{
    // Gun settings
    public float fireRate = 10f; // Bullets per second
    public int maxAmmo = 50;
    public float reloadTime = 2f;
    public float range = 100f; // Maximum range of the gun

    public float ammo = 1000;
    public bool empty;
    public Transform raycastOrigin;

    // Effects
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    // Camera or origin from where the ray will be cast
    public Camera fpsCamera;

    // Private variables
    private int currentAmmo;
    private float nextTimeToFire = 0f;
    private bool isReloading = false;

    void Start()
    {
        currentAmmo = maxAmmo;
        ammo = 50000;
        empty = false;
    }

    void Update()
    {
        // Check if currently reloading
        if (isReloading)
            return;

        // Reload when out of ammo and fire button is pressed
        if (currentAmmo <= 0 && Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(Reload());
            return;
        }

        // Fire the gun when the fire button is held down and the time to fire has passed
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            if (!empty)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                Shoot();
            }
        }

        // Update empty flag based on current ammo
        if (ammo <= 0)
        {
            empty = true;
        }
        else
        {
            empty = false;
        }
    }

    void Shoot()
    {
        // Check ammo count
        if (currentAmmo <= 0)
            return;

        ammo -= 1f;
        currentAmmo--;

        // Play muzzle flash effect
        if (muzzleFlash != null)
        {
            muzzleFlash.Play();
        }

        // Perform the raycast
        RaycastHit hit;
        if (Physics.Raycast(raycastOrigin.position, raycastOrigin.forward, out hit, range))
        {
            // Debugging information
            Debug.Log($"Hit: {hit.collider.name} at {hit.point}");

            // Apply damage to the target if it has a health component
            if (hit.collider.CompareTag("Enemy"))
            {
                // Get the Enemy script from the hit object
                Enemy enemy = hit.collider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    // Call the hit method on the enemy script
                    enemy.hit(); // Pass the damage amount as needed
                }
                if (impactEffect != null)
                {
                    GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(impactGO, 2f); // Destroy the impact effect after 2 seconds
                }
            }
        }
        else
        {
            Debug.Log("Raycast did not hit anything.");
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading...");

        // Wait for reload time
        yield return new WaitForSeconds(reloadTime);

        // Reset ammo and state
        currentAmmo = maxAmmo;
        ammo = Mathf.Max(ammo, 0); // Ensure ammo is not negative
        empty = false;
        isReloading = false;
    }
}