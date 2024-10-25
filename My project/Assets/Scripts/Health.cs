using UnityEngine;
using TMPro;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f; // Maximal health
    public float currentHealth; // Current health
    public CameraSwitch camswitch;
    public GameObject trein;

    // Reference to the TextMeshPro text component to display health
    public TMP_Text healthText;

    private void Start()
    {
        currentHealth = maxHealth; // Set current health to max health at the start
        UpdateHealthText(); // Update the health text at the start
        Debug.Log($"Start health: {currentHealth}");
    }

    private void Update()
    {
        // Continuously log the current health in the console
        Debug.Log($"Current health of {gameObject.name}: {currentHealth}");
        if (currentHealth <= 0f)
        {
            camswitch.death();
        }
    }

    // Method to apply damage
    public void BrengSchadeToe(float amount)
    {
        currentHealth -= amount; // Reduce current health by the damage amount
        Debug.Log($"{gameObject.name} took {amount} damage. Current health: {currentHealth}");

        UpdateHealthText(); // Update the health text display after taking damage

        if (currentHealth <= 0)
        {
            Sterf(); // Call the death function if health drops to 0 or below
        }
    }

    // Check if the object is dead
    public bool IsDead()
    {
        return currentHealth <= 0; // Return true if health is 0 or below
        

    }

    // Remove or destroy the object when it dies
    void Sterf()
    {
        Debug.Log($"{gameObject.name} is vernietigd!");
       
    }

    // Method to update the health display text
    private void UpdateHealthText()
    {
        if (healthText != null)
        {
            healthText.text = $"{currentHealth}";
        }
        else
        {
            Debug.LogWarning("Health text is not assigned in the Inspector.");
        }
    }
}
