using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.GraphicsBuffer;
using TMPro;

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
    public AudioClip shotsound;
    public CameraSwitch cam;
    public TMP_Text ammoCount;

    void Start()
    {
        currentAmmo = maxAmmo;
        ammo = 50000;
        empty = false;
    }

    void Update()
    {
        ammoCount.text = string.Format("{0}/50", currentAmmo);

        // Check if currently reloading
        if (isReloading)
            return;

        // Reload when out of ammo and fire button is pressed
        if (currentAmmo <= 0 && Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(Reload());
            return;
        }

        // Reload when "R" key is pressed and not already reloading
        if (Input.GetKeyDown(KeyCode.R) && !isReloading && currentAmmo < maxAmmo)
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
        if (!cam.inBuy)
        {
            if (currentAmmo <= 0)
                return;

            ammo -= 1f;
            currentAmmo--;
            AudioSource.PlayClipAtPoint(shotsound, transform.position);

            // Play muzzle flash effect
            if (muzzleFlash != null)
            {
                Debug.Log("Playing Muzzle Flash"); // Debugging log
                muzzleFlash.Stop();    // Ensure the particle system is stopped
                muzzleFlash.Clear();   // Clear any remaining particles
                muzzleFlash.Play();    // Now play the particle system again
            }
            else
            {
                Debug.LogWarning("Muzzle Flash is not assigned!");
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
    }

    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading...");

        // Wait for reload time
        yield return new WaitForSeconds(reloadTime);

        // Reset ammo and state
        currentAmmo = maxAmmo;
        isReloading = false;
        Debug.Log("Reload complete!");
    }
}
