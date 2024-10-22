using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIPanelFlyIn : MonoBehaviour
{
    public RectTransform panel;         // RectTransform of the UI panel that needs to move
    public Vector3 targetPosition;      // The target position where the panel should end up (on-screen)
    public Button triggerButton;        // The button that triggers the animation
    public Button backButton;           // The "Back" button to trigger reverse animation
    public float flyInSpeed = 2f;       // Speed at which the panel moves to the target position (lower value for smoothness)
    public bool isPanelVisible = false; // Tracks whether the panel is currently visible or not
    private Vector3 offScreenPosition;  // Start position of the panel (off-screen to the right)
    private Coroutine movePanelCoroutine; // Reference to the currently running coroutine

    void Start()
    {
        // Set the start position of the panel off-screen to the right (right side of the screen)
        offScreenPosition = new Vector3(Screen.width, targetPosition.y, 0f);
        panel.anchoredPosition = offScreenPosition;

        // Add listeners to the buttons
        triggerButton.onClick.AddListener(TogglePanel);
        backButton.onClick.AddListener(ClosePanel);


        
    }

    void Update()
    {
        // Listen for the Escape key
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            TogglePanel();

            
        }
    }

    void TogglePanel()
    {
        // Stop any current panel movement coroutine
        if (movePanelCoroutine != null)
        {
            StopCoroutine(movePanelCoroutine);
        }

        if (isPanelVisible)
        {
            // Move the panel off-screen to the right
            movePanelCoroutine = StartCoroutine(MovePanel(offScreenPosition));
        }
        else
        {
            // Move the panel to the target position (left side or center)
            movePanelCoroutine = StartCoroutine(MovePanel(targetPosition));
        }



        isPanelVisible = !isPanelVisible;  // Toggle the panel's visibility state
        Time.timeScale = 0f;
    }

    // Function to handle back button and reverse animation
    void ClosePanel()
    {
        if (isPanelVisible)
        {
            if (movePanelCoroutine != null)
            {
                StopCoroutine(movePanelCoroutine);
            }
            movePanelCoroutine = StartCoroutine(MovePanel(offScreenPosition));
            isPanelVisible = false;  // Set the panel as hidden after moving out
            Time.timeScale = 1f;

        }
    }

    IEnumerator MovePanel(Vector3 destination)
    {
        float elapsedTime = 0f;          // Track time since movement started
        Vector3 startingPosition = panel.anchoredPosition; // Initial position of the panel

        while (elapsedTime < 1f)
        {
            // Interpolate the position using Lerp
            panel.anchoredPosition = Vector3.Lerp(startingPosition, destination, elapsedTime);

            // Increase elapsed time by the adjusted flyInSpeed
            elapsedTime += Time.deltaTime * flyInSpeed;

            yield return null;  // Wait for the next frame
        }

        // Ensure the panel exactly reaches the destination
        panel.anchoredPosition = destination;
        movePanelCoroutine = null;  // Reset the coroutine reference when done
    }
}
