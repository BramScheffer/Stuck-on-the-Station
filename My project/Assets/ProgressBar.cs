using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public int max = 100; // Set a default value
    public int current = 0; // Set a default value
    public Image mask;

    void Start()
    {
        UpdateProgressBar();
    }

    void Update()
    {
        // This is just for testing. You can remove it or implement a better way to update the progress.
        if (Input.GetKeyDown(KeyCode.Space)) // Press Space to increase current for testing
        {
            current += 10;
            if (current > max) current = max; // Clamp the value to max
            UpdateProgressBar();
        }
    }

    void UpdateProgressBar()
    {
        if (max > 0) // Avoid division by zero
        {
            float fillAmount = (float)current / (float)max;
            mask.fillAmount = fillAmount;
        }
        else
        {
            mask.fillAmount = 0; // Set to zero if max is zero
        }
    }
}