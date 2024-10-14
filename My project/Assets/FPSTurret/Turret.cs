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
                muzzleFlash.Stop();
                muzzleFlash.Clear();
                muzzleFlash.Play();
            }

            // Perform the raycast
            RaycastHit hit;
            if (Physics.Raycast(raycastOrigin.position, raycastOrigin.forward, out hit, range))
            {
                Debug.Log($"Hit: {hit.collider.name} at {hit.point}");

                // Check if the hit object is a gas tank
                GasTank gasTank = hit.collider.GetComponent<GasTank>();
                if (gasTank != null)
                {
                    gasTank.Explode();  // Trigger explosion on gas tank
                }

                // Apply damage to enemies
                if (hit.collider.CompareTag("Enemy"))
                {
                    Enemy enemy = hit.collider.GetComponent<Enemy>();
                    if (enemy != null)
                    {
                        enemy.hit();  // Call hit method on the enemy script
                    }
                    if (impactEffect != null)
                    {
                        GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                        Destroy(impactGO, 2f);
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
