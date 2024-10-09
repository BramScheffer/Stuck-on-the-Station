using UnityEngine;
using TMPro;

public class HealthTest : MonoBehaviour
{
    public float maxHealth = 100f; // The maximum health value
    private float currentHealth; // The current health
    public LOSE losesc; // Reference to the lose screen manager
    public CameraSwitch camswitch; // Reference to camera switch manager
    public TMP_Text healthTxt; // UI Text for displaying health

    private void Start()
    {
        currentHealth = maxHealth; // Initialize current health
        UpdateHealthText(); // Update health display at the start
    }

    // Method to reduce health
    public void TakeDamage(float amount)
    {
        currentHealth -= amount; // Reduce the current health by the damage amount

        // Log the amount of damage taken and the remaining health
        Debug.Log($"{gameObject.name} took {amount} damage, remaining health: {currentHealth}");

        UpdateHealthText(); // Update health display

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
    protected virtual void Die()
    {
        Debug.Log($"{gameObject.name} has been destroyed!");

        if (losesc != null)
        {
            losesc.ShowCanvas(); // Show lose screen if applicable
        }

        if (camswitch != null)
        {
            camswitch.death(); // Switch to death camera if applicable
        }

        Destroy(gameObject); // Destroy the game object
    }

    // Update health text UI
    private void UpdateHealthText()
    {
        if (healthTxt != null)
        {
            healthTxt.text = currentHealth.ToString(); // Update the health text
        }
        else
        {
            Debug.LogWarning("Health Text is not assigned!");
        }
        
    }
}
