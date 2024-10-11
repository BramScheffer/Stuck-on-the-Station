using UnityEngine;

public class CameraWobbleEffect : MonoBehaviour
{
    // Adjust the frequency, amplitude, and options to affect position and/or rotation
    public float wobbleAmplitude = 0.05f; // How much the camera moves
    public float wobbleFrequency = 1f;    // How fast the wobble happens
    public bool affectPosition = true;    // Toggle to affect position
    public bool affectRotation = false;   // Toggle to affect rotation

    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private float wobbleTime = 0f;

    void Start()
    {
        // Store the camera's initial position and rotation at the start of the game
        initialPosition = transform.position;    // Global position, keeping the original place intact
        initialRotation = transform.rotation;    // Global rotation
    }

    void Update()
    {
        wobbleTime += Time.deltaTime;

        // Wobble effect for position using sine waves
        if (affectPosition)
        {
            Vector3 wobbleOffset = new Vector3(
                Mathf.Sin(wobbleTime * wobbleFrequency) * wobbleAmplitude, // Wobble on X axis
                Mathf.Sin((wobbleTime * wobbleFrequency) + Mathf.PI / 2) * wobbleAmplitude, // Wobble on Y axis
                0 // Keep Z axis the same for 2D UI
            );

            // Apply the wobble effect as an offset to the initial position
            transform.position = initialPosition + wobbleOffset;
        }

        // Wobble effect for rotation using sine waves
        if (affectRotation)
        {
            Vector3 wobbleRotation = new Vector3(
                Mathf.Sin(wobbleTime * wobbleFrequency) * wobbleAmplitude * 10f,  // Wobble around X-axis
                Mathf.Sin((wobbleTime * wobbleFrequency) + Mathf.PI / 2) * wobbleAmplitude * 10f, // Wobble around Y-axis
                0 // Keep Z axis the same
            );

            // Apply the wobble effect as an offset to the initial rotation
            transform.rotation = initialRotation * Quaternion.Euler(wobbleRotation);
        }
    }
}
