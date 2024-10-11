using UnityEngine;
using UnityEngine.UI;

public class EnergyManager : MonoBehaviour
{
    public Win winSc;

    // The amount of energy to increase every second
    [Range(0f, 10f)] // This allows you to set a value between 0 and 10 in the Inspector
    public float energyIncreaseAmount = 0.15f;

    // Current energy level
    private float energy = 0.0f;

    // Timer to track time
    private float timer = 0.0f;

    public GameObject train;

    public float maxEnergy = 100f;
    public float currentEnergy = 100f;

    public EnergyBarUI energyBarUI;  // Reference to the first energy bar UI
    

    // Update is called once per frame
    void Update()
    {
        currentEnergy -= Time.deltaTime * 5f; // Energy slowly drains over time
        currentEnergy = Mathf.Clamp(currentEnergy, 0, maxEnergy);

        // Calculate energy percentage
        float energyPercent = energy / maxEnergy;

        // Update both energy bar images with the same percentage
        energyBarUI.UpdateEnergyBar(energyPercent);
        

        // Increase the timer by the time since the last frame
        timer += Time.deltaTime;

        // Check if one second has passed
        if (timer >= 1.0f)
        {
            IncreaseEnergy();
            timer = 0.0f; // Reset the timer
        }

        // When energy is full, the train moves and a win is triggered
        if (energy >= 100f)
        {
            train.transform.Translate(Vector3.forward * 5.0f * Time.deltaTime);
            winSc.ShowCanvas();
            Cursor.lockState = CursorLockMode.Confined;
        }
    }

    // Method to increase energy
    private void IncreaseEnergy()
    {
        energy += energyIncreaseAmount;
        Debug.Log("Current Energy: " + energy);
    }
}
