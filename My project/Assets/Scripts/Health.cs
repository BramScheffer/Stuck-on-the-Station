using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f; // De maximale gezondheid
    private float currentHealth; // De huidige gezondheid

    private void Start()
    {
        currentHealth = maxHealth; // Zet de huidige gezondheid gelijk aan de maximale gezondheid bij het begin
    }

    // Methode om schade toe te brengen
    public void BrengSchadeToe(float amount)
    {
        currentHealth -= amount; // Verminder de huidige gezondheid met het schadebedrag

        if (currentHealth <= 0)
        {
            Sterf(); // Roep de sterf-functie aan als de gezondheid op of onder 0 komt
        }
    }

    // Controleer of het object dood is
    public bool IsDead()
    {
        return currentHealth <= 0; // Retourneer true als de gezondheid 0 of lager is
    }

    // Verwijder of vernietig het object als het dood is
    void Sterf()
    {
        Debug.Log($"{gameObject.name} is vernietigd!");
        Destroy(gameObject); // Vernietig het object als het sterft
    }
}
