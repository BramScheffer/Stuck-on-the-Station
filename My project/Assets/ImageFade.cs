using UnityEngine;
using UnityEngine.UI;

public class ImageFadeIn : MonoBehaviour
{
    // Duration in seconds for the fade to complete
    public float fadeDuration = 2.0f;

    // Reference to the Image component
    private Image image;
    private Color originalColor;
    private float fadeTimer = 0.0f;
    private bool isFading = false;
    private bool hasFadedIn = false; // New variable to track if fading is complete

    void Start()
    {
        // Get the Image component from the GameObject
        image = GetComponent<Image>();

        // Store the original color of the image
        originalColor = image.color;

        // Set the initial color with 0 opacity (fully transparent)
        SetOpacity(0);
    }

    void Update()
    {
        // Check if the canvas (or its parent) is active and if the fading should start
        if (gameObject.activeInHierarchy && !isFading && !hasFadedIn)
        {
            StartFadeIn();
        }

        // Perform fading if isFading is true
        if (isFading)
        {
            fadeTimer += Time.deltaTime;

            // Calculate the new alpha value
            float alpha = Mathf.Clamp01(fadeTimer / fadeDuration);

            // Set the image's color with the new alpha value
            image.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);

            // Once the fade is complete, stop fading and set to maximum opacity
            if (fadeTimer >= fadeDuration)
            {
                SetOpacity(1); // Ensure full opacity
                isFading = false;
                hasFadedIn = true; // Mark fading as complete
            }
        }
    }

    // This function starts the fade-in process
    private void StartFadeIn()
    {
        isFading = true;
        fadeTimer = 0.0f;
    }

    // Set the image opacity to the specified value
    private void SetOpacity(float alpha)
    {
        image.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
    }
}
