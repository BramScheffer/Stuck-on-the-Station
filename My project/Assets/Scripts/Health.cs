using UnityEngine;
using TMPro;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f; // De maximale gezondheid
    private float currentHealth; // De huidige gezondheid
    public LOSE losesc; // Referentie naar het verlies script
    public CameraSwitch camswitch; // Referentie naar de camera schakelaar
    public TMP_Text healthTxt; // Referentie naar de tekstcomponent voor gezondheid

    private void Start()
    {
        currentHealth = maxHealth; // Initialiseer de huidige gezondheid
        UpdateHealthUI(); // Update de gezondheid UI bij de start
    }

    // Methode om schade toe te brengen
    public void BrengSchadeToe(float amount)
    {
        currentHealth -= amount; // Verminder de huidige gezondheid met het schadebedrag
        Debug.Log($"{gameObject.name} heeft {amount} schade genomen, resterende gezondheid: {currentHealth}");

        UpdateHealthUI(); // Update de gezondheid UI

        // Controleer of het object moet worden vernietigd
        if (IsDead())
        {
            Sterf(); // Trigger het sterfproces
        }
    }

    // Controleer of het object dood is
    public bool IsDead()
    {
        return currentHealth <= 0;
    }

    // Afhandelen van de dood (bijv. vernietig het gameobject)
    void Sterf()
    {
        Debug.Log($"{gameObject.name} is vernietigd!");

        // Voeg controles toe om ervoor te zorgen dat de referenties niet null zijn
        if (losesc != null)
        {
            losesc.ShowCanvas(); // Laat het verlies canvas zien
        }
        else
        {
            Debug.LogWarning("losesc is not assigned in Health script.");
        }

        if (camswitch != null)
        {
            camswitch.death(); // Verander naar de doods camera
        }
        else
        {
            Debug.LogWarning("camswitch is not assigned in Health script.");
        }

        // Vernietig het gameobject
        Destroy(gameObject); // Je kunt dit vervangen door deactiveren als dat nodig is (bijv. SetActive(false))
    }


    private void Update()
    {
        UpdateHealthUI(); // Bijwerken van de gezondheid UI elke frame
    }

    // Update de gezondheid UI
    private void UpdateHealthUI()
    {
        if (healthTxt != null)
        {
            healthTxt.text = currentHealth.ToString(); // Toon de huidige gezondheid in de tekstcomponent
        }
    }
}
