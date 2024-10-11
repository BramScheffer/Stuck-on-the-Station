using UnityEngine;

public class CustomScaleOverTime : MonoBehaviour
{
    public Vector3 initialScale;  // Starting scale of the object
    public Vector3 targetScale;   // The target scale to reach
    public float duration = 3f;   // Duration over which to scale

    private float elapsedTime = 0f;  // Track the time passed
    private bool scaling = false;    // Control when to scale

    void Start()
    {
        // Optional: Initialize the object's scale with the initialScale at the start
        transform.localScale = initialScale;
    }

    void OnEnable()
    {
        // When the Canvas (or the GameObject) is enabled, start scaling
        StartScaling();
    }

    void Update()
    {
        // If scaling is active, interpolate the scale over time
        if (scaling)
        {
            if (elapsedTime < duration)
            {
                // Increase elapsed time
                elapsedTime += Time.deltaTime;

                // Linearly interpolate between the initial scale and target scale over time
                transform.localScale = Vector3.Lerp(initialScale, targetScale, elapsedTime / duration);
            }
            else
            {
                // Stop scaling once the time duration has passed
                transform.localScale = targetScale;
                scaling = false;
            }
        }
    }

    // Call this function to start scaling
    private void StartScaling()
    {
        // Reset elapsed time and enable scaling
        elapsedTime = 0f;
        scaling = true;

        // Optionally reset the scale to the starting scale when reactivating
        transform.localScale = initialScale;
    }
}
