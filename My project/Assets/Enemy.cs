using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hits;
    public float health;
    public bool giant;
    public ParticleSystem killed; // ParticleSystem to play on death
    public GameObject particleEffectPrefab;
    public AudioClip killsund;
    public Money mn;
    public SpawnerScript sSC;

    // Start is called before the first frame update
    void Start()
    {
        if (giant)
        {
            health = 50; // If giant, health is 50
        }
        else
        {
            health = 15; // If not giant, health is 15
        }
    }

    public void hit()
    {
        hits += 1;
        health -= 2;

        if (health <= 0)
        {
            Destroy(gameObject);
            SpawnEffect(); // Now the method is accessible
            AudioSource.PlayClipAtPoint(killsund, transform.position);
            if (giant)
            {
                mn.BigZombie();
            }
            else
            {
                mn.SmallZombie();
                sSC.DecrementCountdown();
            }
        }
    }

    public void Explosion()
    {
        health -= 100;

        if (health <= 0)
        {
            Destroy(gameObject);
            SpawnEffect(); // You can now call it from here as well
            AudioSource.PlayClipAtPoint(killsund, transform.position);
            if (giant)
            {
                mn.BigZombie();
            }
            else
            {
                mn.SmallZombie();
                sSC.DecrementCountdown();
            }
        }
    }

    // Method to spawn particle effect
    private void SpawnEffect()
    {
        // Instantiate the particle effect at the position and rotation of the GameObject
        GameObject impactGO = Instantiate(particleEffectPrefab, transform.position, transform.rotation);

        // Optionally destroy the instantiated effect after some time (e.g., 2 seconds)
        Destroy(impactGO, 2f);
    }

    public void turretHit()
    {
        hits += 1;
        health -= 2;

        if (health <= 0)
        {
            Destroy(gameObject);
            SpawnEffect();  // Zorg dat SpawnEffect correct is
            AudioSource.PlayClipAtPoint(killsund, transform.position);

            if (giant)
            {
                mn.BigZombie();
            }
            else
            {
                mn.SmallZombie();
                sSC.DecrementCountdown();
            }
        }
    }
    public void FireDamage()
    {
        health -= 100;
    }

}
