using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public int hits;
    public int health;
    public bool giant;
    public ParticleSystem killed; // ParticleSystem to play on death
    public GameObject particleEffectPrefab;

    // Start is called before the first frame update
    void Start()
    {
        // Corrected the condition to check if the giant is true or false
        if (giant)
        {
            health = 50; // If giant, health is 50
        }
        else
        {
            health = 15; // If not giant, health is 10
        }
    }

    // Update is called once per frame
    void Update()
    {
        print(health); // Prints health to the console
    }

    public void hit()
    {
        hits += 1;
        health -= 2;

        {
            if (health <= 0)
            {
                Destroy(gameObject);
                // Instantiate the ParticleSystem at the enemy's position and rotation
                SpawnEffect();


                // Instantiate and play the particle system

            }
        }
        void SpawnEffect()
        {
            // Instantiate the particle effect at the position and rotation of the GameObject this script is attached to
            GameObject impactGO = Instantiate(particleEffectPrefab, transform.position, transform.rotation);

            // Optionally destroy the instantiated effect after some time (e.g., 2 seconds)
            Destroy(impactGO, 2f);
        }
    }
}
