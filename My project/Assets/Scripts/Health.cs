using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f; // The maximum health
    private float currentHealth; // The current health

    public GameObject deathEffect; // Optional: Prefab for death effect (e.g., particles)

    private void Start()
    {
        currentHealth = maxHealth; // Initialize current health
        Debug.Log($"{gameObject.name} initialized with {currentHealth} health.");
    }

    // Method to apply damage
    public void BrengSchadeToe(float amount)
    {
        currentHealth -= amount; // Reduce current health
        Debug.Log($"{gameObject.name} has taken {amount} damage, remaining health: {currentHealth}");

        if (IsDead())
        {
            Sterf(); // Trigger death process
        }
    }

    // Check if the object is dead
    public bool IsDead()
    {
        return currentHealth <= 0; // Check if dead
    }

    // Handle the death (e.g., destroying the object)
    void Sterf()
    {
        Debug.Log($"{gameObject.name} is destroyed!");

        // Instantiate a death effect if desired
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }

        Destroy(gameObject); // Destroy the object
    }
}
