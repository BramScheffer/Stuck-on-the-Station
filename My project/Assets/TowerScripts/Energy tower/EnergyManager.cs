using Microsoft.Unity.VisualStudio.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

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

    public EnergyBarUI energyBarUI;  // Verwijzing naar de UI scrip

    // Update is called once per frame
    void Update()
    {

        currentEnergy -= Time.deltaTime * 5f; // Energie loopt langzaam af
        currentEnergy = Mathf.Clamp(currentEnergy, 0, maxEnergy);

        // Bereken energiepercentage en update de energiebar
        float energyPercent = energy / maxEnergy;
        energyBarUI.UpdateEnergyBar(energyPercent);
        // Increase timer by the time since the last frame
        timer += Time.deltaTime;

        // Check if one second has passed
        if (timer >= 1.0f)
        {
            IncreaseEnergy();
            timer = 0.0f; // Reset timer
        }
        if (energy >= 100f)
        {
            train.transform.Translate(Vector3.forward * 5.0f * Time.deltaTime);
            winSc.ShowCanvas();
        }
    }

    // Method to increase energy
    private void IncreaseEnergy()
    {
        energy += energyIncreaseAmount;
        Debug.Log("Current Energy: " + energy);
    }
}