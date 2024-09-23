using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f; // The maximum health value
    private float currentHealth; // The current health

    private void Start()
    {
        currentHealth = maxHealth; // Initialize current health
    }

    // Method to reduce health
    public void TakeDamage(float amount)
    {
        currentHealth -= amount; // Reduce the current health by the damage amount
        Debug.Log($"{gameObject.name} took {amount} damage, remaining health: {currentHealth}");

        // Check if the object should be destroyed
        if (currentHealth <= 0f)
        {
            Die(); // Trigger the death process
        }
    }

    // Check if the object is dead
    public bool IsDead()
    {
        return currentHealth <= 0;
    }

    // Handle death (e.g., destroy the game object)
    void Die()
    {
        Debug.Log($"{gameObject.name} has been destroyed!");

        // If you want to play an animation or sound before destruction, trigger it here

        // Destroy the game object
        Destroy(gameObject); // You can replace this with deactivation if needed, like SetActive(false)
    }
}
