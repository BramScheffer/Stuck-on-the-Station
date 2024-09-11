
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
    public int maxAmmo = 30;
    public float reloadTime = 2f;
    public float range = 100f; // Maximum range of the gun
    public float damage = 10f; // Damage dealt to targets
    public Enemy enemySc;
    public float ammo;
    public bool empty;

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
        ammo = 50;
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
        if (ammo <= 0)
        {
            empty = true;
        }
        print(ammo);
    }

    void Shoot()
    {
        // Check ammo count
        if (currentAmmo <= 0)
            return;
        ammo -= 1f;

        // Play muzzle flash effect
        if (muzzleFlash != null)
        {
            muzzleFlash.Play();
        }

        // Perform the raycast
        RaycastHit hit;
        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
        {
            // Debug to see what the ray hit
            Debug.Log(hit.transform.name);

            // Apply damage to the target if it has a health component
           
            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                print("1");
                enemySc.hit();
                
            }

            // Instantiate impact effect at the point of impact
            if (impactEffect != null)
            {
                GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 2f); // Destroy the impact effect after 2 seconds
            }
        }

        // Decrement ammo
        currentAmmo--;
    }

    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading...");

        // Wait for reload time
        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;
        isReloading = false;
    }
}